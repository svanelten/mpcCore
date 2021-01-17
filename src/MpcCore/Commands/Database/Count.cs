using MpcCore.Contracts;
using MpcCore.Contracts.Mpd.Filter;
using MpcCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Database
{
	/// <summary>
	/// Count the number of songs and their total playtime in the database matching the given filter.
	/// The group keyword may be used to group the results by a tag.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-music-database"/>
	/// </summary>
	public class Count : IMpcCoreCommand<int>
	{
		public string Command { get; internal set; }

		/// <summary>
		/// Pass a <see cref="IFilter"/> instance with a configured query to get a count of matching songs.
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching songs which don’t have this group tag. It exists only if at least one such song is found.
		/// </summary>
		/// <param name="filter">IFilter instance</param>
		/// <param name="groupByTag">optional string tag</param>
		public Count(IFilter filter, string groupByTag = null)
		{
			Command = string.IsNullOrEmpty(groupByTag)
				? $"count {filter.CreateFilterString()}"
				: $"count {filter.CreateFilterString()} group {groupByTag}";
		}

		/// <summary>
		/// Pass a filter expression to get a count of matching songs.
		/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#filter-syntax"/>
		/// Pass a <see cref="Tag"/> string to to group the results by a tag. A group with an empty value contains counts of matching songs which don’t have this group tag. It exists only if at least one such song is found.
		/// </summary>
		/// <param name="filter">filter expression</param>
		/// <param name="groupByTag">optional string tag</param>
		public Count(string filter, string groupByTag = null)
		{
			Command = string.IsNullOrEmpty(groupByTag) 
				? $"count {filter}" 
				: $"count {filter} group {groupByTag}";
		}

		public virtual int HandleResponse(IEnumerable<string> response)
		{
			if (response.ToList().IsErrorResponse())
			{
				return 0;
			}

			return Convert.ToInt32(response.First());
		}
	}
}
