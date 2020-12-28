using System;
using System.Runtime.InteropServices;

namespace Gst
{
    public static partial class Global
    {
        public static uint ResourceErrorQuark
            => Native.resource_error_quark();
        
        public static uint StreamErrorQuark
            => Native.stream_error_quark();
        
        public static uint LibraryErrorQuark
            => Native.library_error_quark();
        
        public static uint CoreErrorQuark
            => Native.core_error_quark();

        public static void Init()
            => Init(Array.Empty<string>());
        
        public static void Init(string[] args)
        {
            var argc = args.Length;
            IntPtr[] argv = new IntPtr[argc];

            // Convert string array to IntPtr array
            for (var i = 0; i < argc; i++)
            {
                argv[i] = Marshal.StringToHGlobalAnsi(args[i]);
            }

            IntPtr argvPtr = argv.Length > 0 ? argv[0] : IntPtr.Zero;
            Global.Native.init(ref argc, ref argvPtr);
            
            // Free strings
            foreach (IntPtr ptr in argv)
                Marshal.FreeHGlobal(ptr);
        }
    }
}
