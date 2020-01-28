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
            this.BtnXSDToSeSChema = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BtnToTV = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // BtnXSDToSeSChema
            // 
            this.BtnXSDToSeSChema.Location = new System.Drawing.Point(12, 37);
            this.BtnXSDToSeSChema.Name = "BtnXSDToSeSChema";
            this.BtnXSDToSeSChema.Size = new System.Drawing.Size(164, 38);
            this.BtnXSDToSeSChema.TabIndex = 2;
            this.BtnXSDToSeSChema.Text = "All schemas to TreeView";
            this.BtnXSDToSeSChema.UseVisualStyleBackColor = true;
            this.BtnXSDToSeSChema.Click += new System.EventHandler(this.BtnXSDToSeSChema_Click);
            // 
            // treeView1
            // 
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(12, 81);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(596, 526);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // BtnToTV
            // 
            this.BtnToTV.Location = new System.Drawing.Point(182, 12);
            this.BtnToTV.Name = "BtnToTV";
            this.BtnToTV.Size = new System.Drawing.Size(164, 21);
            this.BtnToTV.TabIndex = 6;
            this.BtnToTV.Text = "1 schema to TreeView";
            this.BtnToTV.UseVisualStyleBackColor = true;
            this.BtnToTV.Click += new System.EventHandler(this.BtnToTV_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(182, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 38);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(614, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 20);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(614, 107);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(208, 20);
            this.textBox2.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 619);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BtnToTV);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.BtnXSDToSeSChema);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnXSDToSeSChema;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button BtnToTV;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

