using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using OSGeo.MapGuide;

using Autodesk.Gis.Map.Platform;
using Autodesk.Gis.Map.Platform.Utils;

namespace MapResourceExplorer.Model
{
    class ResourceManager
    {
        #region Singleton
        private static ResourceManager _instance = null;

        private ResourceManager()
        {

        }

        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }

                return _instance;
            }

        }

        #endregion


        public ArrayList GetResourceAllTypes()
        {
            ArrayList resourceTypes = new ArrayList();
            resourceTypes.Add(MgResourceType.FeatureSource);
            resourceTypes.Add(MgResourceType.LayerDefinition);
            resourceTypes.Add(MgResourceType.SymbolDefinition);
            return resourceTypes;
        }

    }
}
