using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Search through the queue and return all elements where the searchterm matches the given tag.
	/// Returns partial matches unless "strict matching" is true
	/// </summary>
	public class Search : QueryQueueCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="tag">tag that is searched, eg "Title", <see cref=" MpcCore.Mpd.Tag"/></param>
		/// <param name="search">search term</param>
		/// <param name="strictMatching">if true, only exact matches are returned</param>
		public Search(string tag, string search, bool strictMatching = false)
		{
			Command = strictMatching
				? $"playlistfind {tag} {search}"
				: $"playlistsearch {tag} {search}";
		}
	}
}
