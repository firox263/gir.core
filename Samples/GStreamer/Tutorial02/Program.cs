// Copyright (C) GStreamer developers
// Copyright (C) GirCore Developers 2020
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using System;
using GLib;
using GObject;
using Gst;

namespace Tutorial02
{
    /// <summary>
    /// Port of GStreamer Tutorial 02: Basic Concepts
    /// See https://gstreamer.freedesktop.org/documentation/tutorials/basic/concepts.html
    /// </summary>
    class Program
    {
        static int Main(string[] args)
        {
            // Initialise GStreamer
            Gst.Global.Init(args);

            // Create the elements
            Element source = ElementFactory.Make("videotestsrc", "source");
            Element sink = ElementFactory.Make("autovideosink", "sink");

            // Create the empty pipeline
            Bin pipeline = new Pipeline("test-pipeline");

            if (source == null || sink == null)
            {
                Console.WriteLine("Not all elements could be created.");
                return -1;
            }

            // Build the pipeline
            pipeline.AddMany(source, sink);
            if (!Element.Link(source, sink))
            {
                Console.WriteLine("Elements could not be linked");
                return -1;
            }

            // Modify the source's properties
            source["pattern"] = 0;

            // Start playing
            StateChangeReturn ret = pipeline.SetState(State.Playing);
            if (ret == StateChangeReturn.Failure)
            {
                Console.WriteLine("Unable to set the pipeline to the playing state.\n");
                return -1;
            }

            // Wait until error or EOS
            Bus bus = pipeline.GetBus();
            Message? msgReturn = bus.TimedPopFiltered(Gst.Constants.CLOCK_TIME_NONE, MessageType.Error | MessageType.Eos);

            // Parse message
            if (msgReturn.HasValue)
            {
                Message msg = msgReturn.Value;
                switch (msg.Type)
                {
                    case MessageType.Error:
                        msg.ParseError(out Error? err, out var debug);
                        Console.WriteLine($"Error received from element {msg.Src.Name}: {err?.Message}");
                        Console.WriteLine($"Debugging information: {debug ?? "none"}: {err?.Message}");
                        break;
                    case MessageType.Eos:
                        Console.WriteLine("End-Of-Stream reached.");
                        break;
                    default:
                        // We should not reach here because we only asked for Errors and EOS
                        Console.WriteLine("Unexpected message received.");
                        break;
                }
            }

            return 0;
        }
    }
}
