using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Cobalt.Content;

public static class NativeLib
{
	private const string LibName = "Cobalt.Content.Native";
	private const CallingConvention LibCallConv = CallingConvention.StdCall;

	public static unsafe byte[] TestAThing(byte[] input)
	{
		var ret = new byte[input.Length];

		fixed (byte* inptr = input)
		fixed (byte* outptr = ret)
			Check(TestAThing(inptr, outptr, (uint)input.Length));

		return ret;
	}

	[DllImport(LibName, EntryPoint = "test_a_thing", CallingConvention = LibCallConv)]
	private static unsafe extern StatusCode TestAThing(byte* a, byte* b, nuint size);

	private enum StatusCode : uint
	{
		Success,

		Fail,

		InputTooLarge,
		OutputBufferTooSmall,
		InvalidData,
		InvalidArgument,
		NotFound,

		Unsupported,
	}

	private static void Check(StatusCode status, string? paramName = null, string? message = null)
	{
		switch (status)
		{
		case StatusCode.Success:
			return;

		case StatusCode.Fail:
			throw new Exception(message);

		case StatusCode.InputTooLarge:
			throw new ArgumentException(paramName: paramName, message: message);

		case StatusCode.OutputBufferTooSmall:
			throw new ArgumentException(paramName: paramName, message: message);

		case StatusCode.InvalidData:
			throw new InvalidDataException(message);

		case StatusCode.InvalidArgument:
			throw new ArgumentException(paramName: paramName, message: message);

		case StatusCode.Unsupported:
			throw new NotSupportedException(message: message);

		default:
			throw new Exception(message ?? "An unknown error occurred in a native library.");
		}
	}
}