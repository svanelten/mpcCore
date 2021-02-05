using MpcCore.Contracts.Mpd;
using MpcCore.Mpd;
using System;
using System.Collections.Generic;

namespace MpcCore.Response
{
	internal class TrackBuilder
	{
		private Item _track;

		public TrackBuilder New(string path)
		{
			_track = new Item() { Path = path };
			return this;
		}

		public TrackBuilder Add(KeyValuePair<string, string> item)
		{
			switch (item.Key.ToLower())
			{
				// file path / stream url
				case ResponseParserKeys.File:
					_track.Path = item.Value;
					break;

				// playlist / queue data
				case ResponseParserKeys.Duration:
					_track.Duration = Convert.ToDouble(item.Value, System.Globalization.CultureInfo.InvariantCulture);
					break;
				case ResponseParserKeys.Position:
					_track.Position = Convert.ToInt32(item.Value);
					break;
				case ResponseParserKeys.Id:
					_track.Id = Convert.ToInt32(item.Value);
					break;
				case ResponseParserKeys.Range:
					_track.Range = item.Value;
					break;
				case ResponseParserKeys.Format:
					_track.Format = item.Value;
					break;
				
				// unique data
				case Tag.Disc:
					_track.Disc = Convert.ToInt32(item.Value);
					break;
				case Tag.Track:
					_track.Track = Convert.ToInt32(item.Value);
					break;

				// section musicbrainz
				case Tag.MusicBrainzArtistId:
					_track.MusicBrainz.ArtistId = item.Value;
					break;
				case Tag.MusicBrainzAlbumId:
					_track.MusicBrainz.AlbumId = item.Value;
					break;
				case Tag.MusicBrainzAlbumArtistId:
					_track.MusicBrainz.AlbumArtistId = item.Value;
					break;
				case Tag.MusicBrainzTrackId:
					_track.MusicBrainz.TrackId = item.Value;
					break;
				case Tag.MusicBrainzReleaseTrackId:
					_track.MusicBrainz.ReleaseTrackId = item.Value;
					break;
				case Tag.MusicBrainzWorkId:
					_track.MusicBrainz.WorkId = item.Value;
					break;

				// add tag value to list
				default:
					_track.AddMetaData(item.Key, item.Value);
					break;
			}

			return this;
		}

		public bool IsEmpty() => _track == null || string.IsNullOrEmpty(_track.Path);

		public void Clear() => _track = null;

		public IItem Get()
		{
			return _track;
		}
	}
}
