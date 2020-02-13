namespace XSDTypeCl
{
    partial class XSDEditor
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BtnToTV = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Button_Refresh = new System.Windows.Forms.Button();
            this.Button_Add = new System.Windows.Forms.Button();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnXSDToSeSChema
            // 
            this.BtnXSDToSeSChema.Location = new System.Drawing.Point(12, 44);
            this.BtnXSDToSeSChema.Name = "BtnXSDToSeSChema";
            this.BtnXSDToSeSChema.Size = new System.Drawing.Size(164, 38);
            this.BtnXSDToSeSChema.TabIndex = 2;
            this.BtnXSDToSeSChema.Text = "All schemas to TreeView";
            this.BtnXSDToSeSChema.UseVisualStyleBackColor = true;
            this.BtnXSDToSeSChema.Click += new System.EventHandler(this.BtnXSDToSeSChema_Click);
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
            this.BtnToTV.Location = new System.Drawing.Point(182, 10);
            this.BtnToTV.Name = "BtnToTV";
            this.BtnToTV.Size = new System.Drawing.Size(164, 21);
            this.BtnToTV.TabIndex = 6;
            this.BtnToTV.Text = "1 schema to TreeView";
            this.BtnToTV.UseVisualStyleBackColor = true;
            this.BtnToTV.Click += new System.EventHandler(this.BtnToTV_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Enabled = false;
            this.BtnSave.Location = new System.Drawing.Point(182, 44);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(164, 38);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "Save Schema";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Indent = 30;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(771, 565);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(771, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(608, 565);
            this.propertyGrid1.TabIndex = 4;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(771, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 565);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.propertyGrid1);
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Location = new System.Drawing.Point(12, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1379, 565);
            this.panel1.TabIndex = 26;
            // 
            // Button_Refresh
            // 
            this.Button_Refresh.Location = new System.Drawing.Point(1061, 59);
            this.Button_Refresh.Name = "Button_Refresh";
            this.Button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.Button_Refresh.TabIndex = 27;
            this.Button_Refresh.Text = "Refresh";
            this.Button_Refresh.UseVisualStyleBackColor = true;
            this.Button_Refresh.Click += new System.EventHandler(this.Button_Refresh_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(101, 669);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(83, 23);
            this.Button_Add.TabIndex = 28;
            this.Button_Add.Text = "Add Element";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // Button_Remove
            // 
            this.Button_Remove.Location = new System.Drawing.Point(708, 669);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(75, 23);
            this.Button_Remove.TabIndex = 29;
            this.Button_Remove.Text = "Remove";
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // XSDEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1403, 721);
            this.Controls.Add(this.Button_Remove);
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.Button_Refresh);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.BtnToTV);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.BtnXSDToSeSChema);
            this.Name = "XSDEditor";
            this.Text = "XSDEditor";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button BtnXSDToSeSChema;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button BtnToTV;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Button_Refresh;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Button Button_Remove;
    }
}

