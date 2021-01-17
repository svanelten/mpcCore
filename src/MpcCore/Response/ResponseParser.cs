using MpcCore.Contracts.Mpd;
using MpcCore.Extensions;
using MpcCore.Mpd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MpcCore.Response
{
	internal class ResponseParser
	{
		private List<string> _rawResponse;
		private List<KeyValuePair<string, string>> _valueList;

		public bool ResponseHasNoContent => _rawResponse?.Count <= 1;

		public bool ResponseHasMpdError => _rawResponse.IsErrorResponse();

		public ResponseParser(IEnumerable<string> response)
		{
			_rawResponse = response.ToList();
			_valueList = GetKeyValuePairs(_rawResponse);
		}

		/// <summary>
		/// Creates a list of key/value pairs from the response.
		/// Status row is removed.
		/// </summary>
		public List<KeyValuePair<string, string>> GetKeyValuePairs(IEnumerable<string> response)
		{
			var result = new List<KeyValuePair<string, string>>();
			var separator = new[] { ':' };

			foreach (var line in response.Take(response.Count() - 1).ToList())
			{
				var split = line.Split(separator, 2);
				result.Add(new KeyValuePair<string, string>(split[0], split[1].Trim()));
			}

			return result;
		}

		public List<string> GetChangedSystems()
		{
			return _valueList?.Select(e => e.Value).ToList() ?? new List<string>();
		}

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
					case ResponseParserKeys.Time:
#pragma warning disable CS0618 // Typ oder Element ist veraltet
						status.Time = Convert.ToInt32(kv.Value);
#pragma warning restore CS0618 // Typ oder Element ist veraltet
						break;
					case ResponseParserKeys.Elapsed:
						status.Elapsed = Convert.ToInt32(kv.Value);
						break;
					case ResponseParserKeys.Duration:
						status.Duration = Convert.ToInt32(kv.Value);
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
					default:
						throw new Exception($"value '{kv.Key}:{kv.Value}' is not handled");
				}
			}

			return status;
		}

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

		public string GetReplayGainStatus()
		{
			var mode = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.ReplayGainMode);

			return mode?.Value ?? "n/a";
		}

		public static IMpdError ParseMpdError(string command, string statusline)
		{
			var regex = new Regex(@"^ACK \[([\d]*)@([\d]*)\] \{([\w\d -]*)\} ([\w\d -]*)");

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

		public IAudio GetAudio(string str)
		{
			// TODO finish audio settings
			return new Audio
			{

			};
		}

		public string GetFingerprint()
		{
			var print = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.Fingerprint);

			return print?.Value ?? string.Empty;
		}

		public DateTime? GetLastModified()
		{
			var kv = _valueList?.FirstOrDefault(e => e.Key == ResponseParserKeys.LastModified);

			return GetLastModified(kv?.Value);
		}

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
