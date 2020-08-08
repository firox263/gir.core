using System;

namespace Gst
{
    public class Element : GObject.InitiallyUnowned
    {        
        internal Element(IntPtr handle) : base(handle) { }

        public Bus GetBus()
        {
            var ret = Sys.Element.get_bus(Handle);
            return Convert(ret, (r) => new Bus(r));
        }

        public void SetState(State state) 
            => Sys.Element.set_state(Handle, (Sys.State) state);
    }
}