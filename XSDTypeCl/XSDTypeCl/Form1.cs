using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

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

            treeView1.Nodes.Clear();
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
            treeView1.Nodes.Clear();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                SeSchema seSchema = (SeSchema)comboBox1.Items[i];
                seSchema.ClassToTreeView(treeView1.Nodes);
            }
        }
        public void UpdateComboBox(SeSchema newSchema)
        {
            List<SeSchema> seSchemaList = new List<SeSchema>();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                seSchemaList.Add((SeSchema)comboBox1.Items[i]);
            }
            if (newSchema != null)
                seSchemaList.Add(newSchema);
            ComboBoxBind(seSchemaList);
        }
        public void UpdateNode()
        {
            if (treeView1.SelectedNode.Tag is SeSchemaItem)
            {
                SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;
                treeView1.SelectedNode.Text = ssi.ToString();
            }
            else if (treeView1.SelectedNode.Tag is SeSchema)
            {
                SeSchema ss = (SeSchema)treeView1.SelectedNode.Tag;
                treeView1.SelectedNode.Text = ss.ToString();
            }

        }
        public void ComboBoxBind(List<SeSchema> seSchemaList)
        {
            comboBox1.DataSource = null;
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";
            сохранитьТекущуюСхемуToolStripMenuItem.Enabled = true;
        }

        private void BtnToTV_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            treeView1.Nodes.Clear();

            if (seSchema != null)
                seSchema.ClassToTreeView(treeView1.Nodes);
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                SeSchemaItem newssi = new SeSchemaItem();
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Text = newssi.ToString();
                newTreeNode.Tag = newssi;

                treeView1.SelectedNode.Nodes.Add(newTreeNode);
                if (treeView1.SelectedNode.Tag is SeSchema)
                {
                    SeSchema seSchema = (SeSchema)treeView1.SelectedNode.Tag;
                    newssi.Parent = seSchema;
                    seSchema.SchemaItems.Add(newssi);

                }
                else if (treeView1.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;
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
            //UpdateTreeView(treeView1.Nodes);
        }
        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                if (treeView1.SelectedNode.Tag is SeSchema)
                {
                    //comboBox1.DataSource = null;
                    //comboBox1.Items.Remove(comboBox1.SelectedItem);
                    //UpdateComboBox(null);
                }
                else if (treeView1.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;

                    if (treeView1.SelectedNode.Parent.Tag is SeSchemaItem)
                    {
                        SeSchemaItem ssiParent = (SeSchemaItem)treeView1.SelectedNode.Parent.Tag;
                        ssiParent.SchemaItemsChildren.Remove(ssi);
                    }
                    else if (treeView1.SelectedNode.Parent.Tag is SeSchema)
                    {
                        SeSchema ssiParent = (SeSchema)treeView1.SelectedNode.Parent.Tag;
                        ssiParent.SchemaItems.Remove(ssi);
                    }

                }
                treeView1.SelectedNode.Remove();
            }
            else
            {

            }

        }
        private void Button_Schema_Click(object sender, EventArgs e)
        {
            SeSchema newSchema = new SeSchema();
            newSchema.ClassToTreeView(treeView1.Nodes);
            UpdateComboBox(newSchema);
        }
        private void Button_Search_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text.ToLower();
            SeSchema schemaToSearch = (SeSchema)comboBox1.SelectedItem;
            List<SeSchemaItem> ssiList = schemaToSearch.FindElements(search);
            listView1.Clear();
            label1.Text = "Результаты поиска:";
            foreach (SeSchemaItem element in ssiList)
            {
                listView1.Items.Add(element.Name);
                listView1.Items[listView1.Items.Count - 1].Tag = element;
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            listView1.Clear();
            label1.Text = "";
            if (e.Node.Tag is SeSchemaItem)
            {
                //заполнение propertyGrid
                propertyGrid1.SelectedObject = e.Node.Tag;
                //заполнение listView
                SeSchemaItem ssi = (SeSchemaItem)e.Node.Tag;
                if (ssi.Type == "")
                {
                    List<SeSchemaItem> ls = ssi.FindAllTypes();
                    if (ls.Count != 0)
                    {
                        label1.Text = "Упоминания:";
                        foreach (SeSchemaItem element in ls)
                        {
                            listView1.Items.Add(element.Name);
                            listView1.Items[listView1.Items.Count - 1].Tag = element;
                        }
                    }

                }
                //изменение ComboBox при нажатии на разные схемы
                TreeNode tn = e.Node;
                while (tn.Parent.Tag is SeSchemaItem)
                {
                    tn = tn.Parent;
                }
                if (tn.Parent.Tag is SeSchema && tn.Parent.Index < comboBox1.Items.Count)
                    comboBox1.SelectedIndex = tn.Parent.Index;

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
                comboBox1.Refresh();
                propertyGrid1.SelectedObject = e.Node.Tag;
                if (e.Node.Index < comboBox1.Items.Count)
                    comboBox1.SelectedIndex = e.Node.Index;
            }
            else { }

        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateNode();
            if (propertyGrid1.SelectedObject is SeSchemaItem)
            {
                //изменение типа у элементов с измененным ComplexType
                SeSchemaItem ssi = (SeSchemaItem)propertyGrid1.SelectedObject;
                if (ssi.Parent is SeSchema && ((ContainerControl)s).ActiveControl.AccessibleName == "Name" && ssi.Type == "")
                    ssi.ChangeNewComplexType(e.OldValue, e.ChangedItem.Value);
            }
        }
        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                SeSchemaItem ssi = (SeSchemaItem)listView1.SelectedItems[0].Tag;
                propertyGrid1.SelectedObject = ssi;
                TreeNode[] treenodesParent = treeView1.Nodes.Find(ssi.GetSchema(ssi).ToString(), false);
                TreeNode[] treenodes = treenodesParent[0].Nodes.Find(ssi.ToString(), true);

                TreeNode eachTn = treenodes[0];
                while (eachTn.Parent.Tag is SeSchemaItem)
                {
                    eachTn.Parent.Expand();
                    eachTn = eachTn.Parent;

                }
                treeView1.Focus();
                treeView1.SelectedNode = treenodes[0];
            }
        }

        private void открытьВсеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
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
                seSchema.ClassToTreeView(treeView1.Nodes);
            }

            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button2.Enabled = true;

        }

        private void открытьНовыеСхемыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\new\"));
            XmlSchemaSet xss = ReadXSD(diXsd);
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView1.Nodes);


            }
            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button2.Enabled = true;
        }

        private void открытьСхемыФТСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\фтс\"));
            XmlSchemaSet xss = ReadXSD(diXsd);
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView1.Nodes);


            }
            ComboBoxBind(seSchemaList);
            BtnToTV.Enabled = true;
            button2.Enabled = true;
        }

        private void сохранитьТекущуюСхемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = null;
            if ((treeView1.SelectedNode != null) && (treeView1.SelectedNode.Tag is SeSchema))
            {
                seSchema = (SeSchema)treeView1.SelectedNode.Tag;
            }
            else
            {
                seSchema = (SeSchema)comboBox1.SelectedItem;
            }
            XmlSchema xs1 = new XmlSchema();
            seSchema.SaveXSD(xs1);
            XmlSchemaSet xss = new XmlSchemaSet();
            ValidationEventHandler ValidationErrorHandler = null;
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\new\")); //?? работает и при "...\xsd\"
            xss.Add(xs1);
            try
            {
                foreach (var fi in diXsd.GetFiles())
                {
                    using (var sr = new StreamReader(fi.FullName))
                    {
                        xss.Add(xs1);
                    }
                }
                //для записи в readonly ElementSchemaType 
                xss.Compile();
            }
            catch { }
            using (FileStream fs = new FileStream(@"..\..\..\..\xsd\new\" + seSchema.Name + "NEW.xsd", FileMode.Create, FileAccess.ReadWrite))
            {
                using (XmlTextWriter tw = new XmlTextWriter(fs, new UTF8Encoding()))
                {
                    tw.Formatting = Formatting.Indented;
                    xs1.Write(tw);
                }
                fs.Close();
            }
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateTreeView(treeView1.Nodes);
        }

        private void схемуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeSchema newSchema = new SeSchema();
            newSchema.ClassToTreeView(treeView1.Nodes);
            UpdateComboBox(newSchema);
        }

        private void элементToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                SeSchemaItem newssi = new SeSchemaItem();
                TreeNode newTreeNode = new TreeNode();
                newTreeNode.Text = newssi.ToString();
                newTreeNode.Tag = newssi;

                treeView1.SelectedNode.Nodes.Add(newTreeNode);
                if (treeView1.SelectedNode.Tag is SeSchema)
                {
                    SeSchema seSchema = (SeSchema)treeView1.SelectedNode.Tag;
                    newssi.Parent = seSchema;
                    seSchema.SchemaItems.Add(newssi);

                }
                else if (treeView1.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;
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
            TreeNode node_here = treeView1.GetNodeAt(e.X, e.Y);
            treeView1.SelectedNode = node_here;

            // Если treenode в данном месте нет
            if (node_here == null)
            {
                ToolStripMenuItem addSchema = new ToolStripMenuItem("Добавить схему");
                contextMenuStrip1.Items.AddRange(new[] { addSchema });
                addSchema.Click += схемуToolStripMenuItem_Click;
                return;
            }
            if (node_here.Tag is SeSchema || node_here.Tag is SeSchemaItem)
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
                seSchema.ClassToTreeView(treeView1.Nodes);
            }

            ComboBoxBind(seSchemaList);

        }
    }
}

