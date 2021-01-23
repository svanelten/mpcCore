using System.Threading.Tasks;

namespace MpcCore.Contracts.Mpd
{
	public interface IAlbumArt
	{
		byte[] Bytes { get; }
		
		string ItemPath { get; }

		string MimeType { get; }

		bool HasContent { get; }

		Task SaveAsFile(string path);
	}
}