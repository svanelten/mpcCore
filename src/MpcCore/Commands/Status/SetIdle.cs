using MpcCore.Contracts;
using MpcCore.Response;
using System.Collections.Generic;
using System.Linq;

namespace MpcCore.Commands.Status
{
	/// <summary>
	/// Waits until there is a noteworthy change in one or more of MPD’s subsystems. 
	/// As soon as there is one, it lists all changed systems.
	/// Change events accumulate, even while the connection is not in “idle” mode; no events get lost while the client is doing something else with the connection. #
	/// If an event had already occurred since the last call, the new idle command will return immediately.
	/// While a client is waiting for idle results, the server disables timeouts, allowing a client to wait for events as long as mpd runs.
	/// The idle command can be canceled by sending the command noidle (no other commands are allowed). 
	/// MPD will then leave idle mode and print results immediately; might be empty at this time.
	/// If the optional list for subsystems is used, MPD will only send notifications when something changed in one of the specified subsytems.
	/// 
	/// Available subsystems:
	///		database: the item database has been modified after update.
	///		update: a database update has started or finished.If the database was modified during the update, the database event is also emitted.
	///		stored_playlist: a stored playlist has been modified, renamed, created or deleted
	///		playlist: the queue (i.e.the current playlist) has been modified
	///		player: the player has been started, stopped or seeked
	///		mixer: the volume has been changed
	///		output: an audio output has been added, removed or modified(e.g.renamed, enabled or disabled)
	/// 	options: options like repeat, random, crossfade, replay gain
	/// 	partition: a partition was added, removed or changed
	/// 	sticker: the sticker database has been modified.
	/// 	subscription: a client has subscribed or unsubscribed to a channel
	/// 	message: a message was received on a channel this client is subscribed to; this event is only emitted when the queue is empty
	/// 	neighbor: a neighbor was found or lost
	/// 	mount: the mount list has changed
	/// <seealso cref="https://www.musicpd.org/doc/html/protocol.html#querying-mpd-s-status"/>
	/// </summary>
	public class SetIdle : IMpcCoreCommand<IEnumerable<string>>
	{
		/// <summary>
		/// Internal command string
		/// </summary>
		public string Command { get; internal set; }

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="subsystems">optional list of mpd subsystems you want to get notified when changing</param>
		public SetIdle(IEnumerable<string> subsystems = null)
		{
			Command = subsystems != null && subsystems.Any()
				? $"idle {(string.Join(" ", subsystems))}"
				: "idle";
		}

		public IEnumerable<string> HandleResponse(IMpdResponse response)
		{
			var parser = new ResponseParser(response);
			return parser.GetChangedSystems();
		}
	}
}
