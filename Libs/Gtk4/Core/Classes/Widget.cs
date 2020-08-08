using System;
using GObject;

namespace Gtk
{
    public class Widget : GObject.InitiallyUnowned
    {
        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal Widget(IntPtr handle) : base(handle) 
        {
            WidthRequest = PropertyOfInt("width-request");
            HeightRequest = PropertyOfInt("height-request");
        }

        public void Show() => Sys.Widget.show(Handle);
    }
}