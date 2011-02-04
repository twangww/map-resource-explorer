using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MapResourceExplorer.UI
{
    public partial class XmlEditor : Form
    {
        private XmlEditor()
        {
            InitializeComponent();
        }

        private static XmlEditor _instance;

        public static XmlEditor Instance()
        {

            if (_instance == null)
            {
                _instance = new XmlEditor();
                _instance.tbXmlEditor.Text = string.Empty;
            }

            return _instance;

        }

        public void SetXml(string xml)
        {
            _instance.tbXmlEditor.Text = xml;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //Save xml to file
        }
    }
}
