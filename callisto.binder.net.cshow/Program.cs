using System;

namespace Callisto.Binder.Net.CShow
{
	class Program
	{
		static unsafe void Main(string[] args)
		{
            //ShowTypeEnumerate(typeof(NativeWrap));

            Console.WriteLine($"Native mul(2, 5) = {NativeExampleLib2.NativeMul(2.0f, 5.0f)}");

            Console.WriteLine($"Native pow(2, 5) = {NativeExampleLib2.NativeWrap.NativePow(2.0f, 5)}");

            var length = 10;
            var copyLength = 7;
            var srcBytes = new byte[length];
            var dstBytes = new byte[length];

            for (int i = 0; i < srcBytes.Length; i++)
            {
                srcBytes[i] = (byte)(i);
                dstBytes[i] = (byte)(i * 2);
            }

            fixed (byte* srcPtr = srcBytes)
            fixed (byte* dstPtr = dstBytes)
            {
                NativeExampleLib2.NativeWrap.NativeClass.CopyBytes(
                    (IntPtr)srcPtr,
                    (IntPtr)dstPtr,
                    IntPtr.Add(IntPtr.Zero, copyLength)
                );
            }
            foreach (var src in srcBytes) Console.Write($"{src} ");
            Console.WriteLine();
            foreach (var dst in dstBytes) Console.Write($"{dst} ");
            Console.WriteLine();

            var cPoint = NativeExampleLib2.NativeWrap.NativeClass.Point3fClass.ConstructPoint3f(7.4f, 6.3f, 1.2f);
            Console.WriteLine($"Native construct point:({cPoint.x}, {cPoint.y}, {cPoint.z})");
        }
	}
}
