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
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BtnToTV = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.Button_Add = new System.Windows.Forms.Button();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.схемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьВсеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьНовыеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьСхемыФТСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьТекущуюСхемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.схемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.элементToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(20, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // BtnToTV
            // 
            this.BtnToTV.Location = new System.Drawing.Point(190, 26);
            this.BtnToTV.Name = "BtnToTV";
            this.BtnToTV.Size = new System.Drawing.Size(75, 25);
            this.BtnToTV.TabIndex = 6;
            this.BtnToTV.Text = "Вывод";
            this.BtnToTV.UseVisualStyleBackColor = true;
            this.BtnToTV.Click += new System.EventHandler(this.BtnToTV_Click);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.Indent = 30;
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(576, 393);
            this.treeView1.TabIndex = 3;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid1.Size = new System.Drawing.Size(503, 393);
            this.propertyGrid1.TabIndex = 4;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = " ";
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.Location = new System.Drawing.Point(0, 48);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(215, 345);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(0, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 3);
            this.splitter2.TabIndex = 0;
            this.splitter2.TabStop = false;
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(109, 456);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(75, 44);
            this.Button_Add.TabIndex = 28;
            this.Button_Add.Text = "Новый элемент";
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // Button_Remove
            // 
            this.Button_Remove.Location = new System.Drawing.Point(521, 456);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(75, 44);
            this.Button_Remove.TabIndex = 29;
            this.Button_Remove.Text = "Удалить";
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 456);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 44);
            this.button1.TabIndex = 30;
            this.button1.Text = "Новая схема";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Schema_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(128, 20);
            this.textBox1.TabIndex = 33;
            // 
            // Button_Search
            // 
            this.Button_Search.Location = new System.Drawing.Point(137, 0);
            this.Button_Search.Name = "Button_Search";
            this.Button_Search.Size = new System.Drawing.Size(75, 25);
            this.Button_Search.TabIndex = 34;
            this.Button_Search.Text = "Поиск";
            this.Button_Search.UseVisualStyleBackColor = true;
            this.Button_Search.Click += new System.EventHandler(this.Button_Search_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.splitter4);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.splitter3);
            this.panel2.Controls.Add(this.treeView1);
            this.panel2.Location = new System.Drawing.Point(20, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1300, 393);
            this.panel2.TabIndex = 35;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.Button_Search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1085, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 393);
            this.panel1.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(271, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 36;
            this.button2.Text = "Сброс";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // схемаToolStripMenuItem
            // 
            this.схемаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьВсеСхемыToolStripMenuItem,
            this.открытьНовыеСхемыToolStripMenuItem,
            this.открытьСхемыФТСToolStripMenuItem,
            this.toolStripSeparator1,
            this.сохранитьТекущуюСхемуToolStripMenuItem,
            this.toolStripSeparator2,
            this.обновитьToolStripMenuItem});
            this.схемаToolStripMenuItem.Name = "схемаToolStripMenuItem";
            this.схемаToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.схемаToolStripMenuItem.Text = "Схема";
            // 
            // открытьВсеСхемыToolStripMenuItem
            // 
            this.открытьВсеСхемыToolStripMenuItem.Name = "открытьВсеСхемыToolStripMenuItem";
            this.открытьВсеСхемыToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.открытьВсеСхемыToolStripMenuItem.Text = "Открыть все схемы";
            this.открытьВсеСхемыToolStripMenuItem.Click += new System.EventHandler(this.открытьВсеСхемыToolStripMenuItem_Click);
            // 
            // открытьНовыеСхемыToolStripMenuItem
            // 
            this.открытьНовыеСхемыToolStripMenuItem.Name = "открытьНовыеСхемыToolStripMenuItem";
            this.открытьНовыеСхемыToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.открытьНовыеСхемыToolStripMenuItem.Text = "Открыть новые схемы";
            this.открытьНовыеСхемыToolStripMenuItem.Click += new System.EventHandler(this.открытьНовыеСхемыToolStripMenuItem_Click);
            // 
            // открытьСхемыФТСToolStripMenuItem
            // 
            this.открытьСхемыФТСToolStripMenuItem.Name = "открытьСхемыФТСToolStripMenuItem";
            this.открытьСхемыФТСToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.открытьСхемыФТСToolStripMenuItem.Text = "Открыть схемы ФТС";
            this.открытьСхемыФТСToolStripMenuItem.Click += new System.EventHandler(this.открытьСхемыФТСToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // сохранитьТекущуюСхемуToolStripMenuItem
            // 
            this.сохранитьТекущуюСхемуToolStripMenuItem.Name = "сохранитьТекущуюСхемуToolStripMenuItem";
            this.сохранитьТекущуюСхемуToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.сохранитьТекущуюСхемуToolStripMenuItem.Text = "Сохранить текущую схему";
            this.сохранитьТекущуюСхемуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьТекущуюСхемуToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.схемуToolStripMenuItem,
            this.элементToolStripMenuItem});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.добавитьToolStripMenuItem.Text = "Новая";
            // 
            // схемуToolStripMenuItem
            // 
            this.схемуToolStripMenuItem.Name = "схемуToolStripMenuItem";
            this.схемуToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.схемуToolStripMenuItem.Text = "Схема";
            this.схемуToolStripMenuItem.Click += new System.EventHandler(this.схемуToolStripMenuItem_Click);
            // 
            // элементToolStripMenuItem
            // 
            this.элементToolStripMenuItem.Name = "элементToolStripMenuItem";
            this.элементToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.элементToolStripMenuItem.Text = "Элемент";
            this.элементToolStripMenuItem.Click += new System.EventHandler(this.элементToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.схемаToolStripMenuItem,
            this.добавитьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1331, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(576, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 393);
            this.splitter3.TabIndex = 41;
            this.splitter3.TabStop = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(1082, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 393);
            this.splitter4.TabIndex = 42;
            this.splitter4.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.propertyGrid1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(579, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(503, 393);
            this.panel3.TabIndex = 43;
            // 
            // XSDEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 512);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Button_Remove);
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.BtnToTV);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "XSDEditor";
            this.Text = "XSDEditor";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button BtnToTV;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem схемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьВсеСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьНовыеСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьСхемыФТСToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьТекущуюСхемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem схемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem элементToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel3;
    }
}

