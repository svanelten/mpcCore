using MpcCore.Contracts.Mpd;

namespace MpcCore.Mpd
{
	/// <summary>
	/// A “storage” provides access to files in a directory tree. 
	/// The most basic storage plugin is the “local” storage plugin which accesses the local file system, and there are plugins to access NFS and SMB servers.
	/// Multiple storages can be “mounted” together, similar to the mount command on many operating systems, but without cooperation from the kernel.
	/// No superuser privileges are necessary, because this mapping exists only inside the MPD process.
	/// 
	/// This class represents one of these mounts as configured in MPD.
	/// </summary>
	public class Mount : IMount
	{
		/// <summary>
		/// The name of this mount
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The storage path for this mount.
		/// Might be a directory on the server running MPD or on a file server
		/// </summary>
		public string Path { get; set; }

		/// <summary>
		/// True if the Path is not the same server as the MPD 
		/// </summary>
		public bool IsOnRemoteServer => !Path.StartsWith("/");
	}
}
