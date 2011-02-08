using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MapResourceExplorer.Model;
using System.Xml;
using System.Xml.Schema;

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

        private string _currentResourceId;
        public string CurrentResourceId
        {
            get
            {
                return _currentResourceId;
            }
            set
            {
                _currentResourceId = value;
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
            //MessageBox.Show("Validate not implemented");
            //string errMsg = string.Empty;
            //bool isValid = ResourceManager.Instance.ValidateResource(this.CurrentResourceId, tbXmlEditor.Text,ref errMsg);
            //if (isValid == false)
            //{
            //    MessageBox.Show("XML is not valid.\n"+errMsg);
            //}
            //else
            //{
            //    MessageBox.Show("XML is valid.");
            //}

            if (!xmlValidated)
            {
                string xsdPath = ResourceManager.Instance.GetSchemaFilePath(this.CurrentResourceId);
                this.XmlValidate(xsdPath, tbXmlEditor.Text);
            }
        }

        private void toolStripButtonSaveToLibrary_Click(object sender, EventArgs e)
        {
            if (!xmlValidated)
            {
                //TODO: Get xsd path
                string xsdPath = ResourceManager.Instance.GetSchemaFilePath(this.CurrentResourceId);
                this.XmlValidate(xsdPath, tbXmlEditor.Text);
            }

            if (xmlValid)
            {
                ResourceManager.Instance.SetResourceContent(this.CurrentResourceId, tbXmlEditor.Text);
                MessageBox.Show("Resource Content is updated into Library");
            }

        }

        private void tbXmlEditor_TextChanged(object sender, EventArgs e)
        {
            xmlValidated = false;
        }

        #region XML validation
        //whether the xml is checked or not
        private bool xmlValidated = false;
        //whether the xml is valid or not, against xsd
        private bool xmlValid = false;

        private XmlTextReader Reader;
        private void XmlValidate(string xsdPath, string xmlContent)
        {
            try
            {
                // 1- Read XML file content
                this.Reader = new XmlTextReader(new StringReader(xmlContent)); ;

                // 2- Read Schema file content
                StreamReader SR = new StreamReader(xsdPath);

                // 3- Create a new instance of XmlSchema object
                XmlSchema Schema = new XmlSchema();
                // 4- Set Schema object by calling XmlSchema.Read() method
                Schema = XmlSchema.Read(SR,
                    new ValidationEventHandler(ReaderSettings_ValidationEventHandler));

                // 5- Create a new instance of XmlReaderSettings object
                XmlReaderSettings ReaderSettings = new XmlReaderSettings();
                // 6- Set ValidationType for XmlReaderSettings object
                ReaderSettings.ValidationType = ValidationType.Schema;
                // 7- Add Schema to XmlReaderSettings Schemas collection
                ReaderSettings.Schemas.Add(Schema);

                // 8- Add your ValidationEventHandler address to
                // XmlReaderSettings ValidationEventHandler
                ReaderSettings.ValidationEventHandler +=
                    new ValidationEventHandler(ReaderSettings_ValidationEventHandler);

                // 9- Create a new instance of XmlReader object
                XmlReader objXmlReader = XmlReader.Create(Reader, ReaderSettings);

                // 10- Read XML content in a loop
                while (objXmlReader.Read())
                { /*Empty loop*/}

                xmlValid = true;
                xmlValidated = true;

            }//try
            // Handle exceptions if you want
            catch (UnauthorizedAccessException AccessEx)
            {
                throw AccessEx;
            }//catch
            catch (Exception Ex)
            {
                throw Ex;
            }//catch
        }

        private void ReaderSettings_ValidationEventHandler(object sender,
    ValidationEventArgs args)
        {
            // 11- Implement your logic for each validation iteration
            string strTemp;
            strTemp = "Line: " + this.Reader.LineNumber + " - Position: "
                + this.Reader.LinePosition + " - " + args.Message;

            this.tbValidateResult.Text += strTemp;

            xmlValid = false;
        }
        #endregion
    }
}
