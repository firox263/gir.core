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

namespace Tutorial03
{
    /// <summary>
    /// Port of GStreamer Tutorial 03: Dynamic pipelines
    /// See https://gstreamer.freedesktop.org/documentation/tutorials/basic/dynamic-pipelines.html
    /// </summary>
    class Program
    {
        private static Element pipeline;
        private static Element source;
        private static Element convert;
        private static Element resample;
        private static Element sink;
        
        static int Main(string[] args)
        {
            // Initialise GStreamer
            Gst.Global.Init(args);

            // Create the elements
            source = ElementFactory.Make("uridecodebin", "source");
            convert = ElementFactory.Make("audioconvert", "convert");
            resample = ElementFactory.Make("audioresample", "resample");
            sink = ElementFactory.Make("autoaudiosink", "sink");

            // Create the empty pipeline
            pipeline = new Pipeline("test-pipeline");

            if (pipeline == null || source == null || convert == null || resample == null || sink == null)
            {
                Console.WriteLine("Not all elements could be created.");
                return -1;
            }
            
            // Build the pipeline. Note that we are NOT linking the source at this
            // point. We will do it later.
            (pipeline as Bin).AddMany(source, convert, resample, sink);
            if (!Element.LinkMany(convert, resample, sink))
            {
                Console.WriteLine("Elements could not be linked.");
                return -1;
            }
            
            // Set the URI to play
            source["uri"] = "https://www.freedesktop.org/software/gstreamer-sdk/data/media/sintel_trailer-480p.webm";
            
            // Connect to the 'pad-added' (OnPadAdded) signal
            source.OnPadAdded += PadAddedHandler;
            
            // Start playing
            StateChangeReturn ret = pipeline.SetState(State.Playing);
            if (ret == StateChangeReturn.Failure)
            {
                Console.WriteLine("Unable to set the pipeline to the playing state.");
                return -1;
            }

            Bus bus = pipeline.GetBus();
            
            // TODO: Do Loop
            

            return 0;
        }

        static void PadAddedHandler(Element src, Element.PadAddedSignalArgs args)
        {
            // TODO: OnPadAdded
        }
    }
}
