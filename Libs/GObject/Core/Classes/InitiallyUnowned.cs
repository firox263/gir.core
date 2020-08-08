using System;

namespace GObject
{
    [Wrapper("GInitiallyUnowned")]
    public class InitiallyUnowned : Object
    {
        protected InitiallyUnowned() {}
        protected InitiallyUnowned(IntPtr handle) : base(handle) {}

        protected override void Initialize()
        {
            base.Initialize();
            Sys.Object.ref_sink(Handle);
        }
    }
}