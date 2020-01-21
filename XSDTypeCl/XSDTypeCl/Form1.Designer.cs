namespace XSDTypeCl
{
    partial class Form1
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
            this.readXsd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // readXsd
            // 
            this.readXsd.Location = new System.Drawing.Point(392, 68);
            this.readXsd.Name = "readXsd";
            this.readXsd.Size = new System.Drawing.Size(155, 64);
            this.readXsd.TabIndex = 0;
            this.readXsd.Text = "READ";
            this.readXsd.UseVisualStyleBackColor = true;
            this.readXsd.Click += new System.EventHandler(this.readXsd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 539);
            this.Controls.Add(this.readXsd);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button readXsd;
    }
}

