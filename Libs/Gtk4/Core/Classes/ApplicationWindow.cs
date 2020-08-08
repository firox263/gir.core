using System;
using System.Reflection;

namespace Gtk
{
    public class ApplicationWindow : Window
    {
        public ApplicationWindow(Application application) : this(Sys.ApplicationWindow.@new(application.Handle)) {}
        internal ApplicationWindow(IntPtr handle) : base(handle) {}
    }
}