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



        private void treeViewBtn_Click(object sender, EventArgs e)
        {
            Schema Passport;
            SchemaItem PassportTable;
            List<SchemaItem> PassportItem;

            List<SchemaItem> PassportItem2;
            Passport = new Schema("Passport", "Паспорт сделки", PassportTable = new SchemaItem("PassportTable", PassportItem = new List<SchemaItem>()));
            PassportItem.Add(new SchemaItem("PassportItem", PassportItem2 = new List<SchemaItem>()));
            PassportItem2.Add(new SchemaItem("DOC_ID", "int"));
            PassportItem2.Add(new SchemaItem("RegNum", "string", "Номер паспорта"));
            PassportItem2.Add(new SchemaItem("RegDate", "dateTime", "Дата паспорта"));

            // Clear the TreeView each time the method is called.
            treeView1.Nodes.Clear();

            treeView1.Nodes.Add(new TreeNode(Passport.name));
            treeView1.Nodes[0].Nodes.Add(new TreeNode(Passport.discription));
            treeView1.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name));
            treeView1.Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name));
            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].name));

            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].type));


            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].name));

            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].type));
            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].discription));

            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].name));


            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].type));
            treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].discription));

            MessageBox.Show("Вручную");
            treeView1.ExpandAll();
        }

        private void xsdToTreeViewBtn_Click(object sender, EventArgs e)
        {
            Schema Passport;
            SchemaItem PassportTable;
            List<SchemaItem> PassportItem;

            List<SchemaItem> PassportItem2;
            //Passport = new Schema("Passport", "Паспорт сделки", PassportTable = new SchemaItem("PassportTable", PassportItem = new List<SchemaItem>()));
            //PassportItem.Add(new SchemaItem("PassportItem", PassportItem2 = new List<SchemaItem>()));
            //PassportItem2.Add(new SchemaItem("DOC_ID", "int"));
            //PassportItem2.Add(new SchemaItem("RegNum", "string", "Номер паспорта"));
            //PassportItem2.Add(new SchemaItem("RegDate", "dateTime", "Дата паспорта"));

            treeView2.Nodes.Clear();
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            XmlDocument doc = new XmlDocument();
            string xsdName = "Passport.xsd";
            string filePath = @"...\..\..\..\xsd\"+xsdName;

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));

            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            int i = 0;
            int y = 0;
            schemas = new XmlSchemas();
            foreach (var fi in diXsd.GetFiles(xsdName)) //Passport.xsd читается полностью
                                                               //разбиение на функции + цикличность?
                                                               //datagridview
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

            XmlSchemaElement schemaElement = null;


            foreach (var sChemaItem in xs.Items)
            {
                schemaElement = sChemaItem as XmlSchemaElement;
                if (schemaElement != null)
                {

                   // treeView2.Nodes.Add(schemaElement.Name + " (Root)");
                   // ((System.Xml.Schema.XmlSchemaType)new System.Linq.SystemCore_EnumerableDebugView(((System.Xml.Schema.XmlSchema)schemaElement.Parent).Items).Items[2]).Name
                    XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;

                    Passport = new Schema(schemaElement.Name, PassportTable = new SchemaItem(complexType.Name, PassportItem = new List<SchemaItem>()));
                    treeView2.Nodes.Add(new TreeNode(Passport.name + " (Root)"));
                    //treeView2.Nodes[0].Nodes.Add(complexType.Name + " (ComplexType)");
                    treeView2.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name + " (ComplexType)"));
                    XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;
                    
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {
                        PassportItem.Add(new SchemaItem(childElement.Name, PassportItem2 = new List<SchemaItem>()));
                      //  treeView2.Nodes[0].Nodes[i].Nodes.Add(childElement.Name + " (element)");

                        treeView2.Nodes[0].Nodes[i].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name + " (element)"));
                        XmlSchemaComplexType complexType2 = childElement.ElementSchemaType as XmlSchemaComplexType;
                        if (childElement.ElementSchemaType.Name != null)
                        {

                           // treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(childElement.ElementSchemaType.Name);
                          //  treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].name));
                           

                        }

                        XmlSchemaAll all = complexType2.Particle as XmlSchemaAll; // <xsd:all>
                        if (all != null)
                        {
                            

                            foreach (XmlSchemaElement childElement2 in all.Items)
                            {
                                
                                if (complexType2.Name != null)
                                {

                                }
                                else
                                {
                                    if (childElement2.ElementSchemaType.TypeCode.ToString() != "None")
                                    {
                                        PassportItem2.Add(new SchemaItem(childElement2.Name, childElement2.ElementSchemaType.TypeCode.ToString()));
                                        //treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(childElement2.Name + " (" + childElement2.ElementSchemaType.TypeCode + ")");
                                        treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(Passport.schemaItem.schemaItems[0].schemaItems[y].name+" ("+Passport.schemaItem.schemaItems[0].schemaItems[y].type+")");
                                    }
                                }

                                y++;
                            }
                            if (complexType2.AttributeUses.Count > 0)
                            {
                                IDictionaryEnumerator enumerator =
                                    complexType2.AttributeUses.GetEnumerator();

                                while (enumerator.MoveNext())
                                {
                                    XmlSchemaAttribute attribute =
                                        (XmlSchemaAttribute)enumerator.Value;

                                    MessageBox.Show("Attribute: " + attribute.Name);
                                }
                            }

                        }

                    }




                }
            }


           
            //treeView2.Nodes[0].Nodes.Add(new TreeNode(Passport.discription));
            //treeView2.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name));
            //treeView2.Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name));
            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].name));

            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].type));


            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].name));

            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].type));
            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].discription));

            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].name));


            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].type));
            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].discription));
            
            treeView2.ExpandAll();

        }
    }
}
