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
using System.Xml.Linq;

namespace XSDType4
{
    //не выводит имена, выводит все элементы
    public partial class Form1 : Form
    {
        XmlSchemaSet xss = null;
        XmlSchema xs = null;
        XmlSchemas schemas = null;

        ValidationEventHandler ValidationErrorHandler = null;
        public Form1()
        {
            InitializeComponent();
            var doc = XDocument.Load(@"...\..\..\..\xsd\Passport.xsd");
            var root = doc.Root;
            var x = GetNodes(new TreeNode(root.Name.LocalName), root).ToArray();

            treeView1.Nodes.AddRange(x);


        }

        private IEnumerable<TreeNode> GetNodes(TreeNode node, XElement element)
        {
            return element.HasElements ?
                node.AddRange(from item in element.Elements()
                              let tree = new TreeNode(item.Name.LocalName)
                              from newNode in GetNodes(tree, item)
                              select newNode)
                              :
                new[] { node };
        }

        public void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            XmlNodeList nodeList;
            try
            {
                if (inXmlNode.HasChildNodes)
                {
                    
                        
                    nodeList = inXmlNode.ChildNodes;
                    
                    for (int i = 0; i < nodeList.Count - 1; i++)
                    {
                        xNode = inXmlNode.ChildNodes[i];
                        //добавляем новый узел в дерево
                        if (nodeList[i].NodeType == XmlNodeType.Element)
                        {
                            inTreeNode.Nodes.Add(new TreeNode(xNode.LocalName));
                        }
                        else
                        {
                            //добавляем значение элемента
                            if (nodeList[i].NodeType == XmlNodeType.Text)
                            {
                                inTreeNode.NextNode.Nodes.Add(new TreeNode("Value",
                                new TreeNode[] { new TreeNode(inXmlNode.InnerText) }));
                            }
                        }
                        //добавляем атрибуты
                        if (xNode.Attributes != null)
                        {
                            foreach (XmlAttribute atr in xNode.Attributes)
                            {
                                inTreeNode.LastNode.Nodes.Add(new TreeNode(atr.Name,
                                new TreeNode[] { new TreeNode(atr.Value) }));
                            }
                        }
                        //рекурсия
                        AddNode(xNode, inTreeNode.Nodes[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
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

            foreach (XmlSchema schema in xss.Schemas())
            {
                foreach (var sChemaItem in schema.Items)
                {
                    schemaElement = sChemaItem as XmlSchemaElement;
                    if (schemaElement != null)
                    {


                        MessageBox.Show("ELEMENT: " + schemaElement.Name);

                        // Get the complex type of the element.
                        XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
                        MessageBox.Show("NAME OF COMPLEX TYPE OF " + schemaElement.Name + " = " + complexType.Name);


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
                                MessageBox.Show("COMPLEX TYPE OF " + childElement.Name + " = " + childElement.ElementSchemaType.Name);
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
                                        MessageBox.Show("ELEMENT OF " + complexType2.Name + " = " + childElement2.Name);
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

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show(e.Node.Text);
        }
    }
    public static class TreeNodeEx
    {
        public static IEnumerable<TreeNode> AddRange(this TreeNode collection, IEnumerable<TreeNode> nodes)
        {
            var items = nodes.ToArray();
            collection.Nodes.AddRange(items);
            return new[] { collection };
        }
    }
}