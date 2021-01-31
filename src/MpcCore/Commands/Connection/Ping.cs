using MpcCore.Commands.Base;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// Does nothing but get an "OK" response
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class Ping : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public Ping()
		{
			Command = "ping";
		}
	}
}
