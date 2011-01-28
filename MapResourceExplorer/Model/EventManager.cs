using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using MapResourceExplorer.UI;

namespace MapResourceExplorer.Model
{
    class EventManager
    {

        #region Singleton
        private static EventManager _instance;

        private EventManager()
        {

        }

        public static EventManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventManager();
                }

                return _instance;
            }
            
        }

        #endregion 

        /// <summary>
        /// 
        /// </summary>
        public void RegisterEvents()
        {
            Application.DocumentManager.DocumentActivated += new DocumentCollectionEventHandler(DocumentManager_DocumentActivated);
            
        }
        /// <summary>
        /// Refresh resource tree when active document is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DocumentManager_DocumentActivated(object sender, DocumentCollectionEventArgs e)
        {
            ResourceExplorerPalette.Instance.ExplorerForm.ForceRefresh();
        }
    }

}
