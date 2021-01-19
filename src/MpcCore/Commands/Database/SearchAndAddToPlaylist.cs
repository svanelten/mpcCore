using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Filter;
using MpcCore.Extensions;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Search the database for items matching a given filter and add them to a playlist.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	// TODO: verify response type, should probably be SimpleCommand
	public class SearchAndAddToPlaylist : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query and add the resulting items to the queue.
		/// If a playlist by that name doesn’t exist it is created.
		/// </summary>
		/// <param name="name">playlist name</param>
		/// <param name="filter">IFilter instance</param>
		public SearchAndAddToPlaylist(string name, IFilter filter)
		{
			Command = $"searchaddpl \"{name}\" {filter.CreateFilterString()}";
		}

		/// <summary>
		/// Pass a filter expression to add a list of matching items to a playlist.
		/// If a playlist by that name doesn’t exist it is created.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Mpd.Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching items which don’t have this group tag. 
		/// It exists only if at least one item song is found.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="name">playlist name</param>
		/// <param name="filter">filter expression</param>
		/// <param name="sortByTag">optional string tag to sort the result. Append "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public SearchAndAddToPlaylist(string name, string filter, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = string.IsNullOrEmpty(sortByTag)
				? $"searchaddpl \"{name}\" {filter}"
				: $"searchaddpl \"{name}\" {filter} group {sortByTag}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
		}

		public IEnumerable<IItem> HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedTracks();
		}
	}
}
