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
			_parse(item);
			return this;
		}

		public IItem Create()
		{
			return _track;
		}

		public bool IsEmpty()
		{
			return _track == null || string.IsNullOrEmpty(_track.Path);
		}

		private void _parse(KeyValuePair<string, string> item)
		{
			// TODO: add append/update functionality for multiple values in one tag
			// TODO: maybe move non-tagdata to a common parser functionality
			switch (item.Key.ToLower())
			{
				// file path / stream url
				case "file":
					_track.Path = item.Value;
					break;

				// playlist / queue data
				case "duration":
					_track.Duration = Convert.ToDouble(item.Value);
					break;
				case "pos":
					_track.Position = Convert.ToInt32(item.Value);
					break;
				case "id":
					_track.Id = Convert.ToInt32(item.Value);
					break;
				case "range":
					_track.Range = item.Value;
					break;
				case "format":
					_track.Format = item.Value;
					break;
				#pragma warning disable CS0618 // Typ oder Element ist veraltet
				case "time":
					_track.Time = Convert.ToInt32(item.Value);
					break;
				#pragma warning restore CS0618 // Typ oder Element ist veraltet

				// common tag metadata
				case Tag.Album:
					_track.Album = item.Value;
					break;
				case Tag.AlbumSort:
					_track.AlbumSortable = item.Value;
					break;
				case Tag.AlbumArtist:
					_track.AlbumArtist = item.Value;
					break;
				case Tag.AlbumArtistSort:
					_track.AlbumArtistSortable = item.Value;
					break;
				case Tag.Artist:
					_track.Artist = item.Value;
					break;
				case Tag.ArtistSort:
					_track.ArtistSortable = item.Value;
					break;
				case Tag.Title:
					_track.Title = item.Value;
					break;
				case Tag.Name:
					_track.Name = item.Value;
					break;
				case Tag.Genre:
					_track.Genre = item.Value;
					break;
				case Tag.Date:
					_track.Date = item.Value;
					break;
				case Tag.OriginalDate:
					_track.OriginalDate = item.Value;
					break;
				case Tag.Composer:
					_track.Composer = item.Value;
					break;
				case Tag.Performer:
					_track.Performer = item.Value;
					break;
				case Tag.Conductor:
					_track.Conductor = item.Value;
					break;
				case Tag.Work:
					_track.Work = item.Value;
					break;
				case Tag.Comment:
					_track.Comment = item.Value;
					break;
				case Tag.Disc:
					_track.Disc = Convert.ToInt32(item.Value);
					break;
				case Tag.Label:
					_track.Label = item.Value;
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

				// default value: unknown tags
				default:
					_track.UnknownMetadata.Add(item);
					break;
			}
		}
	}

}
