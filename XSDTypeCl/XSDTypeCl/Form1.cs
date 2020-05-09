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
            treeView.ImageList = imageList1;
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
            if (treeView.SelectedNode.Tag != null)
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

            }

        }
        private void Button_Schema_Click(object sender, EventArgs e)
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
            SeSchema schemaToSearch = (SeSchema)comboBox_SchemaList.SelectedItem;
            List<SeSchemaItem> ssiList = schemaToSearch.FindElements(search);
            listView.Clear();
            label_Ref.Text = "Результаты поиска:";
            if (ssiList.Count == 0)
            {
                listView.Items.Add("отсутсвуют!");
            }
            foreach (SeSchemaItem element in ssiList)
            {
                listView.Items.Add(element.Name);
                listView.Items[listView.Items.Count - 1].Tag = element;
            }
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
                    List<SeSchemaItem> ls = ssi.FindAllTypes();
                    if (ls.Count != 0)
                    {
                        label_Ref.Text = "Упоминания:";
                        foreach (SeSchemaItem element in ls)
                        {
                            listView.Items.Add(element.Name);
                            listView.Items[listView.Items.Count - 1].Tag = element;
                        }
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
            //ПОДСВЕТКА ИЗМЕННЫХ ВЕТОК
            UpdateNode();
            if (propertyGrid.SelectedObject is SeSchemaItem)
            {
                //изменение типа у элементов с измененным ComplexType
                SeSchemaItem ssi = (SeSchemaItem)propertyGrid.SelectedObject;
                if (ssi.Parent is SeSchema && ((ContainerControl)s).ActiveControl.AccessibleName == "Name" && ssi.Type == "")
                {
                    ssi.ChangeNewComplexType(e.OldValue, e.ChangedItem.Value);
                    if (treeView.SelectedNode != null)
                        treeView.SelectedNode.ImageIndex = 2;
                }
                //изменение иконки для SimpleType 
                if (((ContainerControl)s).ActiveControl.AccessibleName == "Name" && ssi.Name == "SimpleType")
                {
                   
                    if (treeView.SelectedNode != null)
                        treeView.SelectedNode.ImageIndex = 3;
                }
                //изменение иконки для Choice
                if (((ContainerControl)s).ActiveControl.AccessibleName == "Name" && ssi.Name == "CHOICE")
                {

                    if (treeView.SelectedNode != null)
                        treeView.SelectedNode.ImageIndex = 4;
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
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));
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

        private void открытьНовыеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\new\"));
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
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\фтс2\"));
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

        private void button2_Click(object sender, EventArgs e)
        {
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));
            XmlSchemaSet xss = ReadXSD(diXsd);
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView.Nodes);
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
            //открытие нового окна ? с выбором схемы? и кнопкой опенфайлдиалог для выбора хмл файла
            //или сразу открыть опенфайлдиалог для выбора и взять ранее выбранную схему
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
                msg += e1.Message + Environment.NewLine;
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
            infoParagraph.Append($"Данная схема имеет подтаблиц - {seSchema.SchemaItems.Count-1}");//!!!

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

                    foreach (SeSchemaItem sicc in sic.SchemaItemsChildren)
                    {
                        if (sic.SchemaItemsChildren.Count > 0)
                        {
                            var tableParagraph = doc.InsertParagraph();
                            tableParagraph.Append($"{i}. {sicc.ToString()}:______________________________");
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
    }
}

