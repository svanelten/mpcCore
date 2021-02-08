using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MpcCore.Response
{
	/// <summary>
	/// The Responseparser does all the heavy lifting parsing the MPD response
	/// into DTOs for the different result types.
	/// </summary>
	// TODO: fix the method TODOs.
	// TODO: clean up, maybe split into some logical subcomponents?
	internal class ResponseParser
	{
		/// <summary>
		/// The response string list straight from the reader
		/// </summary>
		private IMpdResponse _response;

		/// <summary>
		/// Key-Value list of all "key: value" pairs from the response,
		/// minus state and error lines. Values are trimmed to get rid of the space.
		/// </summary>
		private List<KeyValuePair<string, string>> _valueList;

		/// <summary>
		/// Constructor. 
		/// Takes the response from the server and calls <see cref="_getKeyValuePairs"/> to create the valueList for further processing
		/// </summary>
		/// <param name="response">Response string list from the MPD server</param>
		public ResponseParser(IMpdResponse response)
		{
			_response = response;
			_valueList = _getKeyValuePairs(response.RawResponse);
		}

		/// <summary>
		/// Parses the list of changed systems from a MPD idle response
		/// </summary>
		/// <returns>string list of changed systems</returns>
		public List<string> GetChangedSystems()
		{
			return _valueList?.Select(e => e.Value).ToList() ?? new List<string>();
		}

		/// <summary>
		/// Returns an the id for a given song from the response
		/// </summary>
		/// <returns></returns>
		public int GetItemId()
		{
			var val = _valueList?.FirstOrDefault(k => k.Key == ResponseParserKeys.Id);
			return val != null ? Convert.ToInt32(val.Value.Value) : 0;
		}

		/// <summary>
		/// Gets a list of mounts from a response
		/// </summary>
		/// <returns>IEnumerable<IMount> List of mount</returns>
		public IEnumerable<IMount> GetListedMounts()
		{
			var result = new List<IMount>();
			IMount mount = null;

			foreach (var item in _valueList)
			{
				if (item.Key == ResponseParserKeys.MountName)
				{
					if (mount != null)
					{
						result.Add(mount);
					}

					mount = new Mount { Name = item.Value };
				}

				if (item.Key == ResponseParserKeys.MountStorage)
				{
					mount.Path = item.Value;
				}
			}

			if (mount != null)
			{
				result.Add(mount);
			}

			return result;
		}

		/// <summary>
		/// Gets a list of neighbors from a response
		/// </summary>
		/// <returns>IEnumerable<INeighbor> List of neighbors</returns>
		public IEnumerable<INeighbor> GetListedNeighbors()
		{
			var result = new List<INeighbor>();
			INeighbor neighbor = null;

			foreach (var item in _valueList)
			{
				if (item.Key == ResponseParserKeys.NeighborName)
				{
					if (neighbor != null)
					{
						result.Add(neighbor);
					}

					neighbor = new Neighbor { Name = item.Value };
				}

				if (item.Key == ResponseParserKeys.NeighborStorage)
				{
					neighbor.Path = item.Value;
				}
			}

			if (neighbor != null)
			{
				result.Add(neighbor);
			}

			return result;
		}

		/// <summary>
		/// Gets a list of output devices from the response;
		/// </summary>
		/// <returns>IEnumerable<IOutputDevice></returns>
		public IEnumerable<IOutputDevice> GetListedOutputDevices()
		{
			var result = new List<IOutputDevice>();
			OutputDevice device = null;

			foreach (var item in _valueList)
			{
				if (item.Key == ResponseParserKeys.OutputDeviceId)
				{
					if (device != null)
					{
						result.Add(device);
					}

					device = new OutputDevice { Id = Convert.ToInt32(item.Value) };
				}

				switch (item.Key)
				{
					case ResponseParserKeys.OutputDeviceEnabled:
						device.Enabled = item.Value == "1";
						break;
					case ResponseParserKeys.OutputDeviceName:
						device.Name = item.Value;
						break;
					case ResponseParserKeys.Plugin:
						device.Plugin = item.Value;
						break;
					case ResponseParserKeys.OutputDeviceAttribute:
						device.Attributes.ToList().Add(item.Value);
						break;
					default:
						break;
				}
			}

			if (device != null)
			{
				result.Add(device);
			}

			return result;
		}

		/// <summary>
		/// Gets a list of DecoderPlugins from the response;
		/// </summary>
		/// <returns>IEnumerable<IDecoderPlugin></returns>
		public IEnumerable<IDecoderPlugin> GetListedDecoderPlugins()
		{
			var result = new List<IDecoderPlugin>();
			IDecoderPlugin plugin = null;

			foreach (var item in _valueList)
			{
				if (item.Key == ResponseParserKeys.Plugin)
				{
					if (plugin != null)
					{
						result.Add(plugin);
					}

					plugin = new DecoderPlugin { Name = item.Value };
				}

				switch (item.Key)
				{
					case ResponseParserKeys.MimeType:
						plugin.SupportedMimeTypes.ToList().Add(item.Value);
						break;
					case ResponseParserKeys.Suffix:
						plugin.SupportedSuffixes.ToList().Add(item.Value);
						break;
					default:
						break;
				}
			}

			if (plugin != null)
			{
				result.Add(plugin);
			}

			return result;
		}

		/// <summary>
		/// Parses the MPD statistics DTO from a MPD response
		/// </summary>
		/// <returns>Statistics DTO</returns>
		public IStatistics GetStatistics()
		{
			var statistics = new Statistics();

			foreach (var kv in _valueList)
			{
				switch (kv.Key)
				{
					case ResponseParserKeys.Albums:
						statistics.Albums = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Artists:
						statistics.Artists = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.DbUpdate:
						statistics.DatabaseLastUpdate = _getDateTimeFromTimestamp(kv.Value);
						break;
					case ResponseParserKeys.DbPlaytime:
						statistics.DatabasePlaytime = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Playtime:
						statistics.Playtime = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Songs:
						statistics.Songs = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Uptime:
						statistics.Uptime = Convert.ToInt32(kv.Value);
						break;
					default:
						throw new Exception($"value '{kv.Key}:{kv.Value}' is not handled");
				}
			}

			return statistics;
		}

		/// <summary>
		/// Returns all values from the current response that have the given key
		/// </summary>
		/// <returns>String list</returns>
		public IEnumerable<string> GetValueList(string key)
		{
			return _valueList.Where(e => e.Key.ToLower() == key.ToLower()).Select(e => e.Value).ToList();
		}

		/// <summary>
		/// Returns the raw key:value pairs from the response, minus the status line.
		/// </summary>
		/// <returns>IEnumerable<KeyValuePair<string, string>></returns>
		public IEnumerable<KeyValuePair<string, string>> GetKeyValueList()
		{
			return _valueList;
		}

		/// <summary>
		/// Reads a list of sticker values from the MPD response
		/// </summary>
		/// <param name="path">The path to the MPD item this sticker belongs to or is situated under in case of dir searches. 
		/// Each sticker item in the response will have this path by default, unless the response defines one.</param>
		/// <param name="type">The sticker type from <see cref="StickerType"/></param>
		/// <returns></returns>
		public List<ISticker> GetListedSticker(string path = "", string type = StickerType.Song)
		{
			var result = new List<ISticker>();
			var filePath = path;

			foreach (var item in _valueList)
			{
				if (item.Key == ResponseParserKeys.File)
				{
					filePath = item.Value;
				}

				if (item.Key == ResponseParserKeys.Sticker && !string.IsNullOrEmpty(item.Value))
				{
					var split = item.Value.Split('=', 2);

					var sticker = new Sticker()
					{
						Path = filePath,
						Type = type,
						Name = split[0],
						Value = split.Length > 1 ? split[1] : string.Empty
					};

					result.Add(sticker);
				}
			}

			return result;
		}

		/// <summary>
		/// Parses a directory list MPD response into directory and item DTOs
		/// TODO: add playlist recognition
		/// TODO: recreate dir structure
		/// </summary>
		/// <param name="path">The requested path from the command</param>
		/// <returns>IDirectory instance</returns>
		public IDirectory GetDirectoryListing(string path = "")
		{
			var split = splitPath(path);
			var result = new Directory { Name = split.Last(), Path = path };
			var builder = new TrackBuilder();

			// we will skip the first line of the response containing the entry for the requested dir
			var skip = 1;

			// check if there is a second line wit a datetime for the requested dir
			if (_valueList.Count >= 2 && _valueList.ElementAt(1).Key.ToLower().StartsWith(ResponseParserKeys.LastModified))
			{
				result.LastModified = GetLastModified(_valueList.ElementAt(1).Value);
				// if yes, skip this line as well
				skip = 2;
			}

			// the current dir being looked at
			IDirectory currentDirectory = null;

			// keeps track if the current section is a directory or not
			bool currentSectionIsDirectory = true;

			foreach (var kv in _valueList.Skip(skip))
			{
				var key = kv.Key.ToLower();

				// if we have been collecting metadata for a file and now there is a new file/directory section,
				// add the current file to the currently parsed direcory
				if (key == ResponseParserKeys.Directory || key == ResponseParserKeys.File)
				{
					if (!builder.IsEmpty())
					{
						if (currentDirectory == null)
						{
							result.Files.Add(builder.Get());
						}
						else
						{
							currentDirectory.Files.Add(builder.Get());
						}

						builder.Clear();
					}

					// only do it if the current dir is _not_ the main entry dir
					if (currentDirectory != null)
					{
						result.Directories.Add(currentDirectory);
					}
				}

				// a new directory section begins
				// start a new directory dto, add the last one to the main directory
				if (key == ResponseParserKeys.Directory)
				{
					currentSectionIsDirectory = true;

					var newDirValues = splitPath(kv.Value);
					currentDirectory = new Directory { Name = newDirValues.Last(), Path = kv.Value };
				}
				// a new file section begins
				// start a new file dto
				else if (key == ResponseParserKeys.File)
				{
					currentSectionIsDirectory = false;

					// having added the last file to the current dir at the top,
					// we start reading a new one here
					builder.New(kv.Value);
				}
				// neither file nor dir, this is a metadata value for something.
				// add data to the current file, unless we are in a directory section
				else
				{
					if (currentSectionIsDirectory)
					{
						// in directory sections, this can only be a last-modified
						if (key == ResponseParserKeys.LastModified)
						{
							currentDirectory.LastModified = GetLastModified(kv.Value);
						}
					}
					else
					{
						builder.Add(kv);
					}
				}
			}

			// add the last element in the pipeline
			if (!currentSectionIsDirectory && !builder.IsEmpty())
			{
				currentDirectory.Files.Add(builder.Get());
			}

			if (currentSectionIsDirectory && currentDirectory != null)
			{
				result.Directories.Add(currentDirectory);
			}

			return result;
		}

		/// <summary>
		/// Reads the status info from the MPD response and parses it into an Status DTO
		/// </summary>
		/// <returns>IStatus current MPD status info</returns>
		public IStatus GetStatus()
		{
			var status = new Status();

			foreach (var kv in _valueList)
			{
				switch (kv.Key)
				{
					case ResponseParserKeys.Partition:
						status.Partition = kv.Value;
						break;
					case ResponseParserKeys.Volume:
						status.Volume = kv.Value == "-1" ? 0 : Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Repeat:
						status.Repeat = kv.Value == "1";
						break;
					case ResponseParserKeys.Random:
						status.Random = kv.Value == "1";
						break;
					case ResponseParserKeys.Single:
						status.Single = kv.Value == "1" || kv.Value == "oneshot";
						break;
					case ResponseParserKeys.Consume:
						status.Consume = kv.Value == "1";
						break;
					case ResponseParserKeys.Playlist:
						status.Playlist = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.PlaylistLength:
						status.PlaylistLength = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.State:
						status.State = kv.Value;
						break;
					case ResponseParserKeys.Song:
						status.Song = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.SongId:
						status.SongId = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.NextSong:
						status.NextSong = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.NextSongId:
						status.NextSongId = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Elapsed:
						status.Elapsed = double.Parse(kv.Value, System.Globalization.CultureInfo.InvariantCulture);
						break;
					case ResponseParserKeys.Duration:
						status.Duration = double.Parse(kv.Value, System.Globalization.CultureInfo.InvariantCulture);
						break;
					case ResponseParserKeys.Bitrate:
						status.Bitrate = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Crossfade:
						status.Crossfade = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.MixRampDb:
						status.MixRampDb = double.Parse(kv.Value, System.Globalization.CultureInfo.InvariantCulture);
						break;
					case ResponseParserKeys.MixRampDelay:
						status.MixRampDelay = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Audio:
						status.AudioSetting = GetAudio(kv.Value);
						break;
					case ResponseParserKeys.Error:
						status.Error = ResponseParser.ParseMpdError("error in status", kv.Value);
						break;
					case ResponseParserKeys.UpdatingDb:
						status.UpdateJobId = Convert.ToInt32(kv.Value);
						break;
					// deprecated values to ignore
					case ResponseParserKeys.Time:
						break;
					// default
					default:
						throw new Exception($"value '{kv.Key}:{kv.Value}' is not handled");
				}
			}

			return status;
		}

		/// <summary>
		/// Reads the volume info from the mpd response.
		/// Will return if there is no value
		/// </summary>
		/// <returns>Nullable int volume</returns>
		public int? GetVolume()
		{
			var volume = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.Volume);
			int? result = null;

			if (volume != null && !string.IsNullOrEmpty(volume.Value.Value))
			{
				result = Convert.ToInt32(volume.Value);
			}

			return result;
		}

		/// <summary>
		/// Reads the replaygainvalue from the MPD response
		/// </summary>
		/// <returns>replaygain string value or n/a</returns>
		// TODO: should probably refactored with a null response for uniform result pattern
		public string GetReplayGainStatus()
		{
			var mode = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.ReplayGainMode);

			return mode?.Value ?? "n/a";
		}

		/// <summary>
		/// Reads the mpd error info from a MPD response
		/// </summary>
		/// <param name="command">sent MPD command</param>
		/// <param name="statusline">string line from the MPD response</param>
		/// <returns>IMpdError DTO</returns>
		public static IMpdError ParseMpdError(string command, string statusline)
		{
			var regex = new Regex(@"^ACK \[([\d]*)@([\d]*)\] \{([\w\d -]*)\} ([\w\d: -]*)");

			var error = new MpdError
			{
				Message = "Unspecified error",
				RawError = statusline,
				Command = command
			};

			if (statusline == Constants.Ack)
			{
				return error;
			}

			var match = regex.Match(statusline);

			if (match.Groups.Count == 5)
			{
				error.Code = match.Groups[1].Value;
				error.Line = match.Groups[2].Value;
				error.Command = match.Groups[3].Value;
				error.Message = match.Groups[4].Value;
			}

			return error;
		}

		/// <summary>
		/// Reads the current audio setting value from the MPD response
		/// </summary>
		/// <param name="str">audio value string</param>
		/// <returns>Audio object</returns>
		public IAudio GetAudio(string str)
		{
			var regex = new Regex(@"^([0-9]*):([0-9]*):([0-9]*)$");
			var match = regex.Match(str);

			if (match.Groups.Count == 4)
			{
				return new Audio
				{
					SampleRate = int.Parse(match.Groups[1].Value),
					Bits = int.Parse(match.Groups[2].Value),
					Channels = int.Parse(match.Groups[3].Value),
				};
			}

			return null;
		}

		/// <summary>
		/// Reads the chroma fingerprint from an MPD response. 
		/// Empty if mpd does not provide the functionality
		/// </summary>
		/// <returns>string fingerprint</returns>
		public string GetFingerprint()
		{
			return GetValueList(ResponseParserKeys.Fingerprint).FirstOrDefault() ?? string.Empty;
		}

		/// <summary>
		/// Tries to parse a last-modified date from a MPD response.
		/// Returns null if not possible
		/// </summary>
		/// <returns>DateTime or null</returns>
		public DateTime? GetLastModified()
		{
			return GetLastModified(GetValueList(ResponseParserKeys.LastModified).FirstOrDefault());
		}

		/// <summary>
		/// Tries to parse a last-modified date from a string.
		/// Returns null if not possible
		/// </summary>
		/// <returns>Nullable DateTime</returns>
		public DateTime? GetLastModified(string str = "")
		{
			if (!string.IsNullOrEmpty(str))
			{
				DateTime dateTime;

				if (DateTime.TryParse(str, out dateTime))
				{
					return dateTime;
				}
			}

			return null;
		}

		/// <summary>
		/// Reads the information on a MPD jon from a MPD response
		/// </summary>
		/// <returns>IJob object</returns>
		public IJob GetJobInformation()
		{
			var jobInfo = GetValueList(ResponseParserKeys.UpdatingDb).FirstOrDefault();

			if (string.IsNullOrEmpty(jobInfo))
			{
				return null;
			}

			return new Job
			{
				JobName = ResponseParserKeys.UpdatingDb,
				JobId = Convert.ToInt32(jobInfo ?? "0")
			};
		}

		/// <summary>
		///  Reads playlist info from a MPD response
		/// </summary>
		/// <returns>List of playlists</returns>
		public IEnumerable<IPlaylist> GetListedPlaylists()
		{
			var list = new List<IPlaylist>();

			if (!_valueList.Any())
			{
				return list;
			}

			IPlaylist playlist = null;

			foreach (var item in _valueList)
			{
				if (item.Key.Equals(ResponseParserKeys.Playlist))
				{
					if (playlist != null)
					{
						list.Add(playlist);
						playlist = null;
					}

					playlist = new Playlist { Name = item.Value };
				}
				else
				{
					playlist.LastModified = GetLastModified(item.Value);
				}
			}

			if (playlist != null)
			{
				list.Add(playlist);
			}

			return list;
		}

		/// <summary>
		/// Reads the items and their metadata from a MPD response
		/// </summary>
		/// <returns>List of MPD items with metadata</returns>
		public IEnumerable<IItem> GetListedTracks()
		{
			var result = new List<IItem>();
			var builder = new TrackBuilder();

			foreach (var kv in _valueList)
			{
				if (kv.Key.Equals(ResponseParserKeys.File))
				{
					if (!builder.IsEmpty())
					{
						result.Add(builder.Get());
					}

					builder.New(kv.Value);
				}
				else
				{
					if (!builder.IsEmpty())
					{
						builder.Add(kv);
					}
				}
			}

			if (!builder.IsEmpty())
			{
				result.Add(builder.Get());
			}

			return result;
		}

		/// <summary>
		/// Creates a list of key/value pairs from the response.
		/// Status row is removed.
		/// </summary>
		private List<KeyValuePair<string, string>> _getKeyValuePairs(IEnumerable<string> response)
		{
			var result = new List<KeyValuePair<string, string>>();
			var separator = new[] { ':' };

			foreach (var line in response.Take(response.Count() - 1).ToList())
			{
				if (line.Contains(Constants.KeyValueMarker))
				{
					var split = line.Split(separator, 2);
					result.Add(new KeyValuePair<string, string>(split[0], split[1].Trim()));
				}
			}

			return result;
		}

		/// <summary>
		/// Splits a path string into its parts and sets some minimum values if neccessary
		/// </summary>
		/// <param name="path">file/directory path</param>
		/// <returns>List of path levels</returns>
		private List<string> splitPath(string path = "")
		{
			var list = new List<string>();
			if (string.IsNullOrEmpty(path))
			{
				list.Add("/");
			}
			else
			{
				if (path.Contains("/"))
				{
					list.AddRange(path.Split("/"));
				}
				else
				{
					list.Add($"{path}");
				}
			}

			return list;
		}

		/// <summary>
		/// Converts a unix timestamp into a DateTime.
		/// Returns null if string is empty.
		/// </summary>
		/// <param name="str">unix timestamp string</param>
		/// <returns>Nullable DateTime</returns>
		// TODO: does this really need to live here?
		private DateTime? _getDateTimeFromTimestamp(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return null;
			}

			var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);

			return dt.AddSeconds(Convert.ToInt32(str));
		}
	}
}
