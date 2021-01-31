# mpcCore

A .netstandard2 MPD client in C#.

Basically i needed something to talk to MPD for a raspberry pi project.
Mpc.NET is a great example, but seems to be dead. Since there is lots of time due to Corona,
programming is fun and .net Core is fun - here it is.

Keep in mind this is a really, _really_ early work in progress.

## Basic usage
Create a connection with ip and port of your mpd server and use to create a client instance.
Default values are `127.0.0.1` for local connection and the standard MPD port `6600`.

Connect to the server.

```csharp
var connection = new MpcCoreConnection("192.168.0.20", "6600");
var client = new MpcCoreClient(connection);
var connected = await client.ConnectAsync();
```

Send commands via `SendAsync()`. For example, load a playlist and start playing.
```csharp
await client.SendAsync(new Commands.Playlist.LoadPlaylist("awesomeplaylistname"));
await client.SendAsync(new Commands.Player.Play());
```

Each command returns a `IMpcCoreResponse<T>` with the raw command sent to MPD, a status object with error information and the result depending on the command.

## TODO
### Commands
* [x] Player
* [x] Queue
* [x] Playlist
* [x] Status
* [x] Option
* [x] Database
* [x] Sticker
* [ ] Connection commands
	* [ ] close
	* [ ] kill
	* [ ] password
	* [ ] ping
	* [ ] tagtypes
	* [ ] tagtypes disable
	* [ ] tagtypes enable
	* [ ] clear
	* [ ] all
* [ ] Mount commands
	* [ ] mount
	* [ ] unmount
	* [ ] listmounts
	* [ ] listneighbors
* [ ] Partition commands
	* [ ] partition
	* [ ] listpartitions
	* [ ] newpartition
	* [ ] delpartition
	* [ ] moveoutput
* [ ] Audio output devices
	* [ ] disableoutput
	* [ ] enableoutput
	* [ ] toggleoutput
	* [ ] outputs
	* [ ] outputset
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

#### Deprecated commands 
- Queue/playlist
- Database/list

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