using System;
using GObject;

namespace Gtk
{
    // AUTOGENERATED FILE - DO NOT MODIFY

    [Wrapper("GtkBin")]
    public partial class Bin : Container
    {
        protected internal new static GObject.Type GetGType() => new GObject.Type(Sys.Bin.get_type());

        #region Constructors
        protected internal Bin(IntPtr ptr) : base(ptr) { }
        protected internal Bin(params ConstructProp[] properties) : base(properties) { }
        #endregion Constructors
    }
}