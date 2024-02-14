using System;
using System.Runtime.InteropServices;

namespace MathexprProcessorCs
{
    public class MathexprProcessor
    {
        static double IntPtrToDouble(IntPtr ptr)
        {
            var bytes = new byte[sizeof(double)];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Marshal.ReadByte(ptr, i);
            }
            return BitConverter.ToDouble(bytes, 0);
        }

        [DllImport("MathexprProcessorCpp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        static extern int process_mathexpr([MarshalAs(UnmanagedType.LPStr)] string expr, IntPtr value);

        public static double Process(string expression)
        {
            IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(double));
            if (0 != process_mathexpr(expression, valuePtr))
            {
                Marshal.FreeHGlobal(valuePtr);
                throw new FormatException();
            }
            double value = IntPtrToDouble(valuePtr);
            Marshal.FreeHGlobal(valuePtr);
            return value;
        }

    }
}
