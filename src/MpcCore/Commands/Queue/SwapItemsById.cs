using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Swaps the position of two items referenced by id in the queue.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class SwapItemsById : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="idA">id of item A</param>
		/// <param name="idB">id of item B</param>
		public SwapItemsById(int idA, int idB)
		{
			Command = $"swapid {idA} {idB}";
		}
	}
}
