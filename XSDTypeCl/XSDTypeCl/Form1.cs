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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public XmlSchemaSet ReadXSD()
        {
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));
            //DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\new\")); //для проверки новых схем


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
            catch { }
            return xss;
        }


        private void BtnToTV_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            treeView1.Nodes.Clear();

            if (seSchema != null)
                seSchema.ClassToTreeView(treeView1.Nodes);
        }

        private void BtnXSDToSeSChema_Click(object sender, EventArgs e)
        {
            XmlSchemaSet xss = ReadXSD();
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(treeView1.Nodes);
                
            }
            
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";

            BtnSave.Enabled = true;
            
            //MessageBox.Show("Все доступные схемы прочитаны и добавлены в ComboBox");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            if (e.Node.Tag is SeSchemaItem)
            {
                propertyGrid1.SelectedObject = e.Node.Tag;
            }
            else
            {
                comboBox1.SelectedIndex = e.Node.Index;
            }
            
        }



        private void BtnSave_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            XmlSchema xs1=new XmlSchema();
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
        
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            //SeSchemaItem ssi=(SeSchemaItem)propertyGrid1.SelectedObject;

            //SeSchemaItem ssi2 = (SeSchemaItem)treeView1.SelectedNode.Tag;
            //if (ssi.HasComplexType==true )
            //{
            //    foreach(SeSchemaItem seSchIt in seSchema.SchemaItems)
            //    {
            //       // if(seSchIt.Name)
            //    }
            //}
            UpdateTreeView();
        }

        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            UpdateTreeView();
        }
        public void UpdateTreeView()
        {
            //string path = treeView1.SelectedNode.FullPath;
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            seSchema.ClassToTreeView(treeView1.Nodes);
            treeView1.EndUpdate();
            treeView1.Refresh();
            treeView1.Update();
        }
        private void Button_Add_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag!=null)
            {
                if (treeView1.SelectedNode.Tag is SeSchema)
                {
                    SeSchema ss = (SeSchema)treeView1.SelectedNode.Tag;
                    ss.SchemaItems.Add(new SeSchemaItem());
                }
                else if (treeView1.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;
                    ssi.SchemaItemsChildren.Add(new SeSchemaItem());
                }
            }
            else
            {

            }
            UpdateTreeView();
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                if (treeView1.SelectedNode.Tag is SeSchema)
                {
                }
                else if (treeView1.SelectedNode.Tag is SeSchemaItem)
                {
                    SeSchemaItem ssi = (SeSchemaItem)treeView1.SelectedNode.Tag;
                    SeSchemaItem ssiParent = (SeSchemaItem)treeView1.SelectedNode.Parent.Tag;
                    ssiParent.SchemaItemsChildren.Remove(ssi);
                }
            }
            else
            {

            }
            UpdateTreeView();
        }
    }
}

