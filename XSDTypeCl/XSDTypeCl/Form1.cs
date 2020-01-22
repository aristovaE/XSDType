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
            treeView2.Nodes.Clear();
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            XmlDocument doc = new XmlDocument();
            string filePath = @"...\..\..\..\xsd\Passport.xsd";

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));

            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();

            schemas = new XmlSchemas();
            foreach (var fi in diXsd.GetFiles("Passport.xsd")) //Passport.xsd читается полностью
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

                    treeView2.Nodes.Add(schemaElement.Name +" (Root)");
                    //MessageBox.Show("ELEMENT: " + schemaElement.Name);

                    // Get the complex type of the element.
                    XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
                    treeView2.Nodes[0].Nodes.Add(complexType.Name + " (ComplexType)");
                    //MessageBox.Show("NAME OF COMPLEX TYPE OF " + schemaElement.Name + " = " + complexType.Name);


                    // Get the sequence particle of the complex type.
                    XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;
                    int i = 0;
                    // Iterate over each XmlSchemaElement in the Items collection.
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {

                        treeView2.Nodes[0].Nodes[i].Nodes.Add(childElement.Name + " (element)");

                        //MessageBox.Show("ELEMENT OF " + complexType.Name + " = " + childElement.Name);



                        XmlSchemaComplexType complexType2 = childElement.ElementSchemaType as XmlSchemaComplexType;
                        if (childElement.ElementSchemaType.Name != null)
                        {
                             //   MessageBox.Show("COMPLEX TYPE OF " + childElement.Name + " = " + childElement.ElementSchemaType.Name);
                            treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(childElement.ElementSchemaType.Name);
                        }


                        //  MessageBox.Show("NAME OF COMPLEX TYPE OF " + childElement.Name + " = " + complexType2.Name);


                        // DataGridView dataGridView1 = new DataGridView();//Create new grid
                        //dataGridView1.Columns[0].Name = "element";
                        //dataGridView1.ColumnCount = 2;
                        // Get the sequence particle of the complex type.

                        XmlSchemaAll all = complexType2.Particle as XmlSchemaAll; // <xsd:all>
                        if (all != null)
                        {
                            //dataGridView1.RowCount = all.Items.Count;
                            int y = 0;
                            // Iterate over each XmlSchemaElement in the Items collection.
                            foreach (XmlSchemaElement childElement2 in all.Items)
                            {
                                if (complexType2.Name != null)
                                {
                                    //if (childElement2.ElementSchemaType.TypeCode.ToString() != "None")
                                    //    MessageBox.Show("ELEMENT OF " + complexType2.Name + " = " + childElement2.Name);
                                    //treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(childElement2.Name);

                                    //dataGridView1.Rows[i].Cells[0].Value = childElement2.Name;
                                    //dataGridView1.Rows[i].Cells[1].Value = complexType2.Name;
                                }
                                else
                                {
                                    if (childElement2.ElementSchemaType.TypeCode.ToString() != "None")

                                        treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(childElement2.Name+" ("+ childElement2.ElementSchemaType.TypeCode + ")");

                                    
                                    //MessageBox.Show("ELEMENT OF COMPLEX TYPE = " + childElement2.Name);

                                }

                                //dataGridView1.Rows[i].Cells[0].Value = childElement2.Name;
                                
                                    // MessageBox.Show("TYPE OF " + childElement2.Name + " = " + childElement2.ElementSchemaType.TypeCode);
                                    //    dataGridView1.Rows[i].Cells[1].Value = childElement2.ElementSchemaType.TypeCode;
                                    //else dataGridView1.Rows[i].Cells[1].Value = childElement2.ElementSchemaType.Name;

                                    //i++;
                                //y++;

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



            treeView2.ExpandAll();

        }
    }
}
