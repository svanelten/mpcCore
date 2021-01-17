using MpcCore.Contracts;
using MpcCore.Contracts.Mpd;
using MpcCore.Response;
using System.Collections.Generic;

namespace MpcCore.Commands.Base
{
	/// <summary>
	/// Base command for commands that return binary objects
	/// </summary>
	public abstract class BinaryResponseCommandBase : IMpcCoreCommand<IAlbumArt>
	{
		public string Command { get; internal set; }

		public virtual IAlbumArt HandleResponse(IEnumerable<string> response)
		{
			var parser = new ResponseParser(response);

			// TODO binary implementation
			// TODO image construction
			
			return null;
		}
	}
}
