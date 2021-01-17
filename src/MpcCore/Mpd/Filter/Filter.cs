using MpcCore.Contracts.Mpd.Filter;

namespace MpcCore.Mpd.Filter
{
	public class Filter : IFilter
	{
		private string _filterString = string.Empty;

		public Filter(string str = "")
		{
			_filterString = str;
		}

		public string CreateFilterString()
		{
			return $"NOTIMPLEMENTED: {_filterString}";
		}
	}
}
