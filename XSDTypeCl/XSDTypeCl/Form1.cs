using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;

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


            treeView1.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
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
            return xss;
        }


        private void BtnToTV_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            treeView1.Nodes.Clear();

            if (seSchema != null)
                treeView1.Nodes.Add(seSchema.Name);
            ConvertXmlNodeToTreeNode(seSchema.schemaItems, treeView1.Nodes[0].Nodes);

            treeView1.Nodes[0].ExpandAll();


        }
        

        private void ConvertXmlNodeToTreeNode(List<SeSchemaItem> schemaItems, TreeNodeCollection treeNodes)
        {
            foreach (SeSchemaItem eachSchemaItem in schemaItems)
            {
                TreeNode newTreeNode;
                if (eachSchemaItem.Type != "")
                {
                    newTreeNode = treeNodes.Add(eachSchemaItem.Name + " (" + eachSchemaItem.Discription + ") type- " + eachSchemaItem.Type);
                }
                else
                    newTreeNode = treeNodes.Add(eachSchemaItem.Name + " (" + eachSchemaItem.Discription + ")" );
            }
        }

        private void xsdToTreeViewBtn_Click(object sender, EventArgs e) //чтение и запись в класс
        {

            XmlSchemaSet xss = ReadXSD();
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                
            }

            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";

            MessageBox.Show("Все доступные схемы прочитаны и добавлены в ComboBox");
        }


    }
}

