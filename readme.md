# mpcCore

A .netstandard2 [MPD](https://www.musicpd.org/) client in C#.

![.NET Build](https://github.com/svanelten/mpcCore/workflows/.NET%20Build/badge.svg?branch=main) ![.NET Test](https://github.com/svanelten/mpcCore/workflows/.NET%20Test/badge.svg)

Basically i needed something to talk to MPD for a raspberry pi project.
Mpc.NET is a great example, but seems to be dead. Since there is lots of time due to Corona,
programming is fun and .net Core is fun - here it is.

Keep in mind this is a really, _really_ early work in progress.

## Basic usage
### Connecting
Create a connection with ip and port of your mpd server and use to create a client instance.
Default values are `127.0.0.1` for local connection and the standard MPD port `6600`.

Connect to the server.

```csharp
var connection = new MpcCoreConnection("192.168.0.20", "6600");
var client = new MpcCoreClient(connection);
var connected = await client.ConnectAsync();
```

### Sending commands
Send commands via `SendAsync()`. For example, load a playlist and start playing.
```csharp
await client.SendAsync(new Commands.Playlist.LoadPlaylist("awesomeplaylistname"));
await client.SendAsync(new Commands.Player.Play());
```
Each command returns a `IMpcCoreResponse<T>` with the raw command sent to MPD, a status object with error information and the result depending on the command.

### Disconnecting
When you are done, disconnect.
```csharp
await client.DisconnectAsync();
```

## TODO
### Commands
The following commands are currently implemented. Grouping follows the [MPD protocol documentation](https://www.musicpd.org/doc/html/protocol.html).

* [x] Player
* [x] Queue
* [x] Playlist
* [x] Status
* [x] Option
* [x] Database
* [x] Sticker
* [x] Partition
* [x] Audio output devices
* [x] Connection
* [ ] Mount commands
	* [ ] mount
	* [ ] unmount
	* [ ] listmounts
	* [ ] listneighbors
* [ ] Reflection
	* [ ] config
	* [ ] commands
	* [ ] notcommands
	* [ ] urlhandlers
	* [ ] decoders
* [ ] Client to client commands
	* [ ] subscribe
	* [ ] unsubscribe
	* [ ] channels
	* [ ] readmessages
	* [ ] sendmessage

#### Commands that won't be implemented
- Queue/playlist - _deprecated_
- Database/list - _deprecated_
- Connection/kill - _not recommended_
- Connection/close - _not recommended_

### Functionality
* [ ] Command list handling
* [ ] More error handling
* [ ] Checking string escaping edgecases
* [x] Binary response handling
* [x] Filter handling / construction
* [ ] Audio format/settings
* [ ] Authentication
* [ ] Nuget export
* [ ] i18n?

### Unittests
* [ ] Find best method to do this
* [ ] Write some tests

### Documentation
* [ ] Code documentation
* [ ] Real readme
* [ ] Usage examples

### Cleanup
* [ ] Connection errors handling
* [ ] Response parsing

## Roadmap
Depending on the level of energy and the endurance of corona, the following things will be done

- finishing commands
- cli client
- test coverage
- REST api