﻿using System;
using Gtk;
using Global = Gtk.Global;
using Object = GObject.Object;

namespace GtkDemo
{
    /// <summary>
    /// Minimalist demo program demonstrating the GTK templating system
    /// </summary>
    public static class Program
    {
        #region Methods
        
        public static void Main(string[] args)
        {
            Global.Init();

            var mainWindow = new Window("MyWindow")
            {
                DefaultWidth = 300, 
                DefaultHeight = 200, 
                Child = new CompositeWidget(),
                [Window.DestroySignal] = (o, e) => Global.MainQuit()
            };
            
            mainWindow.ShowAll();
            Global.Main();
        }
        #endregion
    }
}
