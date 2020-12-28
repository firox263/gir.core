using System;
using Gst;

namespace Sample
{
    public class Gst
    {
        public static void Play()
        {
            Console.WriteLine("Starting to play tears of steal. Please wait while file is being loaded...");
            
            Element ret = Parse.Launch("playbin uri=playbin uri=http://ftp.halifax.rwth-aachen.de/blender/demo/movies/ToS/tears_of_steel_720p.mov");
            if (ret == null)
            {
                Console.WriteLine("Failed to launch playbin. Have you installed gst-plugins?");
                return;   
            }
            ret.SetState(State.Playing);
            Bus bus = ret.GetBus();
            bus.WaitForEndOrError();
            ret.SetState(State.@null);
        }
    }
}
