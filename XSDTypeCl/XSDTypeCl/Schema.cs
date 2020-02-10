using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XSDTypeCl
{
    public class SeSchema : SeISchema
    {
        /// <summary>
        /// Класс SeSchema 
        /// </summary>
        public string Name { get; set; }

        public List<SeSchemaItem> schemaItems { get; set; }
        public SeSchema(XmlSchema schema)
        {
            schemaItems = new List<SeSchemaItem>();
            foreach (var sChemaItem in schema.Items)
            {
                ReadXSD(sChemaItem);
            }
        }

        /// <summary>
        /// Запись в класс SeSchemaItem описания из Annotation в XSD
        /// </summary>
        /// <param name="schemaElement">Элемент из List(SeShemaItem)</param>
        /// <returns>Содержимое Annotation->Documentation из файла XSD (string)</returns>
        public string GetAnnotation(XmlSchemaObject schemaElement)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();

            if (schemaElement is XmlSchemaElement)
            {
                XmlSchemaElement schemaElementAnn = schemaElement as XmlSchemaElement;
                discriptionAnn = schemaElementAnn.Annotation;
            }
            else
            {
                XmlSchemaComplexType schemaElementAnn = schemaElement as XmlSchemaComplexType;
                discriptionAnn = schemaElementAnn.Annotation;
            }

            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            if (discriptionAnn != null)
            {
                foreach (XmlSchemaDocumentation MarkupSchemaItem in discriptionAnn.Items)
                {
                    if (MarkupSchemaItem.Markup[0] != null)
                    {
                        return MarkupSchemaItem.Markup[0].Value.ToString();

                    }
                }
            }
            return null;
        }

        /// <summary>
        ///Чтение дочерних элементов класса SeSchema и запись их в класс SeSchema
        /// </summary>
        /// <param name="sChemaItem">Считанный корневой элемент считанного XSD</param>
        public void ReadXSD(XmlSchemaObject sChemaItem)
        {
            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;
            SeSchemaItem seSchemaItemTable = null;

            if (sChemaItem is XmlSchemaElement)
            {
                List<SeSchemaItem> schemaTypeInCT;
                schemaElement = sChemaItem as XmlSchemaElement;
                Name = schemaElement.Name;

                seSchemaItemTable = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name,null, true, schemaTypeInCT = new List<SeSchemaItem>());

            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;

                seSchemaItemTable = new SeSchemaItem(schemaType.Name, GetAnnotation(schemaType), "",null,false, schemaTypeInCT = new List<SeSchemaItem>());
                XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                try
                {
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {
                        SeProperties seProp = new SeProperties(childElement);
                        seSchemaItemTable.ReadXSD(childElement);
                    }
                }
                catch
                { }

            }

            if (seSchemaItemTable != null)
            {
                schemaItems.Add(seSchemaItemTable);
            }
        }

        /// <summary>
        /// Запись из класса в treeView
        /// </summary>
        /// <param name="treeNodes">Ссылка на дерево</param>
        public void ClassToTreeView(int i, TreeNodeCollection treeNodes)
        {
            List<TreeNode> nodesList = new List<TreeNode>();
            TreeNode newTreeNode = treeNodes.Add(Name);

            foreach (SeSchemaItem schemaItem in schemaItems)
            {
                schemaItem.ClassToTreeView(newTreeNode.Nodes);
            }
            foreach (TreeNode newNode in newTreeNode.Nodes)
            { nodesList.Add(newNode); }

            EachNode(newTreeNode, nodesList);

            foreach (TreeNode eachTnn in newTreeNode.Nodes)
            {
                foreach (TreeNode eachTn in newTreeNode.Nodes)
                {
                    SeSchemaItem newSsi = (SeSchemaItem)eachTnn.Tag;
                    SeSchemaItem eachSsi = (SeSchemaItem)eachTn.Tag;
                    if (newSsi.HasComplexType == true && eachSsi.HasComplexType == false)
                    {
                        if (newSsi.Type == eachSsi.Name)
                        {
                            TreeNode clonedNode = (TreeNode)eachTn.Clone();
                            eachTnn.Nodes.Insert(0, clonedNode);
                        }


                    }
                }
            }



        }
        public void EachNode(TreeNode eachTnn, List<TreeNode> nodesList)
        {
            foreach (TreeNode nodeTable in nodesList)
            {
                SeSchemaItem ssiTable = (SeSchemaItem)nodeTable.Tag;
                if (ssiTable.HasComplexType == false)
                    foreach (TreeNode nodeElement in nodeTable.Nodes[0].Nodes)
                    {
                        SeSchemaItem ssiElement = (SeSchemaItem)nodeElement.Tag;
                        if (ssiElement.HasComplexType == true)
                        {
                            foreach (TreeNode nodeTable2 in nodesList)
                            {
                                SeSchemaItem ssiTable2 = (SeSchemaItem)nodeTable2.Tag;
                                if (ssiElement.Type == ssiTable2.Name)
                                {
                                    if (nodeElement.Nodes.Count == 0)
                                    {
                                        EachNode2(nodeTable2.Nodes[0], nodesList);
                                        TreeNode clonedNode = (TreeNode)nodeTable2.Clone();
                                        nodeElement.Nodes.Insert(0, clonedNode);
                                        break;
                                    }

                                }
                            }

                        }
                    }
            }
        }

        public void EachNode2(TreeNode eachTnn, List<TreeNode> nodesList)
        {
            foreach (TreeNode nodeTable in nodesList)
            {
                SeSchemaItem ssiTable = (SeSchemaItem)nodeTable.Tag;
                if (ssiTable.HasComplexType == false)
                    foreach (TreeNode nodeElement in eachTnn.Nodes)
                    {
                        SeSchemaItem ssiElement = (SeSchemaItem)nodeElement.Tag;
                        if (ssiElement.HasComplexType == true)
                        {
                            foreach (TreeNode nodeTable2 in nodesList)
                            {

                                SeSchemaItem ssiTable2 = (SeSchemaItem)nodeTable2.Tag;
                                if (ssiElement.Type == ssiTable2.Name)
                                {
                                    if (nodeElement.Nodes.Count == 0)
                                    {
                                        EachNode2(nodeTable2.Nodes[0], nodesList);
                                        TreeNode clonedNode = (TreeNode)nodeTable2.Clone();
                                        nodeElement.Nodes.Insert(0, clonedNode);

                                        break;
                                    }
                                }

                            }
                        }
                    }
            }
            
        }
        /// <summary>
        /// Запись в новый XSD файл содержимого класса SeSchema
        /// </summary>
        /// <param name="xs1">Новый экземпляр схемы</param>
        public void SaveXSD(XmlSchema xs1)
        {
            xs1.AttributeFormDefault = XmlSchemaForm.Unqualified;
            xs1.ElementFormDefault = XmlSchemaForm.Qualified;
            xs1.Namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            foreach (SeSchemaItem newschemaItem in schemaItems)
            {
                XmlSchemaElement newElement = new XmlSchemaElement();


                XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
                //newElement.SchemaType = newSchemaType;

                if (newschemaItem.Type != "")
                {
                    newElement.Name = newschemaItem.Name;
                    newElement.SchemaTypeName = new XmlQualifiedName(newschemaItem.Type);

                    newElement.Annotation = SetAnnotation(newschemaItem);
                    xs1.Items.Add(newElement);
                }
                else
                {
                    newSchemaType.Name = newschemaItem.Name;

                    XmlSchemaSequence newSeq = new XmlSchemaSequence();
                    newSchemaType.Particle = newSeq;
                    XmlSchemaElement newElement1 = new XmlSchemaElement();

                    newSchemaType.Annotation = SetAnnotation(newschemaItem);
                    if (newschemaItem.SchemaItemsChildren[0].Discription != null)
                    {
                        newElement1.Annotation = SetAnnotation(newschemaItem.SchemaItemsChildren[0]);
                    }

                    foreach (SeSchemaItem seqItem in newschemaItem.SchemaItemsChildren)
                    {

                        newElement1.Name = seqItem.Name;
                        newSeq.Items.Add(newElement1);

                        seqItem.SaveXSD(newElement1, xs1);
                    }
                    xs1.Items.Add(newSchemaType);
                }
            }
        }



        /// <summary>
        /// Запись Annotation в новый файл XSD
        /// </summary>
        /// <param name="newschemaItem">Текущий экземпляр класса SeSchemaItem(список List(SeSchemaItem) класса SeSchema</param>
        /// <param name="discriptionAnn">Новый элемент XmlSchemaAnnotation</param>
        /// <returns>Измененный элемент XmlSchemaAnnotation</returns>
        public XmlSchemaAnnotation SetAnnotation(SeSchemaItem newschemaItem)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            discriptionAnn.Items.Add(discriptionDoc);
            discriptionDoc.Markup = TextToNodeArray(newschemaItem.Discription);
            return discriptionAnn;
        }
        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }
    }

}
