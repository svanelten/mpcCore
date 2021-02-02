using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Read "comments" (i.e. key-value pairs) from the file specified by path. This path can be a path relative to the music directory or an absolute path.
	/// Can be used to list metadata of remote files (e.g. path beginning with "http://" or "smb://").
	/// MpcCore will try to parse this info into item an DTOs with metadata, all unmatched data will be added to the <see cref="Mpd.Item.UnknownMetadata"/> dictionary.
	/// Comment contents depends on the codec, and not all decoder plugins support it. For example, on Ogg files, this lists the Vorbis comments.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class ReadComments : IMpcCoreCommand<IItem>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="path">path to file</param>
		public ReadComments(string path = "")
		{
			Command = $"readcomments \"{path}\"";
		}

		/// <summary>
		/// Handles the response and returns an item DTO or null if none is found
		/// </summary>
		/// <param name="response">MPD response</param>
		/// <returns>item DTO with metadata</returns>
		public IItem HandleResponse(IMpdResponse response)
		{
			if (!response.HasContent)
			{
				return null;
			}

			var parser = new ResponseParser(response);

			return parser.GetListedTracks().ToList().FirstOrDefault();
		}
	}
}
