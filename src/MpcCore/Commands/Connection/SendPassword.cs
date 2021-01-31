using MpcCore.Commands.Base;

namespace MpcCore.Commands.Connection
{
	/// <summary>
	/// This is used for authentication with the server with the plaintext password.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#connection-settings"/>
	/// </summary>
	public class SendPassword : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="password">your password</param>
		public SendPassword(string password)
		{
			Command = $"password {password}";
		}
	}
}
