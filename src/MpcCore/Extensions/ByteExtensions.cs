using System.Linq;

namespace MpcCore.Extensions
{
	public static class ByteExtensions
	{
		public static byte[] Combine(byte[] first, byte[] second)
		{
			return first.Concat(second).ToArray();
		}
	}
}
