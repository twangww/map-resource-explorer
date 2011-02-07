using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using MapResourceExplorer.UI;
using Autodesk.Gis.Map.Platform.Utils;
using MapResourceExplorer.Model;

namespace MapResourceExplorer
{
    /// <summary>
    /// Entry point class of this assembly.
    /// </summary>
    public class AppEntry : IExtensionApplication
    {
        public void Initialize()
        {
            Util.PrintLn("MapResourceExplore application initialized.");

            // Prompt the commands this assembly offers
            Commands cmd = new Commands();
            cmd.CmdListCommand();

            cmd.ResourceExplorerCommand();
            //Register Events;
            cmd.RegisterEvents();
        }

        /// <summary>
        /// .Net assembly can't be unloaded from AutoCAD like ARX libraries.
        /// So this method wouldn't be invoked until AutoCAD exits.
        /// You're not encouraged to do anything in this method.
        /// </summary>
        public void Terminate()
        {
        }
    }

    public class Commands
    {


        [CommandMethod("CmdList")]
        public void CmdListCommand()
        {
            Util.PrintLn("PROMPT: MapResourceExplore commands:");
            Util.PrintLn("ShowResourceExplorer");
            Util.PrintLn("RegisterEvents");
        }

        [CommandMethod("ShowResourceExplorer")]
        public void ResourceExplorerCommand()
        {
            ResourceExplorerPalette.Instance.Show();
        }

        [CommandMethod("RegisterEvents")]
        public void RegisterEvents()
        {
            EventManager.Instance.RegisterEvents();
            
        }

    
    }
}
