namespace MapResourceExplorer.UI
{
    partial class XmlEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbXmlEditor = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tbXmlEditor
            // 
            this.tbXmlEditor.Location = new System.Drawing.Point(12, 11);
            this.tbXmlEditor.Name = "tbXmlEditor";
            this.tbXmlEditor.Size = new System.Drawing.Size(497, 343);
            this.tbXmlEditor.TabIndex = 0;
            this.tbXmlEditor.Text = "";
            // 
            // XmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 411);
            this.Controls.Add(this.tbXmlEditor);
            this.Name = "XmlEditor";
            this.Text = "XmlEditor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbXmlEditor;
    }
}