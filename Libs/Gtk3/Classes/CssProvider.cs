using GObject;
using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Gtk
{
    public partial class CssProvider
    {
        public CssProvider() {}

        public override string ToString()
            => Marshal.PtrToStringAnsi(Native.to_string(Handle)) ?? string.Empty;

        public bool LoadFromData(string data, out GLib.Error? error)
        {
            // Get as ANSI characters
            byte[] buf = Encoding.ASCII.GetBytes(data);

            // Unmanaged Call
            bool result = Native.load_from_data(Handle, buf, buf.Length, out var errPtr);

            // Console.WriteLine(ToString()); <-- Testing

            // Handle Error
            if (errPtr == IntPtr.Zero)
                error = null;
            else
                error = Marshal.PtrToStructure<GLib.Error>(errPtr);

            // Return
            return result;
        }
    }
}
