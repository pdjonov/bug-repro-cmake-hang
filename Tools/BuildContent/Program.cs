using System;
using System.IO;
using System.Threading.Tasks;

namespace Cobalt.Content.Build;

public class Program
{
	private static void Main(string[] args)
	{
		var tested = NativeLib.TestAThing([3, 2, 1, 0]);
		Console.WriteLine(string.Join(", ", tested));
	}
}
