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
            this.xsdToTreeViewBtn = new System.Windows.Forms.Button();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // xsdToTreeViewBtn
            // 
            this.xsdToTreeViewBtn.Location = new System.Drawing.Point(12, 37);
            this.xsdToTreeViewBtn.Name = "xsdToTreeViewBtn";
            this.xsdToTreeViewBtn.Size = new System.Drawing.Size(468, 38);
            this.xsdToTreeViewBtn.TabIndex = 2;
            this.xsdToTreeViewBtn.Text = "READ";
            this.xsdToTreeViewBtn.UseVisualStyleBackColor = true;
            this.xsdToTreeViewBtn.Click += new System.EventHandler(this.xsdToTreeViewBtn_Click);
            // 
            // treeView2
            // 
            this.treeView2.ItemHeight = 20;
            this.treeView2.Location = new System.Drawing.Point(12, 81);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(468, 526);
            this.treeView2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 619);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.xsdToTreeViewBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button xsdToTreeViewBtn;
        private System.Windows.Forms.TreeView treeView2;
    }
}

