﻿using System;
using System.Runtime.InteropServices;
using Gst;
using Object = GObject.Object;

namespace GstVideo
{
    public partial interface Navigation
    {
        public void SendMouseEvent(string @event, int button, double x, double y)
        {
            // TODO: Can we do this?
            Object obj = (Object) this;
            Native.send_mouse_event(obj.Handle, @event, button, x, y);
        }

        public void SendCommand(NavigationCommand command)
        {
            Object obj = (Object) this;
            Native.send_command(obj.Handle, command);
        }

        public static NavigationMessageType MessageGetType(Message message)
        {
            // Marshal message structure
            IntPtr messagePtr = Marshal.AllocHGlobal(Marshal.SizeOf(message));
            Marshal.StructureToPtr(message, messagePtr, false);

            // Native Call
            var result = Global.Native.navigation_message_get_type(messagePtr);

            // Update message structure (Not necessary?)
            Marshal.PtrToStructure(messagePtr, message);

            return result;
        }
    }
}
