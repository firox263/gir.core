using System;

namespace WebKit2
{
    public class WebInspector : GObject.InitiallyUnowned
    {
        internal WebInspector(IntPtr handle) : base(handle) { }

        public void Show() => Sys.WebInspector.show(Handle);  
    }
}