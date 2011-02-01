using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapResourceExplorer.UI
{
    public partial class XmlEditor : Form
    {
        private XmlEditor()
        {
            InitializeComponent();
        }

        private static XmlEditor _instance;

        public static XmlEditor Instance(string xml)
        {

            if (_instance == null)
            {
                _instance = new XmlEditor();
                _instance.tbXmlEditor.Text = xml;
            }

            return _instance;

        }
    }
}
