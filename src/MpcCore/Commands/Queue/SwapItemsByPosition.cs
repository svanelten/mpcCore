using MpcCore.Commands.Base;

namespace MpcCore.Commands.Queue
{
	/// <summary>
	/// Swaps the position of two items in the queue by position reference.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#the-queue"/>
	/// </summary>
	public class SwapItemsByPosition : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="itemPositionA">position of item A</param>
		/// <param name="itemPositionB">position of item B</param>
		public SwapItemsByPosition(int itemPositionA, int itemPositionB)
		{
			Command = $"swap {itemPositionA} {itemPositionB}";
		}
	}
}
