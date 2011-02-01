using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using OSGeo.MapGuide;

using Autodesk.Gis.Map.Platform;
using Autodesk.Gis.Map.Platform.Utils;
using System.Xml;

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


        private MgResourceService _resourceService;
        public MgResourceService ResourceService
        {
            get
            {
                if (_resourceService == null)
                {
                    _resourceService = AcMapServiceFactory.GetService(MgServiceType.ResourceService) as MgResourceService;
                }
                return _resourceService;
            }
        }

        /// <summary>
        /// Get valid resource type in Map3D
        /// --------------------------------------
        /// FeatureSource Contains the required parameters for connecting to a geospatial feature source 
        /// LayerDefinition Contains the required parameters for displaying and styling a layer. Layers can be drawing layers, vector layers, or grid (raster) layers. 
        /// SymbolDefinition Defines a symbol to be displayed on a map. 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetResourceAllTypes()
        {
            Dictionary<string, string> resourceTypes = new Dictionary<string, string>();
            resourceTypes.Add(MgResourceType.FeatureSource, "Contains the required parameters for connecting to a geospatial feature source.");
            resourceTypes.Add(MgResourceType.LayerDefinition, "Contains the required parameters for displaying and styling a layer. Layers can be drawing layers, vector layers, or grid (raster) layers.");
            resourceTypes.Add(MgResourceType.SymbolDefinition, "Defines a symbol to be displayed on a map.");
            return resourceTypes;
        }


        public Dictionary<string, string> GetResourcesByType(string resourceType)
        {
            //TODO:
            if (!IsValidMap3DResourceType(resourceType))
            {
                throw new ApplicationException("unspported resource type by Map3D");
            }

            Dictionary<string, string> resources = new Dictionary<string, string>();

            MgResourceIdentifier rootResId = new MgResourceIdentifier(@"Library://");
            MgByteReader reader = ResourceService.EnumerateResources(rootResId, -1, resourceType.ToString());

            //Convert to string 
            String resStr = reader.ToString();

            //Load into XML document so we can parse and get the names of the maps
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resStr);

            //let's extract the map names and list them        
            XmlNodeList resIdNodeList;
            XmlElement root = doc.DocumentElement;
            resIdNodeList = root.SelectNodes("//ResourceId");
            int resCount = resIdNodeList.Count;
            for (int i = 0; i < resCount; i++)
            {
                XmlNode resIdNode = resIdNodeList.Item(i);
                String resId = resIdNode.InnerText;
                int index1 = resId.LastIndexOf('/') + 1;
                int index2 = resId.IndexOf(resourceType) - 2;
                int length = index2 - index1 + 1;
                string resName = resId.Substring(index1, length);
                resources.Add(resName, resId);

            }


            return resources;
        }

        /// <summary>
        /// check resource type is valid in Map 3D or not
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        private bool IsValidMap3DResourceType(string resourceType)
        {
            if (resourceType == MgResourceType.FeatureSource || resourceType == MgResourceType.LayerDefinition || resourceType == MgResourceType.SymbolDefinition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string GetResourceContent(string resourceId)
        {
            MgResourceIdentifier resId = new MgResourceIdentifier(resourceId);
            MgByteReader reader = ResourceService.GetResourceContent(resId);
            return reader.ToString();
        }

        public string GetResourceHeader(string resourceId)
        {
            MgResourceIdentifier resId = new MgResourceIdentifier(resourceId);
            MgByteReader reader = ResourceService.GetResourceHeader(resId);
            return reader.ToString();
        }

        
    }
}
