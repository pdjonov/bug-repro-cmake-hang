using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Cobalt.Content.Build.ScriptHelpers;

using Serialization;

public static class Path
{
	public static string? GetDirectoryName(string path)
	{
		ArgumentNullException.ThrowIfNull(path);
		if (!ContentLocator.IsValidSourcePath(path))
			throw new ArgumentException("Not a valid source path.");

		var lastSep = path.LastIndexOf('/');
		if (lastSep > 0 && lastSep == path.Length - 1)
			lastSep = path.LastIndexOf('/', lastSep - 1);

		if (lastSep == -1)
			return null;

		Debug.Assert(lastSep != 0); //IsValidSourcePath checks for leading /, repeated here in case of changes

		return path.Substring(0, lastSep);
	}

	public static string Combine(string a, string b)
	{
		ArgumentNullException.ThrowIfNull(a);
		ArgumentNullException.ThrowIfNull(b);
		if (!ContentLocator.IsValidSourcePath(a) || !ContentLocator.IsValidSourcePath(b))
			throw new ArgumentException("Not a valid source path.");

		Debug.Assert(!b.StartsWith('/')); //IsValidSourcePath checks for leading /, repeated here in case of changes

		return a.EndsWith('/') ?
			string.Concat(a, b) :
			string.Concat(a, "/", b);
	}

	public static string Combine(params string[] parts)
	{
		if (parts == null || parts.Contains(null))
			throw new ArgumentNullException(nameof(parts));
		if (parts.Any(p => !ContentLocator.IsValidSourcePath(p)))
			throw new ArgumentException("Not a valid source path.");

		var ret = new StringBuilder();

		foreach (var p in parts)
		{
			if (p == "")
				continue;

			Debug.Assert(!p.StartsWith('/')); //IsValidSourcePath checks for leading /, repeated here in case of changes

			if (ret.Length != 0)
				ret.Append('/');

			ret.Append(p);

			if (ret[ret.Length - 1] == '/')
				ret.Length--;
		}

		return ret.ToString();
	}
}