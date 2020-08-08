using System;

namespace JavaScriptCore
{
    public class Context : GObject.Object
    {
        internal Context(IntPtr handle) : base(handle) { }

        public void Throw(string errorMessage) => Sys.Context.@throw(Handle, errorMessage);
        public void SetValue(string name, Value value) => Sys.Context.set_value(Handle, name, value.Handle);
        public Value GetValue(string name) => new Value(Sys.Context.get_value(Handle, name));
        public Value GetGlobalObject() => new Value(Sys.Context.get_global_object(Handle));
    }
}