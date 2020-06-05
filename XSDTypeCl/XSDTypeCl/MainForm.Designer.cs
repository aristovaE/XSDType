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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XSDEditor));
            this.comboBox_SchemaList = new System.Windows.Forms.ComboBox();
            this.BtnToTV = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label_Ref = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.Button_Add = new System.Windows.Forms.Button();
            this.Button_Remove = new System.Windows.Forms.Button();
            this.button_AddSchema = new System.Windows.Forms.Button();
            this.textBox_Search = new System.Windows.Forms.TextBox();
            this.Button_Search = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Refresh = new System.Windows.Forms.Button();
            this.схемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.схемуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.всеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.схемыФТСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.текущуюСхемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.всеОткрытыеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.схемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.элементToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.проверитьXMLФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выгрузитьВWordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFD_XSD = new System.Windows.Forms.OpenFileDialog();
            this.saveFD_XSD = new System.Windows.Forms.SaveFileDialog();
            this.openFD_XML = new System.Windows.Forms.OpenFileDialog();
            this.saveFD_DOCX = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_SchemaList
            // 
            this.comboBox_SchemaList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.comboBox_SchemaList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_SchemaList.FormattingEnabled = true;
            this.comboBox_SchemaList.Location = new System.Drawing.Point(20, 29);
            this.comboBox_SchemaList.Name = "comboBox_SchemaList";
            this.comboBox_SchemaList.Size = new System.Drawing.Size(164, 21);
            this.comboBox_SchemaList.TabIndex = 4;
            this.toolTip1.SetToolTip(this.comboBox_SchemaList, "Список доступных схем");
            // 
            // BtnToTV
            // 
            this.BtnToTV.Enabled = false;
            this.BtnToTV.Location = new System.Drawing.Point(190, 26);
            this.BtnToTV.Name = "BtnToTV";
            this.BtnToTV.Size = new System.Drawing.Size(75, 25);
            this.BtnToTV.TabIndex = 6;
            this.BtnToTV.Text = "Вывод";
            this.toolTip1.SetToolTip(this.BtnToTV, "Вывести выбранную из списка схему");
            this.BtnToTV.UseVisualStyleBackColor = true;
            this.BtnToTV.Click += new System.EventHandler(this.BtnToTV_Click);
            // 
            // treeView
            // 
            this.treeView.ContextMenuStrip = this.contextMenuStrip;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView.Indent = 30;
            this.treeView.ItemHeight = 20;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(503, 331);
            this.treeView.TabIndex = 3;
            this.toolTip1.SetToolTip(this.treeView, "Здесь выводятся схемы в древовидном представлени");
            this.treeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
            this.treeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.SelectedItemWithFocusBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.propertyGrid.Size = new System.Drawing.Size(493, 393);
            this.propertyGrid.TabIndex = 4;
            this.toolTip1.SetToolTip(this.propertyGrid, "Здесь выводятся свойства выбранного элемента схемы");
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // label_Ref
            // 
            this.label_Ref.AutoSize = true;
            this.label_Ref.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Ref.Location = new System.Drawing.Point(0, 32);
            this.label_Ref.Name = "label_Ref";
            this.label_Ref.Size = new System.Drawing.Size(11, 13);
            this.label_Ref.TabIndex = 8;
            this.label_Ref.Text = " ";
            // 
            // listView
            // 
            this.listView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 48);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(215, 345);
            this.listView.TabIndex = 7;
            this.toolTip1.SetToolTip(this.listView, "Здесь выводятся результаты поиска \r\nили упоминания элементов");
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
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
            this.Button_Add.Location = new System.Drawing.Point(84, 346);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(75, 44);
            this.Button_Add.TabIndex = 28;
            this.Button_Add.Text = "Новый элемент";
            this.toolTip1.SetToolTip(this.Button_Add, "Добавьте элемент в схему");
            this.Button_Add.UseVisualStyleBackColor = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // Button_Remove
            // 
            this.Button_Remove.Enabled = false;
            this.Button_Remove.Location = new System.Drawing.Point(422, 346);
            this.Button_Remove.Name = "Button_Remove";
            this.Button_Remove.Size = new System.Drawing.Size(75, 44);
            this.Button_Remove.TabIndex = 29;
            this.Button_Remove.Text = "Удалить";
            this.toolTip1.SetToolTip(this.Button_Remove, "Удалите выделенный элемент");
            this.Button_Remove.UseVisualStyleBackColor = true;
            this.Button_Remove.Click += new System.EventHandler(this.Button_Remove_Click);
            // 
            // button_AddSchema
            // 
            this.button_AddSchema.Location = new System.Drawing.Point(3, 346);
            this.button_AddSchema.Name = "button_AddSchema";
            this.button_AddSchema.Size = new System.Drawing.Size(75, 44);
            this.button_AddSchema.TabIndex = 30;
            this.button_AddSchema.Text = "Новая схема";
            this.toolTip1.SetToolTip(this.button_AddSchema, "Добавьте новую схему в окно \r\nдревовидного представления");
            this.button_AddSchema.UseVisualStyleBackColor = true;
            this.button_AddSchema.Click += new System.EventHandler(this.Button_AddSchema_Click);
            // 
            // textBox_Search
            // 
            this.textBox_Search.Location = new System.Drawing.Point(0, 3);
            this.textBox_Search.Name = "textBox_Search";
            this.textBox_Search.Size = new System.Drawing.Size(131, 20);
            this.textBox_Search.TabIndex = 33;
            this.toolTip1.SetToolTip(this.textBox_Search, "Введите данные для поиска в данных схемы");
            this.textBox_Search.TextChanged += new System.EventHandler(this.textBox_Search_TextChanged);
            // 
            // Button_Search
            // 
            this.Button_Search.Enabled = false;
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
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.splitter3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.splitter4);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(20, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1217, 393);
            this.panel2.TabIndex = 35;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.propertyGrid);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(506, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(493, 393);
            this.panel5.TabIndex = 46;
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(503, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 393);
            this.splitter3.TabIndex = 45;
            this.splitter3.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button_AddSchema);
            this.panel4.Controls.Add(this.treeView);
            this.panel4.Controls.Add(this.Button_Remove);
            this.panel4.Controls.Add(this.Button_Add);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.MinimumSize = new System.Drawing.Size(400, 393);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(503, 393);
            this.panel4.TabIndex = 44;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter4.Location = new System.Drawing.Point(999, 0);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(3, 393);
            this.splitter4.TabIndex = 42;
            this.splitter4.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView);
            this.panel1.Controls.Add(this.label_Ref);
            this.panel1.Controls.Add(this.textBox_Search);
            this.panel1.Controls.Add(this.Button_Search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1002, 0);
            this.panel1.MinimumSize = new System.Drawing.Size(215, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 393);
            this.panel1.TabIndex = 37;
            // 
            // button_Refresh
            // 
            this.button_Refresh.Enabled = false;
            this.button_Refresh.Location = new System.Drawing.Point(271, 26);
            this.button_Refresh.Name = "button_Refresh";
            this.button_Refresh.Size = new System.Drawing.Size(75, 25);
            this.button_Refresh.TabIndex = 36;
            this.button_Refresh.Text = "Сброс";
            this.toolTip1.SetToolTip(this.button_Refresh, "Вывести все схемы списка");
            this.button_Refresh.UseVisualStyleBackColor = true;
            this.button_Refresh.Click += new System.EventHandler(this.button_Refresh_Click);
            // 
            // схемаToolStripMenuItem
            // 
            this.схемаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.toolStripSeparator1,
            this.сохранитьToolStripMenuItem,
            this.toolStripSeparator2,
            this.обновитьToolStripMenuItem});
            this.схемаToolStripMenuItem.Name = "схемаToolStripMenuItem";
            this.схемаToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.схемаToolStripMenuItem.Text = "Схема";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.схемуToolStripMenuItem1,
            this.всеСхемыToolStripMenuItem,
            this.схемыФТСToolStripMenuItem});
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // схемуToolStripMenuItem1
            // 
            this.схемуToolStripMenuItem1.Name = "схемуToolStripMenuItem1";
            this.схемуToolStripMenuItem1.Size = new System.Drawing.Size(140, 22);
            this.схемуToolStripMenuItem1.Text = "Схему";
            this.схемуToolStripMenuItem1.Click += new System.EventHandler(this.схемуToolStripMenuItem1_Click);
            // 
            // всеСхемыToolStripMenuItem
            // 
            this.всеСхемыToolStripMenuItem.Name = "всеСхемыToolStripMenuItem";
            this.всеСхемыToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.всеСхемыToolStripMenuItem.Text = "Схемы СТМ";
            this.всеСхемыToolStripMenuItem.Click += new System.EventHandler(this.открытьВсеСхемыToolStripMenuItem_Click);
            // 
            // схемыФТСToolStripMenuItem
            // 
            this.схемыФТСToolStripMenuItem.Name = "схемыФТСToolStripMenuItem";
            this.схемыФТСToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.схемыФТСToolStripMenuItem.Text = "Схемы ФТС";
            this.схемыФТСToolStripMenuItem.Click += new System.EventHandler(this.открытьСхемыФТСToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.текущуюСхемуToolStripMenuItem,
            this.всеОткрытыеСхемыToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // текущуюСхемуToolStripMenuItem
            // 
            this.текущуюСхемуToolStripMenuItem.Name = "текущуюСхемуToolStripMenuItem";
            this.текущуюСхемуToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.текущуюСхемуToolStripMenuItem.Text = "Текущую схему";
            this.текущуюСхемуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьТекущуюСхемуToolStripMenuItem_Click);
            // 
            // всеОткрытыеСхемыToolStripMenuItem
            // 
            this.всеОткрытыеСхемыToolStripMenuItem.Name = "всеОткрытыеСхемыToolStripMenuItem";
            this.всеОткрытыеСхемыToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.всеОткрытыеСхемыToolStripMenuItem.Text = "Все открытые схемы";
            this.всеОткрытыеСхемыToolStripMenuItem.Click += new System.EventHandler(this.всеОткрытыеСхемыToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.ToolTipText = "Вывести все схемы списка";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.схемуToolStripMenuItem,
            this.элементToolStripMenuItem});
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            // 
            // схемуToolStripMenuItem
            // 
            this.схемуToolStripMenuItem.Name = "схемуToolStripMenuItem";
            this.схемуToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.схемуToolStripMenuItem.Text = "Схему";
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
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.схемаToolStripMenuItem,
            this.добавитьToolStripMenuItem,
            this.проверитьXMLФайлToolStripMenuItem,
            this.выгрузитьВWordToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(1254, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // проверитьXMLФайлToolStripMenuItem
            // 
            this.проверитьXMLФайлToolStripMenuItem.Name = "проверитьXMLФайлToolStripMenuItem";
            this.проверитьXMLФайлToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.проверитьXMLФайлToolStripMenuItem.Text = "Проверить XML файл";
            this.проверитьXMLФайлToolStripMenuItem.ToolTipText = "Выберите схему, по которой будет проверяться файд XML";
            this.проверитьXMLФайлToolStripMenuItem.Click += new System.EventHandler(this.проверитьXMLФайлToolStripMenuItem_Click);
            // 
            // выгрузитьВWordToolStripMenuItem
            // 
            this.выгрузитьВWordToolStripMenuItem.AutoToolTip = true;
            this.выгрузитьВWordToolStripMenuItem.Name = "выгрузитьВWordToolStripMenuItem";
            this.выгрузитьВWordToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.выгрузитьВWordToolStripMenuItem.Text = "Экспорт в MS Word";
            this.выгрузитьВWordToolStripMenuItem.ToolTipText = "Нажмите на схему, чтобы выгрузить её в Word";
            this.выгрузитьВWordToolStripMenuItem.Click += new System.EventHandler(this.выгрузитьВWordToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "schemaPic.png");
            this.imageList1.Images.SetKeyName(1, "elementPic.png");
            this.imageList1.Images.SetKeyName(2, "complexTypePic.png");
            this.imageList1.Images.SetKeyName(3, "simpleTypePic.png");
            this.imageList1.Images.SetKeyName(4, "choicePic.png");
            // 
            // openFD_XSD
            // 
            this.openFD_XSD.FileName = "openXSD";
            this.openFD_XSD.Filter = "xsd files (*.xsd)|*.xsd";
            // 
            // saveFD_XSD
            // 
            this.saveFD_XSD.Filter = "*.xsd | XSD";
            // 
            // openFD_XML
            // 
            this.openFD_XML.FileName = "openXML";
            this.openFD_XML.Filter = "xml files (*.xml)|*.xml";
            // 
            // saveFD_DOCX
            // 
            this.saveFD_DOCX.Filter = "*.docx | DOCx";
            // 
            // XSDEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 456);
            this.Controls.Add(this.button_Refresh);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BtnToTV);
            this.Controls.Add(this.comboBox_SchemaList);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(855, 448);
            this.Name = "XSDEditor";
            this.Text = "XSDEditor";
            this.Load += new System.EventHandler(this.XSDEditor_Load);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_SchemaList;
        private System.Windows.Forms.Button BtnToTV;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button Button_Add;
        private System.Windows.Forms.Button Button_Remove;
        private System.Windows.Forms.Button button_AddSchema;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label label_Ref;
        private System.Windows.Forms.TextBox textBox_Search;
        private System.Windows.Forms.Button Button_Search;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.ToolStripMenuItem схемаToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem схемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem элементToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem всеСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem схемыФТСToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem текущуюСхемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem всеОткрытыеСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem проверитьXMLФайлToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFD_XSD;
        private System.Windows.Forms.SaveFileDialog saveFD_XSD;
        private System.Windows.Forms.ToolStripMenuItem схемуToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFD_XML;
        private System.Windows.Forms.ToolStripMenuItem выгрузитьВWordToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFD_DOCX;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

