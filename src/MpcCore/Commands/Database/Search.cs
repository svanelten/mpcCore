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
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Search : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query to get a list of matching items.
		/// If you want case sensitive search set "caseSensitive" to true.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		/// <param name="caseSensitive">optional enable case sensitivity for search</param>
		public Search(IFilter filter, bool caseSensitive = false)
		{
			Command = $"{(caseSensitive ? "find" : "search")} {filter.CreateFilterString()}";
		}

		/// <summary>
		/// Pass a filter expression to get a list of matching items.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching items which don’t have this group tag.
		/// It exists only if at least one such item is found.
		/// Pass a range to get a subset of the actual result.
		/// If you want case sensitive search set "caseSensitive" to true.
		/// </summary>
		/// <param name="filter">filter expression</param>
		/// <param name="caseSensitive">sets the case sensitivity for the search</param>
		/// <param name="sortByTag">optional string tag to sort the result. Append "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public Search(string filter, bool caseSensitive, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			var cmd = caseSensitive ? "find" : "search";

			Command = string.IsNullOrEmpty(sortByTag)
				? $"{cmd} {filter}"
				: $"{cmd} {filter} group {sortByTag}";

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
