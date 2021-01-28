using MpcCore.Commands.Base;
using MpcCore.Contracts.Mpd.Filter;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Count the number of items and their total playtime in the database matching the given filter.
	/// The group keyword may be used to group the results by a tag.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Count : IntCommandBase
	{
		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query to get a count of matching items.
		/// Pass a <see cref="Mpd.Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching items which don’t have this group tag. It exists only if at least one such items is found.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		/// <param name="groupByTag">optional string tag to group results, eg <see cref="Mpd.Tag.Artist"/></param>
		public Count(IFilter filter, string groupByTag = null)
		{
			Command = string.IsNullOrEmpty(groupByTag)
				? $"count {filter.CreateFilterString()}"
				: $"count {filter.CreateFilterString()} group {groupByTag}";
		}

		/// <summary>
		/// Pass a filter expression to get a count of matching items.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching items which don’t have this group tag. It exists only if at least one such items is found.
		/// </summary>
		/// <param name="filter">filter expression, remember to escape special characters!</param>
		/// <param name="groupByTag">optional string tag to group results by</param>
		public Count(string filter, string groupByTag = null)
		{
			Command = string.IsNullOrEmpty(groupByTag) 
				? $"count {filter}" 
				: $"count {filter} group {groupByTag}";
		}
	}
}
