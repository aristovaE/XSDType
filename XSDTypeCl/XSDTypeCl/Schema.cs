using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Name of XSD")]
        public string Name { get; set; }

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("List of items")]
        public List<SeSchemaItem> SchemaItems { get; set; }
        public SeSchema(XmlSchema schema)
        {

            SchemaItems = new List<SeSchemaItem>();
            if (schema.Elements.Count == 0)
            {
                List<string> name = schema.TargetNamespace.Split(new char[] { ':' }).ToList();
                Name = name[name.Count - 2];
            }
            foreach (var sChemaItem in schema.Items)
            {
                ReadXSD(sChemaItem);
            }
        }
        public SeSchema()
        {
            Name = "Untitled";
            SchemaItems = new List<SeSchemaItem>();
        }

        public override string ToString()
        {
            return Name;
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
            else if (schemaElement is XmlSchemaComplexType)
            {
                XmlSchemaComplexType schemaElementAnn = schemaElement as XmlSchemaComplexType;
                discriptionAnn = schemaElementAnn.Annotation;
            }
            else if (schemaElement is XmlSchemaSimpleType)
            {
                XmlSchemaSimpleType schemaElementAnn = schemaElement as XmlSchemaSimpleType;
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
            XmlSchemaSimpleType schemaSType = null;
            SeSchemaItem seSchemaItemTable = null;

            if (sChemaItem is XmlSchemaElement)
            {
                List<SeSchemaItem> schemaTypeInCT;
                schemaElement = sChemaItem as XmlSchemaElement;
                Name = schemaElement.Name;

                seSchemaItemTable = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, this, schemaTypeInCT = new List<SeSchemaItem>());

            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;

                seSchemaItemTable = new SeSchemaItem(schemaType.Name, GetAnnotation(schemaType), "", this, schemaTypeInCT = new List<SeSchemaItem>());
                if (schemaType.ContentTypeParticle is XmlSchemaSequence)
                {
                    XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                    try
                    {
                        foreach (var childElement in sequence.Items)
                        {
                            seSchemaItemTable.ReadXSD(childElement);
                        }

                    }
                    catch (Exception e)
                    {
                        //MessageBox.Show(e.ToString());
                    }
                }


            }
            else if (sChemaItem is XmlSchemaSimpleType)
            {
                List<SeSchemaItem> schemaTypeInST = new List<SeSchemaItem>();
                schemaSType = sChemaItem as XmlSchemaSimpleType;
                seSchemaItemTable = new SeSchemaItem(schemaSType.Name, GetAnnotation(schemaSType),GetSimpleType(schemaTypeInST,schemaSType),this, schemaTypeInST);
            }
            if (seSchemaItemTable != null)
            {
                SchemaItems.Add(seSchemaItemTable);
            }
        }

         public string GetSimpleType(List<SeSchemaItem> schemaTypeInST, XmlSchemaObject schemaSType)
        {
            XmlSchemaSimpleType schemaST = schemaSType as XmlSchemaSimpleType;
            string type;
            
            if (schemaST != null)
            {
                XmlSchemaSimpleTypeRestriction restriction = schemaST.Content as XmlSchemaSimpleTypeRestriction;
                if (restriction != null)
                {
                    type = restriction.BaseTypeName.Name;

                    var enumFacets = restriction.Facets.OfType<XmlSchemaFacet>();

                    if (enumFacets.Any())
                    {

                        foreach (var facet in enumFacets)
                        {
                            schemaTypeInST.Add(new SeSchemaItem("SimpleType", facet.ToString().Split('.')[3] + "-" + facet.Value, "", this));
                        }
                    }
                    return type;
                }

            }
            return null;
        }

        /// <summary>
        /// Запись из класса в treeView
        /// </summary>
        /// <param name="treeNodes">Ссылка на дерево</param>
        public void ClassToTreeView(TreeNodeCollection treeNodes)
        {
            List<TreeNode> nodesList = new List<TreeNode>();
            TreeNode newTreeNode = treeNodes.Add(Name);
            newTreeNode.Name = ToString();
            newTreeNode.Tag = this;
            newTreeNode.ImageIndex = 0;
            foreach (SeSchemaItem schemaItem in SchemaItems)
            {
                schemaItem.ClassToTreeView(newTreeNode.Nodes);
            }
            foreach (TreeNode newNode in newTreeNode.Nodes)
            {
             
                    nodesList.Add(newNode);
                if (newNode.Nodes.Count != 0)
                {
                    newNode.ImageIndex = 2;
                }
            }
         
            CloneEachNode(newTreeNode, nodesList);

            foreach (TreeNode eachTnn in newTreeNode.Nodes)
            {
                foreach (TreeNode eachTn in newTreeNode.Nodes)
                {
                    SeSchemaItem newSsi = (SeSchemaItem)eachTnn.Tag;
                    SeSchemaItem eachSsi = (SeSchemaItem)eachTn.Tag;
                   
                    if (newSsi.Type == eachSsi.Name)
                    {
                        eachTn.ImageIndex = 2;
                        TreeNode clonedNode = (TreeNode)eachTn.Clone();
                        clonedNode.ImageIndex=2;
                        eachTnn.Nodes.Insert(0, clonedNode);
                        
                    }
                }
            }
            
        }
        /// <summary>
        /// Копирование веток от complexType к ветке, у которой этот ComplexType
        /// </summary>
        /// <param name="eachTnn">Текущая ветка</param>
        /// <param name="nodesList">Список родительских веток</param>
        private void CloneEachNode(TreeNode eachTnn, List<TreeNode> nodesList)
        {
            foreach (TreeNode nodeTable in nodesList)
            {
                SeSchemaItem ssiTable = (SeSchemaItem)nodeTable.Tag;
                if (ssiTable.Type == "")
                    nodeTable.ImageIndex = 2;
                foreach (TreeNode nodeElementItem in nodeTable.Nodes)
                    {
                        foreach (TreeNode nodeElement in nodeElementItem.Nodes)
                        {
                            SeSchemaItem ssiElement = (SeSchemaItem)nodeElement.Tag;
                        foreach (SeSchemaItem.CommonType ct in Enum.GetValues(typeof(SeSchemaItem.CommonType)))
                        {
                            if (ssiElement.Type == ct.ToString())
                       
                            nodeElement.ImageIndex = 1;
                            foreach (TreeNode nodeTable2 in nodesList)
                                {
                                    SeSchemaItem ssiTable2 = (SeSchemaItem)nodeTable2.Tag;
                                    if (ssiElement.Type == ssiTable2.Name)
                                    {
                                        nodeElement.ImageIndex = 2;
                                        if (nodeElement.Nodes.Count == 0)
                                        {
                                            if (nodeTable2.Nodes.Count != 0)
                                            {
                                              //  CloneEachNodeChild(nodeTable2.Nodes[0], nodesList); ?????????????????????????????????????????????????????????
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
            }
        }

        //public void CloneEachNodeChild(TreeNode eachTnn, List<TreeNode> nodesList)
        //{
        //    foreach (TreeNode nodeTable in nodesList)
        //    {
        //        SeSchemaItem ssiTable = (SeSchemaItem)nodeTable.Tag;
        //        if (ssiTable.Type == "")
        //            foreach (TreeNode nodeElement in eachTnn.Nodes)
        //            {
        //                SeSchemaItem ssiElement = (SeSchemaItem)nodeElement.Tag;
        //                if (ssiElement.Type != SeSchemaItem.SimpleType.Decimal.ToString().ToLower() && ssiElement.Type != SeSchemaItem.SimpleType.String.ToString().ToLower() && ssiElement.Type != SeSchemaItem.SimpleType.integer.ToString() && ssiElement.Type != SeSchemaItem.SimpleType.dateTime.ToString())
        //                {
        //                    foreach (TreeNode nodeTable2 in nodesList)
        //                    {

        //                        SeSchemaItem ssiTable2 = (SeSchemaItem)nodeTable2.Tag;
        //                        if (ssiElement.Type == ssiTable2.Name)
        //                        {
        //                            nodeElement.ImageIndex = 2;
        //                            if (nodeElement.Nodes.Count == 0)
        //                            {
        //                                CloneEachNodeChild(nodeTable2.Nodes[0], nodesList);
        //                                TreeNode clonedNode = (TreeNode)nodeTable2.Clone();
        //                                nodeElement.Nodes.Insert(0, clonedNode);
        //                                break;
        //                            }
        //                        }

        //                    }
        //                }
        //            }
        //    }

        //}


        /// <summary>
        /// Запись в новый XSD файл содержимого класса SeSchema
        /// </summary>
        /// <param name="xs1">Новый экземпляр схемы</param>
        public void SaveXSD(XmlSchema xs1)
        {
            //XmlSchemaImport import = new XmlSchemaImport();
            //import.Namespace = "urn:customs.ru:CommonLeafTypes:5.10.0";
            //import.SchemaLocation = @"..\фтс\CommonLeafTypesCust.xsd";
            //xs1.Includes.Add(import);
            xs1.AttributeFormDefault = XmlSchemaForm.Unqualified;
            xs1.ElementFormDefault = XmlSchemaForm.Qualified;
            xs1.Namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            foreach (SeSchemaItem newschemaItem in SchemaItems)
            {
                XmlSchemaElement newElement = new XmlSchemaElement();
                XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
                //newElement.SchemaType = newSchemaType;
                if (newschemaItem.Type != "")
                {
                    newElement.Name = newschemaItem.Name;
                    newschemaItem.CheckToCommonTypes(newschemaItem, newElement);
                    if (newschemaItem.Description != null && newschemaItem.Description != "")
                    {
                        newElement.Annotation = SetAnnotation(newschemaItem);
                    }
                    xs1.Items.Add(newElement);
                }
                else
                {
                    newSchemaType.Name = newschemaItem.Name;

                    XmlSchemaSequence newSeq = new XmlSchemaSequence();
                    newSchemaType.Particle = newSeq;
                    XmlSchemaElement newElement1 = new XmlSchemaElement();
                    if (newschemaItem.Description != null && newschemaItem.Description != "")
                        newSchemaType.Annotation = SetAnnotation(newschemaItem);
                    if (newschemaItem.SchemaItemsChildren.Count != 0)
                    {
                        foreach (SeSchemaItem seqItem in newschemaItem.SchemaItemsChildren)
                        {
                            XmlSchemaElement newElementSeq = new XmlSchemaElement();
                            newElementSeq.Name = seqItem.Name;
                            newSeq.Items.Add(newElementSeq);
                            seqItem.SaveXSD(newElementSeq, xs1);
                            if (newschemaItem.SchemaItemsChildren[0].Description != null && newschemaItem.SchemaItemsChildren[0].Description != "")
                            {
                                newElementSeq.Annotation = SetAnnotation(newschemaItem.SchemaItemsChildren[0]);
                            }
                        }
                        xs1.Items.Add(newSchemaType);
                    }

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
            discriptionDoc.Markup = TextToNodeArray(newschemaItem.Description);
            return discriptionAnn;
        }
        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }

        /// <summary>
        /// Поиск элементов, имя которого содержит слово из поисковой строки
        /// </summary>
        /// <param name="search">Искомое слово</param>
        /// <returns></returns>
        public List<SeSchemaItem> FindElements(string search)
        {
            List<SeSchemaItem> ssiList = new List<SeSchemaItem>();
            foreach (SeSchemaItem ssiElement in SchemaItems)
            {
                if (ssiElement.Name.ToLower().Contains(search) == true)
                {
                    ssiList.Add(ssiElement);
                }
                if (ssiElement.SchemaItemsChildren.Count != 0)
                    foreach (SeSchemaItem ssiElement2 in ssiElement.SchemaItemsChildren[0].SchemaItemsChildren)
                    {
                        if (ssiElement2.Name.ToLower().Contains(search) == true)
                        {
                            ssiList.Add(ssiElement2);
                        }

                    }
            }
            return ssiList;
        }
        
    }
    
}
