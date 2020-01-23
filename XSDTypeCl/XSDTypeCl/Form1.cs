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
        int nodeIndex = 0;

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

            XmlDocument doc = new XmlDocument();
           // string xsdName = "Passport.xsd";
            string filePath = @"...\..\..\..\xsd\";

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));


            treeView2.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
            foreach (var fi in diXsd.GetFiles(filePath))
            {
                using (var sr = new StreamReader(fi.FullName))
                {
                    xs = XmlSchema.Read(sr, ValidationErrorHandler);
                    xss.Add(xs);
                    schemas.Add(xs);
                }
                MessageBox.Show("Schema " + fi.Name + " read successully ");

            }
            xss.Compile();
            return xss;
        }
        public void ClassToTreeView(SeSchema Passport,int i)
        {
            try
            {


                treeView2.Nodes[nodeIndex].Nodes.Add(Passport.schemaItems[i].name + " (" + Passport.schemaItems[i].type + ")");
                
                    ComplexTypeToTreeView(nodeIndex,i, Passport.schemaItems[i]);
                    nodeIndex++;
                }
                catch { }
            
        }

        public void ComplexTypeToTreeView(int nodeIndex, int i,SeSchemaItem pI)
        {

            foreach (SeSchemaItem pI2 in pI.schemaItems)
            {
                try
                {

                    treeView2.Nodes[nodeIndex].Nodes[i].Nodes.Add(pI2.name + " (" + pI2.type + ")");

                }
                catch { }
            }
        }

           
        
        public void RecursionForSchemaElement(SeSchema Passport, XmlSchemaElement schemaElement)
        {
            
        }

        public void RecursionForSchemaComplexType()
        {
        }
        private void xsdToTreeViewBtn_Click(object sender, EventArgs e)
        {
            //чтение последнего файла xsd в xss
            XmlSchemaSet xss = ReadXSD();
            XmlSchema xs = xss.Schemas(). //как обратиться к xs в SchemaSet?
                ; 
            SeSchema seSchema;
            SeSchemaItem seSchemaItemTable = new SeSchemaItem("", "", "");
            List<SeSchemaItem> seSchemaItem = new List<SeSchemaItem>();
            List<SeSchemaItem> seSchemaItemInComplexType
            //= new List<SchemaItem>()
            ;
            seSchema = new SeSchema(
                //?
                , seSchemaItem);
            treeView2.Nodes.Add(seSchema.name);

            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;

            int i = 0;
            int y = 0;
            List<List<SeSchemaItem>> complexTypeList = new List<List<SeSchemaItem>>();
            complexTypeList.Add(seSchemaItem);

            foreach (var sChemaItem in xs.Items)
            {

                //schemaElement = sChemaItem as XmlSchemaElement;
                //schemaType = sChemaItem as XmlSchemaComplexType;
                if (sChemaItem is XmlSchemaElement)
                {//annotation
                    schemaElement = sChemaItem as XmlSchemaElement;
                    seSchemaItem.Add(new SeSchemaItem(schemaElement.Name, schemaElement.SchemaTypeName.ToString()));

                    ClassToTreeView(seSchema,i);
                }
                else if (sChemaItem is XmlSchemaComplexType)
                {
                    schemaType = sChemaItem as XmlSchemaComplexType;
                    seSchemaItem.Add(new SeSchemaItem(schemaType.Name,seSchemaItemInComplexType 
                        = new List<SeSchemaItem>()
                        ));

                    XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {
                        //  MessageBox.Show("ELEMENT OF "+ complexType.Name+" = " + childElement.Name);
                        seSchemaItemInComplexType.Add(new SeSchemaItem(childElement.Name, childElement.SchemaTypeName.ToString()));
                        XmlSchemaComplexType complexType2 = childElement.ElementSchemaType as XmlSchemaComplexType;
                        complexTypeList.Add(seSchemaItemInComplexType);
                    }

                    nodeIndex = 0;
                    ClassToTreeView(seSchema,i);


                }
                i++;
            }


            // ПРОВЕРКА ПРАВИЛЬНОЙ ЗАПИСИ В КЛАСС
            //MessageBox.Show(Passport.name + " have:\n" + Passport.schemaItems[0].name + " \n " + Passport.schemaItems[1].name + " \n " + Passport.schemaItems[2].name + " \n " + Passport.schemaItems[3].name + " \n " + " \n "
            //    + Passport.schemaItems[0].name + " have:\n" + Passport.schemaItems[0].schemaItems[0].name + " \n "
            //    + Passport.schemaItems[1].name + " have:\n" + Passport.schemaItems[1].schemaItems[0].name + " \n "
            //     + Passport.schemaItems[2].name + " have:\n" + Passport.schemaItems[2].schemaItems[0].name + " \n "
            //    );



            treeView2.ExpandAll();

        }

       
    }
}

