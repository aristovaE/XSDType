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

namespace XSDType6
{ //убрать лишнее в treeView 
    public partial class Form1 : Form
    {
        XmlSchemaSet xss = null;
        XmlSchema xs = null;
        XmlSchemas schemas = null;

        XmlDocument doc = new XmlDocument();

        ValidationEventHandler ValidationErrorHandler = null;
        string filePath = @"...\..\..\..\xsd\Passport.xsd";
        DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));
        public Form1()
        {
            InitializeComponent();
            
            XmlTreeDisplay(doc, filePath);
            label1.Text = filePath;
        }
      

        public void XmlTreeDisplay(XmlDocument doc,string filePath)
        {
            treeView1.Nodes.Clear();
            
            // Load the XML Document

            try
            {
                doc.Load(filePath);
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
                return;
            }

            ConvertXmlNodeToTreeNode(doc, treeView1.Nodes);
            treeView1.Nodes[0].ExpandAll();
        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode,TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode = treeNodes.Add(xmlNode.LocalName);
          

            switch (xmlNode.NodeType)
            {
                //case XmlNodeType.XmlDeclaration:
                //    newTreeNode.Text = "<?" + xmlNode.Name + " " +
                //       xmlNode.Value + "?>";
                //    break;
                case XmlNodeType.Element:
                    if (xmlNode.LocalName.ToString() == "simpleType") { newTreeNode.Collapse(); break; }
                    else
                    {
                       // newTreeNode.BackColor = Color.Pink;
                        newTreeNode.Text = xmlNode.LocalName;
                        break;
                    }
                case XmlNodeType.Attribute:
                    if ((xmlNode.LocalName.ToString()!= "name") &&(xmlNode.LocalName.ToString() != "type"))
                    {
                        newTreeNode.Text = xmlNode.LocalName;
                        newTreeNode.Collapse();
                        break;
                    }
                    else
                    {
                        newTreeNode.Text = xmlNode.LocalName;
                        break;
                    }
                case XmlNodeType.Document:
                    newTreeNode.Text = xmlNode.NodeType.ToString();
                    break;
                case XmlNodeType.Text:
                    if (xmlNode.ParentNode.LocalName.ToString() == "documentation")
                    {
                        newTreeNode.Text = xmlNode.Value;
                        newTreeNode.Parent.Parent.Collapse();
                        break;
                    }
                    else
                    {
                        newTreeNode.BackColor = Color.LightGray;
                        newTreeNode.Text = xmlNode.Value;
                        break;
                    }

                case XmlNodeType.Comment:
                    newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
            }
        }



        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {
            //XmlNodeList nodes = doc.GetElementsByTagName(e.Node.Text);
            //textBox1.Text = "";
            //textBox2.Text = "";
            //int index = e.Node.Index;
            //if (nodes.Count > 0)
            //{
            //    XmlAttributeCollection atrCol;
            //    atrCol = nodes[e.Node.Index].Attributes;
            //    if (atrCol.Count > 0)
            //    {
            //        //получаем все атрибуты
            //        foreach (XmlAttribute atr in atrCol)
            //        {
            //            textBox1.Text = atr.Name;
            //            textBox2.Text = atr.Value;
            //        }
            //    }
            //}

            //находим узел по имени
            XmlNodeList nodes = doc.GetElementsByTagName(e.Node.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            int index = e.Node.Index;
            try
            {
                if (e.Node.Text == "xsd:element")
                {
                    textBox1.Text = e.Node.NextVisibleNode.Text;
                    textBox2.Text = e.Node.NextVisibleNode.FirstNode.Text;

                }
                else if (e.Node.Text == "name")
                {
                    textBox1.Text = e.Node.Text;
                    textBox2.Text = e.Node.FirstNode.Text;

                }
                else
                {
                    textBox1.Text = e.Node.Parent.Text;
                    textBox2.Text = e.Node.Text;

                }
            }
            catch(NullReferenceException er)
            {
                MessageBox.Show(er.Message);
                return;
            }
            


        }

       
            
      
        private void button2_Click(object sender, EventArgs e)
        {
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

            foreach (XmlSchema schema in xss.Schemas())
            {
                foreach (var sChemaItem in schema.Items)
                {
                    schemaElement = sChemaItem as XmlSchemaElement;
                    if (schemaElement != null)
                    {


                       // MessageBox.Show("ELEMENT: " + schemaElement.Name);

                        // Get the complex type of the element.
                        XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
                      //  MessageBox.Show("NAME OF COMPLEX TYPE OF " + schemaElement.Name + " = " + complexType.Name);


                        // Get the sequence particle of the complex type.
                        XmlSchemaSequence sequence = complexType.ContentTypeParticle as XmlSchemaSequence;
                        int i = 0;
                        // Iterate over each XmlSchemaElement in the Items collection.
                        foreach (XmlSchemaElement childElement in sequence.Items)
                        {
                            //  MessageBox.Show("ELEMENT OF "+ complexType.Name+" = " + childElement.Name);





                            XmlSchemaComplexType complexType2 = childElement.ElementSchemaType as XmlSchemaComplexType;
                            if (childElement.ElementSchemaType.Name != null)
                            {
                           //     MessageBox.Show("COMPLEX TYPE OF " + childElement.Name + " = " + childElement.ElementSchemaType.Name);
                            }


                            //  MessageBox.Show("NAME OF COMPLEX TYPE OF " + childElement.Name + " = " + complexType2.Name);


                            // DataGridView dataGridView1 = new DataGridView();//Create new grid
                            //dataGridView1.Columns[0].Name = "element";
                            dataGridView1.ColumnCount = 2;
                            // Get the sequence particle of the complex type.

                            XmlSchemaAll all = complexType2.Particle as XmlSchemaAll; // <xsd:all>
                            if (all != null)
                            {
                                dataGridView1.RowCount = all.Items.Count;

                                // Iterate over each XmlSchemaElement in the Items collection.
                                foreach (XmlSchemaElement childElement2 in all.Items)
                                {
                                    if (complexType2.Name != null)
                                    {
                               //         MessageBox.Show("ELEMENT OF " + complexType2.Name + " = " + childElement2.Name);
                                        dataGridView1.Rows[i].Cells[0].Value = childElement2.Name;
                                        dataGridView1.Rows[i].Cells[1].Value = complexType2.Name;
                                    }
                                    else
                                    {// MessageBox.Show("ELEMENT OF COMPLEX TYPE = " + childElement2.Name);
                                    }

                                    dataGridView1.Rows[i].Cells[0].Value = childElement2.Name;
                                    if (childElement2.ElementSchemaType.TypeCode.ToString() != "None")
                                        // MessageBox.Show("TYPE OF " + childElement2.Name + " = " + childElement2.ElementSchemaType.TypeCode);
                                        dataGridView1.Rows[i].Cells[1].Value = childElement2.ElementSchemaType.TypeCode;
                                    else dataGridView1.Rows[i].Cells[1].Value = childElement2.ElementSchemaType.Name;

                                    i++;

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
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox2.Text = dataGridView1.CurrentCell.Value.ToString();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //if (treeView1.SelectedNode != null)
            //{
            //    XmlNodeList nodes;
            //    nodes = doc.GetElementsByTagName(dataGridView1.CurrentCell.Value.ToString());
            //    //получаем индекс
            //    int index = treeView1.SelectedNode.Index;
            //    if (nodes.Count > 0)
            //    {
            //        foreach (XmlAttribute attribute in nodes[index].Attributes)
            //        {
            //            attribute.InnerText = textBox2.Text;
            //        }
            //        //вносим изменения в XML файл
            //        doc.Save(filePath);
            //        MessageBox.Show("!");
            //    }
            //}
            if (treeView1.SelectedNode != null)
            {
                XmlNodeList nodes;
                nodes = doc.GetElementsByTagName(treeView1.SelectedNode.Text);
                //получаем индекс
                int index = treeView1.SelectedNode.Index;
                if (nodes.Count > 0)
                {
                    //  ((System.Xml.XmlAttribute)new System.Linq.SystemCore_EnumerableDebugView(((System.Xml.XmlElement)new System.Linq.SystemCore_EnumerableDebugView(nodes).Items[0]).Attributes).Items[0]).Value

                    nodes[index].Attributes[0].Value = textBox2.Text;

                    //вносим изменения в XSD файл
                   
                    doc.Save(filePath);
                    XmlTreeDisplay(doc, filePath);
                }
            }
        }
    }
}
