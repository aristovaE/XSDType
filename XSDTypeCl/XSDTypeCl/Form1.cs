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


            treeView2.Nodes.Clear();
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

        public void ClassToTreeView(SeSchema Passport, int i)
        {
            //try
            //{


            //    treeView2.Nodes[nodeIndex].Nodes.Add(Passport.schemaItems[i].name + " (" + Passport.schemaItems[i].Type + ")");

            //    ComplexTypeToTreeView(nodeIndex, i, Passport.schemaItems[i]);
            //    nodeIndex++;
            //}
            //catch { }

        }

        public void ComplexTypeToTreeView(int nodeIndex, int i, SeSchemaItem pI)
        {

            //foreach (SeSchemaItem pI2 in pI.schemaItems)
            //{
            //    try
            //    {

            //        treeView2.Nodes[nodeIndex].Nodes[i].Nodes.Add(pI2.name + " (" + pI2.Type + ")");

            //    }
            //    catch { }
            //}
        }

        private void BtnToTV_Click(object sender, EventArgs e)
        {

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


                // ПРОВЕРКА ПРАВИЛЬНОЙ ЗАПИСИ В КЛАСС
                //try
                //{
                //    MessageBox.Show(seSchema.Name + " have:\n" + seSchema.schemaItems[0].Name
                //        + " \n " + seSchema.schemaItems[1].Name + " \n " + seSchema.schemaItems[2].Name + " \n " + seSchema.schemaItems[3].Name + " \n " + " \n "
                //        + seSchema.schemaItems[0].Name + " have:\n" + seSchema.schemaItems[0].schemaItemsChildren[0].Name + " \n "
                //        + seSchema.schemaItems[1].Name + " have:\n" + seSchema.schemaItems[1].schemaItemsChildren[0].Name + " \n "
                //         + seSchema.schemaItems[2].Name + " have:\n" + seSchema.schemaItems[2].schemaItemsChildren[0].Name + " \n "

                //          + seSchema.schemaItems[0].schemaItemsChildren[0].Name + " have:\n" + seSchema.schemaItems[0].schemaItemsChildren[0].schemaItemsChildren[0].Name + " \n "
                //       + seSchema.schemaItems[1].schemaItemsChildren[0].Name + " have:\n" + seSchema.schemaItems[1].schemaItemsChildren[0].schemaItemsChildren[0].Name + " \n "
                //       + seSchema.schemaItems[2].schemaItemsChildren[0].Name + " have:\n" + seSchema.schemaItems[2].schemaItemsChildren[0].schemaItemsChildren[0].Name + " \n "
                //        );
                //}
                //catch { }

                comboBox1.Items.Add(seSchema.Name);
            }

        }


    }
}

