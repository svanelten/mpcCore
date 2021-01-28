using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Filter;
using MpcCore.Extensions;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Search the database for items matching a given filter.
	/// Includes the "search" and "find" MPD functionality.
	/// Pass a filter object to get a list of matching items.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
	/// </summary>
	public class Search : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query to get a list of matching items.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public Search(IFilter filter, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = $"{(filter.CaseSensitive ? "find" : "search")} {filter.CreateFilterString()}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {rangeStart.GetParamString()}:{rangeEnd.GetParamString()}";
			}
		}

		/// <summary>
		/// Search the database for items matching a given filter.
		/// Includes the "search" and "find" MPD functionality.
		/// Pass a filter expression to get a list of matching items. Be aware that you need to escape the query string!
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Mpd.Tag"/> string to sort the results by this tag. A group with an empty value contains counts of matching items which don’t have this group tag.
		/// It exists only if at least one such item is found.
		/// Pass a range to get a subset of the actual result.
		/// If you want case sensitive search set "caseSensitive" to true.
		/// </summary>
		/// <param name="filter">filter expression, remember to escape special characters!</param>
		/// <param name="caseSensitive">sets the case sensitivity for the search</param>
		/// <param name="sortByTag">optional string tag to sort the result. Prepend "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public Search(string filter, bool caseSensitive, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			var cmd = caseSensitive ? "find" : "search";

			Command = string.IsNullOrEmpty(sortByTag)
				? $"{cmd} \"{filter}\" "
				: $"{cmd} \"{filter}\" sort {sortByTag}";

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
