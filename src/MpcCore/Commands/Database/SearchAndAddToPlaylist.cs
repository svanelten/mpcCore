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
	public class SearchAndAddToPlaylist : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query and add the resulting items to the queue.
		/// If a playlist by that name doesn’t exist it is created.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="name">playlist name</param>
		/// <param name="filter">IFilter instance</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public SearchAndAddToPlaylist(string name, IFilter filter, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"searchaddpl \"{name}\" {filter.CreateFilterString()}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
		}

		/// <summary>
		/// Search the database for items matching the given parameters and adds them to a playlist.
		/// Pass a filter expression to get a list of matching items. Be aware that you need to escape the query string!
		/// If a playlist by that name doesn’t exist it is created.
		/// Search the database for items matching a given filter and add them to a playlist.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Mpd.Tag"/> string to to sort the results by a tag. A group with an empty value contains counts of matching items which don’t have this group tag. 
		/// It exists only if at least one item song is found.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="name">playlist name</param>
		/// <param name="filter">filter expression</param>
		/// <param name="sortByTag">optional string tag to sort the result. Prepend "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public SearchAndAddToPlaylist(string name, string filter, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = string.IsNullOrEmpty(sortByTag)
				? $"searchaddpl \"{name}\" {filter}"
				: $"searchaddpl \"{name}\" {filter} sort {sortByTag}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
		}

		public IEnumerable<IItem> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedTracks();
		}
	}
}
