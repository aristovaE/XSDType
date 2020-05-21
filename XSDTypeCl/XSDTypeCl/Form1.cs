using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Linq;
using Xceed.Words.NET;

namespace XSDTypeCl
{
    public partial class XSDEditor : Form
    {
        public XSDEditor()
        {
            InitializeComponent();
        }
        public XmlSchemaSet ReadXSD(DirectoryInfo diXsd)
        {
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            treeView.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
            try
            {
                foreach (var fi in diXsd.GetFiles())
                {
                    using (var sr = new StreamReader(fi.FullName))
                    {
                        xs = XmlSchema.Read(sr, ValidationErrorHandler);
                        xss.Add(xs);
                        schemas.Add(xs);
                    }
                    // MessageBox.Show("Schema " + fi.Name + " read successfully ");

                }
                xss.Compile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return xss;
        }

        public void UpdateTreeView(TreeNodeCollection tnc)
        {
            //treeView1.BeginUpdate();
            treeView.Nodes.Clear();
            for (int i = 0; i < comboBox_SchemaList.Items.Count; i++)
            {
                SeSchema seSchema = (SeSchema)comboBox_SchemaList.Items[i];
                seSchema.ClassToTreeView(treeView.Nodes);
            }
            //treeView1.EndUpdate();
        }
        public void UpdateComboBox()
        {
            List<SeSchema> seSchemaList = new List<SeSchema>();
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                seSchemaList.Add((SeSchema)treeView.Nodes[i].Tag);
            }

            ComboBoxBind(seSchemaList);
        }
        public void UpdateNode()
        {
            //treeView1.BeginUpdate();
            if (treeView.SelectedNode.Tag is SeSchemaItem)
            {
                SeSchemaItem ssi = (SeSchemaItem)treeView.SelectedNode.Tag;
                treeView.SelectedNode.Text = ssi.ToString();
            }
            else if (treeView.SelectedNode.Tag is SeSchema)
            {
                SeSchema ss = (SeSchema)treeView.SelectedNode.Tag;
                treeView.SelectedNode.Text = ss.ToString();
            }
            //treeView1.EndUpdate();
        }
        public void ComboBoxBind(List<SeSchema> seSchemaList)
        {
            comboBox_SchemaList.DataSource = null;
            comboBox_SchemaList.Items.Clear();
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox_SchemaList.DataSource = bindingSource1.DataSource;
            comboBox_SchemaList.DisplayMember = "Name";
            comboBox_SchemaList.ValueMember = "Name";
            текущуюСхемуToolStripMenuItem.Enabled = true;
        }

        private void BtnToTV_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox_SchemaList.SelectedItem;
            treeView.Nodes.Clear();

            if (seSchema != null)
                seSchema.ClassToTreeView(treeView.Nodes);
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                SeSchemaItem newssi = new SeSchemaItem();
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Text = newssi.ToString();
                newTreeNode.Tag = newssi;
                newTreeNode.ImageIndex = 1;
                treeView.SelectedNode.Nodes.Add(newTreeNode);
                if (treeView.SelectedNode.Tag is SeSchema)
                {
                    SeSchema seSchema = (SeSchema)treeView.SelectedNode.Tag;
                    newssi.Parent = seSchema;
                    seSchema.SchemaItems.Add(newssi);

                }
                else if (treeView.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView.SelectedNode.Tag;
                    if (ssi.SchemaItemsChildren == null)
                        ssi.SchemaItemsChildren = new List<SeSchemaItem>();
                    newssi.Parent = ssi;
                    ssi.SchemaItemsChildren.Add(newssi);
                }
            }
            else
            {
                MessageBox.Show("Для добавления нового элемента нажмите на ветку дерева");
            }
            //UpdateTreeView(treeView1.Nodes);
        }
        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode!= null)
            {
                treeView.SelectedNode.Remove();
                if (treeView.SelectedNode.Tag is SeSchema)
                {
                    UpdateComboBox();
                }
                else if (treeView.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView.SelectedNode.Tag;

                    if (treeView.SelectedNode.Parent.Tag is SeSchemaItem)
                    {
                        SeSchemaItem ssiParent = (SeSchemaItem)treeView.SelectedNode.Parent.Tag;
                        ssiParent.SchemaItemsChildren.Remove(ssi);
                    }
                    else if (treeView.SelectedNode.Parent.Tag is SeSchema)
                    {
                        SeSchema ssiParent = (SeSchema)treeView.SelectedNode.Parent.Tag;
                        ssiParent.SchemaItems.Remove(ssi);
                    }

                }

            }
            else
            {
                MessageBox.Show("Для удаления элемента нажмите на ветку дерева");
            }

        }
        private void Button_AddSchema_Click(object sender, EventArgs e)
        {
            SeSchema newSchema = new SeSchema();
            newSchema.ClassToTreeView(treeView.Nodes);
            UpdateComboBox();

            BtnToTV.Enabled = true;
            button_Refresh.Enabled = true;
            Button_Remove.Enabled = true;
        }
        private void Button_Search_Click(object sender, EventArgs e)
        {
            string search = textBox_Search.Text.ToLower();
            if (comboBox_SchemaList.SelectedItem != null)
            {
                SeSchema schemaToSearch = (SeSchema)comboBox_SchemaList.SelectedItem;
                List<SeSchemaItem> ssiList = schemaToSearch.FindElements(search);
                listView.Clear();
                label_Ref.Text = "Результаты поиска:";
                if (ssiList.Count == 0)
                {
                    listView.Items.Add("отсутствуют!");
                }
                foreach (SeSchemaItem element in ssiList)
                {
                    listView.Items.Add(element.Name);
                    listView.Items[listView.Items.Count - 1].Tag = element;
                }
            }
            else MessageBox.Show("Для поиска нужно открыть схемы XSD");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            treeView.BeginUpdate();
            listView.Clear();
            label_Ref.Text = "";

            if (e.Node.Tag is SeSchemaItem)
            {
                //заполнение propertyGrid
                propertyGrid.SelectedObject = e.Node.Tag;
                //заполнение listView
                SeSchemaItem ssi = (SeSchemaItem)e.Node.Tag;
                if (ssi.Type == "")
                {
                    SeSchemaItem ssiOfType = ssi.FindElementOfType();
                    if (ssiOfType != null)
                    {
                        label_Ref.Text = "Упоминания:";
                        listView.Items.Add(ssiOfType.Name);
                        listView.Items[listView.Items.Count - 1].Tag = ssiOfType;

                    }

                }
                //изменение ComboBox при нажатии на разные схемы
                TreeNode tn = e.Node;
                while (tn.Parent.Tag is SeSchemaItem)
                {
                    tn = tn.Parent;
                }
                if (tn.Parent.Tag is SeSchema && tn.Parent.Index < comboBox_SchemaList.Items.Count)
                    comboBox_SchemaList.SelectedIndex = tn.Parent.Index;

                //обновление каждой дочерней ветки
                if (e.Node.Nodes != null)
                {
                    foreach (TreeNode treenode in e.Node.Nodes)
                    {
                        SeSchemaItem ssichild = (SeSchemaItem)treenode.Tag;
                        treenode.Text = ssichild.ToString();
                    }
                }
            }
            else if (e.Node.Tag is SeSchema)
            {
                comboBox_SchemaList.Refresh();
                propertyGrid.SelectedObject = e.Node.Tag;
                if (e.Node.Index < comboBox_SchemaList.Items.Count)
                    comboBox_SchemaList.SelectedIndex = e.Node.Index;
            }
            else { }
            treeView.EndUpdate();

            this.Cursor = Cursors.Default;
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateNode();
            if (propertyGrid.SelectedObject is SeSchemaItem)
            {
                SeSchemaItem ssi = (SeSchemaItem)propertyGrid.SelectedObject;
                if (treeView.SelectedNode != null)
                {
                    //изменение типа у элементов с измененным ComplexType
                    if (ssi.Parent is SeSchema && ((ContainerControl)s).ActiveControl.AccessibleName == "Name" && ssi.Type == "")
                    {
                        ssi.ChangeNewComplexType(e.OldValue, e.ChangedItem.Value);
                    }
                    //изменение иконки для ComplexType
                    if (ssi.Type == "")
                    {
                        treeView.SelectedNode.ImageIndex = 2;
                    }
                    //изменение иконки для element
                    if (ssi.Type != "")
                    {
                        treeView.SelectedNode.ImageIndex = 1;
                    }
                    //изменение иконки для SimpleType 
                    if (ssi.Name == "SimpleType")
                    {
                        treeView.SelectedNode.ImageIndex = 3;
                    }
                    //изменение иконки для Choice
                    if (ssi.Name == "CHOICE")
                    {
                        treeView.SelectedNode.ImageIndex = 4;
                    }

                    //проверка строки на содержание цифр
                    if (((ContainerControl)s).ActiveControl.AccessibleName == "Name")
                    {
                        bool correct=true;
                        string marks = ".,/':;!?-=+|/";
                        foreach (char symbol in ssi.Name)
                        {
                            if (symbol < '0' || symbol > '9')
                            {
                                correct = true;
                            }
                            else
                                correct = false;
                            foreach (char mark in marks)
                            {
                                if (symbol == mark)
                                    correct = false;
                            }
                         }
                        if (!correct)
                        {
                            ((ContainerControl)s).ActiveControl.Text = "Untitled";
                            MessageBox.Show("Имя элемента не может содержать цифры или знаки препинания");
                            
                        }
                    }
                }
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                SeSchemaItem ssi = (SeSchemaItem)listView.SelectedItems[0].Tag;
                propertyGrid.SelectedObject = ssi;

                TreeNode[] treenodesParent = treeView.Nodes.Find(ssi.GetSchema(ssi).ToString(), false);
                TreeNode[] treenodes = treenodesParent[0].Nodes.Find(ssi.ToString() + ssi.Parent.ToString(), true);
                TreeNode eachTn = treenodes[0];
                while (eachTn.Parent.Tag is SeSchemaItem)
                {
                    eachTn.Parent.Expand();
                    eachTn = eachTn.Parent;

                }
                treeView.Focus();
                treeView.SelectedNode = treenodes[0];

            }
        }

        private void открытьВсеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\Resources\xsd\"));
            XmlSchemaSet xss = ReadXSD(diXsd);
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            //treeView1.BeginUpdate();
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView.Nodes);
            }
            //treeView1.EndUpdate();
            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button_Refresh.Enabled = true;
            Button_Remove.Enabled = true;

        }

        private void открытьСхемыФТСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView.SelectedNode = null;
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\Resources\xsd\фтс2\"));
            XmlSchemaSet xss = ReadXSD(diXsd);
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            this.Cursor = Cursors.WaitCursor;

            treeView.BeginUpdate();

            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView.Nodes);
            }
            treeView.EndUpdate();
            this.Cursor = Cursors.Default;
            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button_Refresh.Enabled = true;
            Button_Remove.Enabled = true;
        }

        private void сохранитьТекущуюСхемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFD_XSD.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFD_XSD.FileName;

            XmlSchema xs1 = new XmlSchema();
            SeSchema seSchema = null;
            XmlSchemaSet xss = new XmlSchemaSet();
            ValidationEventHandler ValidationErrorHandler = null;
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            if ((treeView.SelectedNode != null) && (treeView.SelectedNode.Tag is SeSchema))
            {
                seSchema = (SeSchema)treeView.SelectedNode.Tag;
            }
            else
            {
                seSchema = (SeSchema)comboBox_SchemaList.SelectedItem;
            }
            seSchema.SaveXSD(xs1);
            xss.Add(xs1);
            //для записи в readonly ElementSchemaType 
            try
            {
                xss.Compile();
            }
            catch (Exception em)
            {
                MessageBox.Show(em.ToString());
            }
            using (FileStream fs = new FileStream(filename + ".xsd", FileMode.Create, FileAccess.ReadWrite))
            {
                using (XmlTextWriter tw = new XmlTextWriter(fs, new UTF8Encoding()))
                {
                    tw.Formatting = Formatting.Indented;
                    xs1.Write(tw);
                }
                fs.Close();
            }

            MessageBox.Show($"Файл {filename}.xsd успешно сохранен");
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTreeView(treeView.Nodes);
        }

        private void схемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeSchema newSchema = new SeSchema();
            newSchema.ClassToTreeView(treeView.Nodes);
            UpdateComboBox();
        }

        private void элементToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                SeSchemaItem newssi = new SeSchemaItem();
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Text = newssi.ToString();
                newTreeNode.Tag = newssi;

                treeView.SelectedNode.Nodes.Add(newTreeNode);
                if (treeView.SelectedNode.Tag is SeSchema)
                {
                    SeSchema seSchema = (SeSchema)treeView.SelectedNode.Tag;
                    newssi.Parent = seSchema;
                    seSchema.SchemaItems.Add(newssi);

                }
                else if (treeView.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView.SelectedNode.Tag;
                    if (ssi.SchemaItemsChildren == null)
                        ssi.SchemaItemsChildren = new List<SeSchemaItem>();
                    newssi.Parent = ssi;
                    ssi.SchemaItemsChildren.Add(newssi);
                }
            }
            else
            {
                MessageBox.Show("Для добавления нового элемента или схемы нажмите на ветку TreeView");//?
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            // Убедитесь, что это правая кнопка.
            if (e.Button != MouseButtons.Right) return;

            // Выберите этот узел.
            TreeNode node_here = treeView.GetNodeAt(e.X, e.Y);
            treeView.SelectedNode = node_here;

            // Если treenode в данном месте нет
            if (node_here == null)
            {
                ToolStripMenuItem addSchema = new ToolStripMenuItem("Добавить схему");
                contextMenuStrip1.Items.AddRange(new[] { addSchema });
                addSchema.Click += схемуToolStripMenuItem_Click;
                return;
            }
            else if (node_here.Tag is SeSchema)
            {
                ToolStripMenuItem addElement = new ToolStripMenuItem("Добавить элемент");
                ToolStripMenuItem delElement = new ToolStripMenuItem("Удалить cхему");
                ToolStripMenuItem saveSchema = new ToolStripMenuItem("Сохранить cхему");
                contextMenuStrip1.Items.AddRange(new[] { addElement, delElement, saveSchema });
                addElement.Click += элементToolStripMenuItem_Click;
                delElement.Click += Button_Remove_Click;
                saveSchema.Click += сохранитьТекущуюСхемуToolStripMenuItem_Click;
            }
            else if (node_here.Tag is SeSchemaItem)
            {
                ToolStripMenuItem addElement = new ToolStripMenuItem("Добавить элемент");
                ToolStripMenuItem delElement = new ToolStripMenuItem("Удалить элемент");
                contextMenuStrip1.Items.AddRange(new[] { addElement, delElement });
                addElement.Click += элементToolStripMenuItem_Click;
                delElement.Click += Button_Remove_Click;
            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            List<SeSchema> seSchemaList = null;
            treeView.Nodes.Clear();
            foreach (SeSchema schema in comboBox_SchemaList.Items)
            {
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(schema);
                schema.ClassToTreeView(treeView.Nodes);
            }
            ComboBoxBind(seSchemaList);
        }

        private void всеОткрытыеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSchemaSet xss = new XmlSchemaSet();
            ValidationEventHandler ValidationErrorHandler = null;
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();

            foreach (SeSchema seSchema in comboBox_SchemaList.Items)
            {
                XmlSchema xs1 = new XmlSchema();
                seSchema.SaveXSD(xs1);
                xss.Add(xs1);
            }
            //для записи в readonly ElementSchemaType 
            xss.Compile();
            string schemaName = "";
            foreach (XmlSchema xs in xss.Schemas())

            {
                foreach (XmlSchemaObject xObject in xs.Items)
                {
                    if (xObject is XmlSchemaElement)
                    {
                        XmlSchemaElement xElement = (XmlSchemaElement)xObject;
                        schemaName = xElement.Name;
                    }
                }
                using (FileStream fs = new FileStream(@"..\..\..\..\xsd\new\" + schemaName + "NEW.xsd", FileMode.Create, FileAccess.ReadWrite))
                {
                    using (XmlTextWriter tw = new XmlTextWriter(fs, new UTF8Encoding()))
                    {
                        tw.Formatting = Formatting.Indented;
                        xs.Write(tw);
                    }
                    fs.Close();
                }
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //treeView1.BeginUpdate();//долгая загрузка
            this.SuspendLayout();//белый экран
        }


        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            this.ResumeLayout();
            //treeView1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Справка
        /// </summary>
        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, @"XSDEditor.chm", HelpNavigator.Index);
        }

        /// <summary>
        /// Метод, отменяющий замену иконки ветки при ее раскрытии
        /// </summary>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                treeView.SelectedImageIndex = treeView.SelectedNode.ImageIndex;
            }

        }

        private void проверитьXMLФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFD_XML.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFD_XML.SafeFileName;
            // читаем файл в строку

            XmlReaderSettings xsdSetting = new XmlReaderSettings();
            XmlSchema xs1 = new XmlSchema();
            SeSchema seSchema = null;
            XmlSchemaSet xss = new XmlSchemaSet();
            ValidationEventHandler ValidationErrorHandler = null;
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            if ((treeView.SelectedNode != null) && (treeView.SelectedNode.Tag is SeSchema))
            {
                seSchema = (SeSchema)treeView.SelectedNode.Tag;
            }
            else
            {
                seSchema = (SeSchema)comboBox_SchemaList.SelectedItem;
            }
            seSchema.SaveXSD(xs1);
            xss.Add(xs1);
            //для записи в readonly ElementSchemaType 
            try
            {
                xss.Compile();
            }
            catch (Exception em)
            {
                MessageBox.Show(em.ToString());
            }
            XmlSchemaSet schemas = new XmlSchemaSet();

            XDocument doc = XDocument.Load(filename);
            string msg = "";
            doc.Validate(xss, (o, e1) =>
            {
                msg += "\n - " + e1.Message + Environment.NewLine;
            }
            );
            MessageBox.Show(msg == "" ? $"Документ { filename} успешно прошел проверку по схеме {seSchema.Name}" : $"Документ { filename} НЕ прошел проверку: \n" + msg);
        }

        private void схемуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFD_XSD.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filePath = openFD_XSD.FileName;
            string filename = openFD_XSD.SafeFileName;
            DirectoryInfo diXsd = new DirectoryInfo(filePath);

            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            treeView.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
            try
            {

                using (var sr = new StreamReader(diXsd.FullName))
                {
                    xs = XmlSchema.Read(sr, ValidationErrorHandler);
                    xss.Add(xs);
                    schemas.Add(xs);
                }
                // MessageBox.Show("Schema " + fi.Name + " read successfully ");


                xss.Compile();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e.ToString());
            }

            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            //treeView1.BeginUpdate();
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView.Nodes);
            }
            //treeView1.EndUpdate();
            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button_Refresh.Enabled = true;
            Button_Remove.Enabled = true;
        }

        private void выгрузитьВWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFD_DOCX.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFD_DOCX.FileName;
            SeSchema seSchema = (SeSchema)comboBox_SchemaList.SelectedItem;


            DocX doc = DocX.Create(filename);
            var titleParagraph = doc.InsertParagraph();
            titleParagraph.Append(seSchema.ToString());
            titleParagraph.Bold();
            var infoParagraph = doc.InsertParagraph();
            infoParagraph.Append($"Данная схема имеет подтаблиц - {seSchema.SchemaItems.Count - 1}");//!!!

            var emptyParagraph = doc.InsertParagraph();
            emptyParagraph.Append(" ");

            foreach (SeSchemaItem si in seSchema.SchemaItems)
            {

                foreach (SeSchemaItem sic in si.SchemaItemsChildren)
                {
                    int i = 1;
                    var tableInfoParagraph = doc.InsertParagraph();

                    tableInfoParagraph.Append(si.ToString() + ":");
                    tableInfoParagraph.Bold();
                    emptyParagraph.Append(" ");

                    foreach (SeSchemaItem sicchild in sic.SchemaItemsChildren)
                    {
                        if (sic.SchemaItemsChildren.Count > 0)
                        {
                            var tableParagraph = doc.InsertParagraph();
                            if (sicchild.CheckToCommonTypes() != true)
                                tableParagraph.Append($"{i}. {sicchild.ToString()}: (данные таблицы {sicchild.Type})");
                            else
                            {
                                tableParagraph.Append($"{i}. {sicchild.ToString()}:______________________________");
                            }
                            i++;
                        }
                        var emptyParagraph1 = doc.InsertParagraph();
                        emptyParagraph1.Append(" ");
                    }
                    var emptyParagraph2 = doc.InsertParagraph();
                    emptyParagraph2.Append(" ");
                }
            }

            doc.Save();

        }

        private void XSDEditor_Load(object sender, EventArgs e)
        {
            treeView.ImageList = imageList1;
            
        }

        private void textBox_Search_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Search.Text != "")
                Button_Search.Enabled = true;
            else
                Button_Search.Enabled = false;
        }
    }
    }

