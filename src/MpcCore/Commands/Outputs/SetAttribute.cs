using MpcCore.Commands.Base;

namespace MpcCore.Commands.Outputs
{
	/// <summary>
	/// Set a runtime attribute. These are specific to the output plugin, and supported values are usually printed in the <see cref="GetOutputDeviceList"/> response.
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#audio-output-devices"/>
	/// </summary>
	public class SetAttribute : SimpleCommandBase
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="id">output id</param>
		/// <param name="id">parameter name</param>
		/// <param name="id">parameter value</param>
		public SetAttribute(int id, string name, string value)
		{
			Command = $"outputset {id} {name} {value}";
		}
	}
}
