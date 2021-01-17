using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Contracts.Mpd.Filter;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Search the database for songs matching a given filter and add them to the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class FindAndAddToQueue : IMpcCoreCommand<IEnumerable<IItem>>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query and add the resulting songs to the queue.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		public FindAndAddToQueue(IFilter filter)
		{
			Command = $"findadd {filter.CreateFilterString()}";
		}

		/// <summary>
		/// Pass a filter expression to add a list of matching songs to the queue.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching songs which don’t have this group tag. It exists only if at least one such song is found.
		/// Pass a range to get a subset of the actual result.
		/// </summary>
		/// <param name="filter">filter expression</param>
		/// <param name="sortByTag">optional string tag to sort the result. Append "-" to sort descending</param>
		/// <param name="rangeStart">optional start position of subset range</param>
		/// <param name="rangeEnd">optional end position of subset range</param>
		public FindAndAddToQueue(string filter, string sortByTag = null, int? rangeStart = null, int? rangeEnd = null)
		{
			Command = string.IsNullOrEmpty(sortByTag)
				? $"findadd {filter}"
				: $"findadd {filter} group {sortByTag}";

			if (rangeStart.HasValue || rangeEnd.HasValue)
			{
				Command += $" window {(rangeStart.HasValue ? rangeStart.ToString() : string.Empty)}:{ (rangeEnd.HasValue ? rangeEnd.Value.ToString() : string.Empty)}";
			}
		}

		public IEnumerable<IItem> HandleResponse(IEnumerable<string> response)
		{
			// TODO: check response type
			var parser = new ResponseParser(response);

			return parser.GetListedTracks();
		}
	}
}
