using MpcCore.Contracts.Mpd.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MpcCore.Mpd.Filter
{
	public class Filter : IFilter
	{
		private List<string> _groupBy = new List<string>();
		private string _sortBy;
		private List<string> _matchers = new List<string>();

		public bool CaseSensitive { get; set; }

		public IFilter WithCaseSensitivity(bool enabled = false)
		{
			CaseSensitive = enabled;
			return this;
		}

		public IFilter SortResultByTag(string tagName)
		{
			_sortBy = tagName;
			return this;
		}

		public IFilter SortResultByTagDescending(string tagName)
		{
			_sortBy = $"-{tagName}";

			return this;
		}

		public IFilter SearchInFile(string path)
		{
			_matchers.Add($"(file == \\'{path}\\')");
			return this;
		}

		public IFilter SearchInDirectory(string path)
		{
			_matchers.Add($"(base \\'{path}\\')");
			return this;
		}

		public IFilter TagExists(string tagName)
		{
			_matchers.Add($"({tagName} != \\'\\')");
			return this;
		}

		public IFilter TagNotExists(string tagName)
		{
			_matchers.Add($"({tagName} == \\'\\')");
			return this;
		}

		public IFilter AnyTagContains(string value)
		{
			_matchers.Add($"(any == \\'{_escape(value)}\\')");
			return this;
		}

		public IFilter AnyTagNotContains(string value)
		{
			_matchers.Add($"(!(any == \\'{_escape(value)}\\'))");
			return this;
		}

		public IFilter TagContains(string tagName, string value)
		{
			_matchers.Add($"({tagName} contains \\'{_escape(value)}\\' )");
			return this;
		}

		public IFilter TagNotContains(string tagName, string value)
		{
			_matchers.Add($"(!({tagName} contains \\'{_escape(value)}\\'))");
			return this;
		}

		public IFilter TagValueIs(string tagName, string value)
		{
			_matchers.Add($"({tagName} == \\'{_escape(value)}\\')");
			return this;
		}

		public IFilter TagValueNotIs(string tagName, string value)
		{
			_matchers.Add($"({tagName} != \\'{_escape(value)}\\')");
			return this;
		}

		public IFilter TagMatches(string tagName, Regex regex)
		{
			_matchers.Add($"({tagName} =~ \\'{regex}\\')");
			return this;
		}

		public IFilter TagNotMatches(string tagName, Regex regex)
		{
			_matchers.Add($"({tagName} !~ \\'{regex}\\')");
			return this;
		}

		public IFilter ModifiedSince(DateTime dateTime)
		{
			_matchers.Add($"(modified-since '{dateTime.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}')");
			return this;
		}

		public IFilter NotModifiedSince(DateTime dateTime)
		{
			_matchers.Add($"(!(modified-since '{dateTime.ToString("o", System.Globalization.CultureInfo.InvariantCulture)}'))");
			return this;
		}

		public IFilter AudioFormatIs(int sampleRate, int bits, int channels)
		{
			_matchers.Add($"(AudioFormat == \\'{sampleRate}:{bits}:{channels}\\')");
			return this;
		}

		public IFilter AudioFormatMatches(Regex regex)
		{
			_matchers.Add($"(AudioFormat =~ \\'{regex}\\')");
			return this;
		}

		public string CreateFilterString()
		{
			var sb = new StringBuilder();
			sb.Append("\"");

			if (_matchers.Any())
			{
				if (_matchers.Count > 1)
				{
					sb.Append("(");
				}
				
				sb.Append(string.Join(" AND ", _matchers));
				
				if (_matchers.Count > 1)
				{
					sb.Append(")");
				}
			}
			
			sb.Append("\"");

			if (!string.IsNullOrEmpty(_sortBy))
			{
				sb.Append($" sort {_sortBy}");
			}

			return sb.ToString();
		}

		// TODO: Test escaping on non-standard tag values containing ', ", \ etc
		private string _escape(string str)
		{
			return str;
		}
	}
}
