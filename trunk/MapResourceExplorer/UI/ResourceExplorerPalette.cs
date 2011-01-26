﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.Windows;

namespace MapResourceExplorer.UI
{
    internal class ResourceExplorerPalette
    {
        private static string PaletteName = @"Resource Explorer";

        private PaletteSet _paletteSet;
        private Panel _panel;

        /// <summary>
        /// Using Singleton pattern to make sure there'll be only one instance of this class.
        /// </summary>

        private static ResourceExplorerPalette _instance;
        public static ResourceExplorerPalette Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceExplorerPalette();

                } return _instance;
            }
        }

        private ResourceExplorerPalette()
        {
            _paletteSet = new PaletteSet(PaletteName);

            //Set the properties of paletteSet
            _paletteSet.Style = PaletteSetStyles.NameEditable |
                PaletteSetStyles.ShowPropertiesMenu |
                PaletteSetStyles.ShowAutoHideButton |
                PaletteSetStyles.UsePaletteNameAsTitleForSingle |
                PaletteSetStyles.Snappable |
                PaletteSetStyles.ShowCloseButton;

            _panel = new Panel();
            _paletteSet.Add("Resource Explorer", _panel);
        }


        public PaletteSet PaletteSet
        {
            get
            {
                return _paletteSet;
            }

        }


        private ExplorerForm explorerForm
        {
            get
            {
                return _panel.Child;
            }

        }

        /// <summary>
        /// Show or hide the UI.
        /// </summary>
        public void Show(bool visible)
        {
            _paletteSet.Visible = visible;
            _paletteSet.KeepFocus = visible;

            if (visible)
            {
                explorerForm.ForceRefresh();
            }
        }

    }
}