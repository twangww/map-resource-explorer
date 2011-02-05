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

        public static XmlEditor Instance
        {
            get
            {

                if (_instance == null)
                {
                    _instance = new XmlEditor();
                    _instance.tbXmlEditor.Text = string.Empty;
                }

                return _instance;
            }

        }

        public void SetXml(string xml)
        {
            _instance.tbXmlEditor.Text = xml;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //Save xml to file
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(tbXmlEditor.Text);
                }
            }

        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                string fileContents;
                using (StreamReader sr = new StreamReader(@fileName))
                {
                    fileContents = sr.ReadToEnd();
                }

                tbXmlEditor.Text = fileContents;
            }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbXmlEditor.Text);
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                tbXmlEditor.Text = Clipboard.GetText();
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbXmlEditor.Text);
            tbXmlEditor.Text = string.Empty;
        }

        private void toolStripButtonValidate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Validate not implemented");
        }

        private void toolStripButtonSaveToLibrary_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Save to library not implemented");
        }
    }
}
