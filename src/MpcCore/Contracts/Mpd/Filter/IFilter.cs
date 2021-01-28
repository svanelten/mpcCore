using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MpcCore.Contracts.Mpd.Filter
{
	public interface IFilter
	{
		bool CaseSensitive { get; set; }

		IFilter AnyTagContains(string value);
		IFilter AnyTagNotContains(string value);
		IFilter AudioFormatIs(int sampleRate, int bits, int channels);
		IFilter AudioFormatMatches(Regex regex);
		string CreateFilterString();
		IFilter ModifiedSince(DateTime dateTime);
		IFilter NotModifiedSince(DateTime dateTime);
		IFilter SearchInDirectory(string path);
		IFilter SearchInFile(string path);
		IFilter SortResultByTag(string tagName);
		IFilter SortResultByTagDescending(string tagName);
		IFilter TagContains(string tagName, string value);
		IFilter TagExists(string tagName);
		IFilter TagMatches(string tagName, Regex regex);
		IFilter TagNotContains(string tagName, string value);
		IFilter TagNotExists(string tagName);
		IFilter TagNotMatches(string tagName, Regex regex);
		IFilter TagValueIs(string tagName, string value);
		IFilter TagValueNotIs(string tagName, string value);
		IFilter WithCaseSensitivity(bool enabled = false);
	}
}