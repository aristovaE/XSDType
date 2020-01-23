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
        XmlSchemaSet xss = null;
        XmlSchema xs = null;
        XmlSchemas schemas = null;
        ValidationEventHandler ValidationErrorHandler = null;

        XmlDocument doc = new XmlDocument();
        static string xsdName = "AirPassList.xsd";
        string filePath = @"...\..\..\..\xsd\" + xsdName;

        DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));


        public Form1()
        {
            InitializeComponent();
        }
        
        public void ReadXSD()
        {
            treeView2.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
            foreach (var fi in diXsd.GetFiles(xsdName))
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
        }
        public void ClassToTreeView(Schema Passport, List<SchemaItem> PassportItem)
        {

            treeView2.Nodes.Add(Passport.name);
            foreach (SchemaItem pI in PassportItem)
            { treeView2.Nodes[0].Nodes.Add(pI.name); }
            //treeView2.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name));
            //treeView2.Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name));
            //treeView2.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].name));
        }
        public void RecursionForSchemaElement(Schema Passport,XmlSchemaElement schemaElement)
        {
            //XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
            
            ////treeView2.Nodes.Add(new TreeNode(Passport.name + " (Root)"));
            ////treeView2.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name + " (ComplexType)"));
            //XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;

            //Passport.ReadXSD(schemaElement);
        }

        public void RecursionForSchemaComplexType()
        {

        }
        private void xsdToTreeViewBtn_Click(object sender, EventArgs e)
        {
            ReadXSD();
            Schema Passport;
            SchemaItem PassportTable=new SchemaItem("","","");
            List<SchemaItem> PassportItem;
            List<SchemaItem> PassportItemInCT;
            Passport = new Schema(xsdName, PassportItem=new List<SchemaItem>()) ;

            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;

            int i = 0;
            int y = 0;

            foreach (var sChemaItem in xs.Items)
            {

                //schemaElement = sChemaItem as XmlSchemaElement;
                //schemaType = sChemaItem as XmlSchemaComplexType;
                if (sChemaItem is XmlSchemaElement)
                {
                    //RecursionForSchemaElement(Passport, schemaElement);
                    schemaElement = sChemaItem as XmlSchemaElement;
                    PassportItem.Add(new SchemaItem(schemaElement.Name, schemaElement.SchemaTypeName.ToString()));

                }
                else if (sChemaItem is XmlSchemaComplexType)
                {
                    schemaType = sChemaItem as XmlSchemaComplexType;
                    PassportItem.Add(new SchemaItem(schemaType.Name, PassportItemInCT = new List<SchemaItem>()));

                }
                
            }
            //foreach (SchemaItem pI in PassportItem)
            //{ MessageBox.Show(pI.name + " " + pI.discription + " " + pI.type); }
            //    foreach (XmlSchemaElement childElement in sequence.Items)
            //    {
            //        PassportItem.Add(new SchemaItem(childElement.Name, PassportItem2 = new List<SchemaItem>()));

            //        treeView2.Nodes[0].Nodes[i].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name + " (element)"));
            //        XmlSchemaComplexType complexType2 = childElement.ElementSchemaType as XmlSchemaComplexType;
            //        if (childElement.ElementSchemaType.Name != null)
            //        {

            //        }

            //        XmlSchemaAll all = complexType2.Particle as XmlSchemaAll; // <xsd:all>
            //        if (all != null)
            //        {


            //            foreach (XmlSchemaElement childElement2 in all.Items)
            //            {

            //                if (complexType2.Name != null)
            //                {

            //                }
            //                else
            //                {

            //                        PassportItem2.Add(new SchemaItem(childElement2.Name, childElement2.ElementSchemaType.TypeCode.ToString()));
            //                        treeView2.Nodes[0].Nodes[i].Nodes[0].Nodes.Add(Passport.schemaItem.schemaItems[0].schemaItems[y].name+" ("+Passport.schemaItem.schemaItems[0].schemaItems[y].type+")");

            //                }

            //                y++;
            //            }
            //            if (complexType2.AttributeUses.Count > 0)
            //            {
            //                IDictionaryEnumerator enumerator =
            //                    complexType2.AttributeUses.GetEnumerator();

            //                while (enumerator.MoveNext())
            //                {
            //                    XmlSchemaAttribute attribute =
            //                        (XmlSchemaAttribute)enumerator.Value;

            //                    MessageBox.Show("Attribute: " + attribute.Name);
            //                }
            //            }

            //        }

            ClassToTreeView(Passport, PassportItem);
            treeView2.ExpandAll();

        }

        //treeView1.Nodes.Clear();

        //treeView1.Nodes.Add(new TreeNode(Passport.name));
        //treeView1.Nodes[0].Nodes.Add(new TreeNode(Passport.discription));
        //treeView1.Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.name));
        //treeView1.Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].name));
        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].name));

        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[0].type));


        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].name));

        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].type));
        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[1].discription));

        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].name));


        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].type));
        //treeView1.Nodes[0].Nodes[1].Nodes[0].Nodes[2].Nodes.Add(new TreeNode(Passport.schemaItem.schemaItems[0].schemaItems[2].discription));



    }
}

