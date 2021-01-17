using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Filter;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Search the database for songs matching a given filter.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Find : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query to get a list of matching songs.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		public Find(IFilter filter)
		{
			Command = $"find {filter.CreateFilterString()}";
		}

		/// <summary>
		/// Pass a filter expression to get a list of matching songs.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching songs which don’t have this group tag. It exists only if at least one such song is found.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="filter">filter expression</param>
		/// <param name="sortByTag">optional string tag to sort the result. Append "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public Find(string filter, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = string.IsNullOrEmpty(sortByTag)
				? $"find {filter}"
				: $"find {filter} group {sortByTag}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {(rangeStart.HasValue ? rangeStart.ToString() : string.Empty)}:{ (rangeEnd.HasValue ? rangeEnd.Value.ToString() : string.Empty)}";
			}
		}

		public IEnumerable<IItem> HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			return parser.GetListedTracks();
		}
	}
}
