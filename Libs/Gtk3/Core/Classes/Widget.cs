using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Widget : GObject.InitiallyUnowned, IWidget
    {
        private Builder? builder;

        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal Widget(string template, string obj, Assembly assembly) : this(new Builder(template, assembly), obj) {}

        internal Widget(Builder builder, string obj) : this(builder.GetObject(obj))
        {
            this.builder = builder;
            builder.Connect(this);
        }

        internal Widget(IntPtr handle) : base(handle) 
        {
            WidthRequest = PropertyOfInt("width-request");
            HeightRequest = PropertyOfInt("height-request");
        }

        public void Show() => Sys.Widget.show(Handle);
        public void ShowAll() => Sys.Widget.show_all(Handle);
    }
}