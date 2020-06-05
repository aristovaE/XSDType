using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchema : SeISchema
    {
        /// <summary>
        /// Класс SeSchema 
        /// </summary>

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Название схемы")]
        public string Name { get; set; }

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Список элементов схемы")]
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
            SeSchemaItem seSchemaItem = null;

            if (sChemaItem is XmlSchemaElement)
            {
                schemaElement = sChemaItem as XmlSchemaElement;
                Name = schemaElement.Name;

                seSchemaItem = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, this, new List<SeSchemaItem>());

            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;

                seSchemaItem = new SeSchemaItem(schemaType.Name, GetAnnotation(schemaType), "", this, schemaTypeInCT = new List<SeSchemaItem>());
                if (schemaType.ContentTypeParticle is XmlSchemaSequence)
                {
                    XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                    try
                    {
                        foreach (var childElement in sequence.Items)
                        {
                            seSchemaItem.ReadXSD(childElement);
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }


            }
            else if (sChemaItem is XmlSchemaSimpleType)
            {
                List<SeSchemaItem> schemaTypeInST = new List<SeSchemaItem>();
                schemaSType = sChemaItem as XmlSchemaSimpleType;
                seSchemaItem = new SeSchemaItem(schemaSType.Name, GetAnnotation(schemaSType),GetSimpleType(schemaTypeInST,schemaSType),this, schemaTypeInST);
            }
            if (seSchemaItem != null)
            {
                SchemaItems.Add(seSchemaItem);
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
            TreeNode newTreeNode = treeNodes.Add(Name);
            newTreeNode.Name = ToString();//для поиска (метод listView)
            newTreeNode.Tag = this;
            newTreeNode.ImageIndex = 0;
            foreach (SeSchemaItem schemaItem in SchemaItems)
            {
                schemaItem.ClassToTreeView(newTreeNode.Nodes);
            }
         
            CloneEachNode(newTreeNode);

        }
        /// <summary>
        /// Копирование веток от complexType к ветке, у которой этот ComplexType
        /// </summary>
        /// <param name="eachTnn">Текущая ветка</param>
        /// <param name="nodesList">Список родительских веток</param>
        private void CloneEachNode(TreeNode eachTnn)
        {
            foreach (TreeNode eachTnnode in eachTnn.Nodes)
            {
                foreach (TreeNode eachTnNode in eachTnn.Nodes)
                {
                    SeSchemaItem newSsi = (SeSchemaItem)eachTnnode.Tag;
                    SeSchemaItem eachSsi = (SeSchemaItem)eachTnNode.Tag;

                    if (newSsi.Type == eachSsi.Name)
                    {
                        eachTnNode.ImageIndex = 2;
                        TreeNode clonedNode = (TreeNode)eachTnNode.Clone();
                        clonedNode.ImageIndex = 2;
                        eachTnnode.Nodes.Insert(0, clonedNode);

                    }
                }
            }

        }

        /// <summary>
        /// Запись в новый XSD файл содержимого класса SeSchema
        /// </summary>
        /// <param name="xs">Новый экземпляр схемы</param>
        public void SaveXSD(XmlSchema xs)
        {
            xs.AttributeFormDefault = XmlSchemaForm.Unqualified;
            xs.ElementFormDefault = XmlSchemaForm.Qualified;
            xs.Namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            foreach (SeSchemaItem newschemaItem in SchemaItems)
            {
                XmlSchemaElement newElement = new XmlSchemaElement();
                XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
                
                if (newschemaItem.Type != "")
                {
                    newElement.Name = newschemaItem.Name;
                    if (newschemaItem.CheckToCommonTypes() == true)
                        newElement.SchemaTypeName = new XmlQualifiedName(newschemaItem.Type, "http://www.w3.org/2001/XMLSchema");
                    else
                        newElement.SchemaTypeName = new XmlQualifiedName(newschemaItem.Type);
                    if (newschemaItem.Description != null && newschemaItem.Description != "")
                    {
                        newElement.Annotation = SetAnnotation(newschemaItem);
                    }
                    xs.Items.Add(newElement);
                }
                else
                {
                    newSchemaType.Name = newschemaItem.Name;
                    XmlSchemaSequence newSeq = new XmlSchemaSequence();
                    newSchemaType.Particle = newSeq;
                    if (newschemaItem.Description != null && newschemaItem.Description != "")
                        newSchemaType.Annotation = SetAnnotation(newschemaItem);
                    if (newschemaItem.SchemaItemsChildren.Count != 0)
                    {
                        foreach (SeSchemaItem seqItem in newschemaItem.SchemaItemsChildren)
                        {
                            XmlSchemaElement newElementSeq = new XmlSchemaElement();
                            newElementSeq.Name = seqItem.Name;
                            newSeq.Items.Add(newElementSeq);
                            seqItem.SaveXSD(newElementSeq, xs);
                            if (newschemaItem.SchemaItemsChildren[0].Description != null && newschemaItem.SchemaItemsChildren[0].Description != "")
                            {
                                newElementSeq.Annotation = SetAnnotation(newschemaItem.SchemaItemsChildren[0]);
                            }
                        }
                        xs.Items.Add(newSchemaType);
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
                    foreach (SeSchemaItem ssiElementChild in ssiElement.SchemaItemsChildren[0].SchemaItemsChildren)
                    {
                        if (ssiElementChild.Name.ToLower().Contains(search) == true)
                        {
                            ssiList.Add(ssiElementChild);
                        }

                    }
            }

            return ssiList;
        }
        
    }
    
}
