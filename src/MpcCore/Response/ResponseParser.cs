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
	/// TODO: fix the method TODOs.
	/// TODO: clean up, maybe split into some logical subcomponents?
	/// </summary>
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
		/// Reads a sticker value from the MPD response
		/// </summary>
		/// <param name="path">The path to the MPD item this sticker belongs to</param>
		/// <param name="type">The sticker type</param>
		/// <returns></returns>
		public List<ISticker> GetStickerList(string path = "", string type = StickerType.Song)
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
		/// TODO: make more efficient/cleaner
		/// TODO: merge with TrackBuilder functionality to read file metadata
		/// TODO: add playlist recognition
		/// </summary>
		/// <param name="path">The requested path from the command</param>
		/// <returns>IDirectory instance</returns>
		public IDirectory GetDirectoryListing(string path = "")
		{
			var split = splitPath(path);

			var result = new Directory { Name = split.Last(), Path = path };

			Directory currentDirectory = null;
			Item currentFile = null;

			foreach (var kv in _valueList)
			{
				if (kv.Key == ResponseParserKeys.Directory)
				{
					if (currentDirectory != null)
					{
						result.Directories.Add(currentDirectory);
					}

					var newDirValues = splitPath(kv.Value);
					currentDirectory = new Directory { Name = newDirValues.Last(), Path = kv.Value };
				}
				else if (kv.Key == ResponseParserKeys.File)
				{
					var newFileValues = splitPath(kv.Value);
					currentFile = new Item { Name = newFileValues.Last(), Path = kv.Value };
					if (currentDirectory != null)
					{
						currentDirectory.Files.Add(currentFile);
					}
					else
					{
						result.Files.Add(currentFile);
					}
				}
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
						status.Elapsed = Double.Parse(kv.Value, System.Globalization.CultureInfo.InvariantCulture);
						break;
					case ResponseParserKeys.Duration:
						status.Duration = Double.Parse(kv.Value, System.Globalization.CultureInfo.InvariantCulture);
						break;
					case ResponseParserKeys.Bitrate:
						status.Bitrate = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Crossfade:
						status.Crossfade = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.MixRampDb:
						status.MixRampDb = Convert.ToDouble(kv.Value);
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
		/// <returns>int volume or null</returns>
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
		/// TODO: should probably refactored with a null response for uniform result pattern
		/// </summary>
		/// <returns>replaygain string value or n/a</returns>
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
		/// TODO: add regex and type conversion
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public IAudio GetAudio(string str)
		{
			return new Audio
			{

			};
		}

		/// <summary>
		/// Reads the chroma fingerprint from an MPD response. 
		/// Empty if mpd does not provide the functionality
		/// </summary>
		/// <returns></returns>
		public string GetFingerprint()
		{
			var print = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.Fingerprint);

			return print?.Value ?? string.Empty;
		}

		/// <summary>
		/// Tries to parse a last-modified date from a MPD response.
		/// Returns null if not possible
		/// </summary>
		/// <returns>DateTime or null</returns>
		public DateTime? GetLastModified()
		{
			var kv = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.LastModified);

			return GetLastModified(kv?.Value);
		}

		/// <summary>
		/// Tries to parse a last-modified date from a string.
		/// Returns null if not possible
		/// </summary>
		/// <returns>DateTime or null</returns>
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
		/// <returns></returns>
		public IJob GetJobInformation()
		{
			var jobInfo = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.UpdatingDb);

			if (string.IsNullOrEmpty(jobInfo?.Value))
			{
				return null;
			}

			return new Job
			{
				JobName = ResponseParserKeys.UpdatingDb,
				JobId = Convert.ToInt32(jobInfo?.Value ?? "0")
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
			var list = new List<IItem>();

			if (!_valueList.Any())
			{
				return list;
			}

			var builder = new TrackBuilder();

			foreach (var item in _valueList)
			{
				if (item.Key.Equals(ResponseParserKeys.File))
				{
					if (!builder.IsEmpty())
					{
						list.Add(builder.Create());
					}

					builder.New(item.Value);
				}
				else
				{
					builder.Add(item);
				}
			}

			if (!builder.IsEmpty())
			{
				list.Add(builder.Create());
			}

			return list;
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
		/// Splits a path string into its parts and does sets some minimum values if neccessary
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
		/// <returns>DateTime or null</returns>
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
