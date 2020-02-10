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

          //  if (seSchema != null)
               // seSchema.ClassToTreeView(treeView1.Nodes);

            // treeView1.Nodes[0].ExpandAll();


        }

        private void BtnXSDToSeSChema_Click(object sender, EventArgs e)
        {
            XmlSchemaSet xss = ReadXSD();
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            int i = 0;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);
                seSchema.ClassToTreeView(i,treeView1.Nodes);
                
            }
           
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";

            //MessageBox.Show("Все доступные схемы прочитаны и добавлены в ComboBox");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            if (e.Node.Tag != null)
            {
                SeSchemaItem ssi = (SeSchemaItem)e.Node.Tag;

                textBox1.DataBindings.Clear();
                textBox2.DataBindings.Clear();
                textBox3.DataBindings.Clear();
                textBox4.DataBindings.Clear();

                textBox1.DataBindings.Add(new Binding("Text", ssi, "Name", true));
                textBox2.DataBindings.Add(new Binding("Text", ssi, "Type", true));
                textBox3.DataBindings.Add(new Binding("Text", ssi, "Discription", true));

                if (ssi.Parent!=null)
                    textBox4.DataBindings.Add(new Binding("Text", ssi, "Parent.Name", true));

            }
            else
            {
                textBox1.Text = seSchema.Name;
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
            
        }



        private void BtnSave_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            int i = 0;
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

        private void Btn_SaveChanges_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;

            treeView1.Nodes.Clear();
            seSchema.ClassToTreeView(comboBox1.SelectedIndex, treeView1.Nodes);
        }
    }
}

