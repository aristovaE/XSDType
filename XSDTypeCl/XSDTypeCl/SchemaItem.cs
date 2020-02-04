using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchemaItem : SeISchema
    {
        /// <summary>
        /// Элемент списка SchemaItems класса SeSchema
        /// </summary>
        public string Name;
        public string Discription;
        public string Type;
        public SeSchemaItem Parent;
        public List<SeSchemaItem> SchemaItemsChildren;

        /// <summary>
        /// Конструкторы класса SeSchemaItem
        /// </summary>
        /// <param name="Name">Имя элемента</param>
        /// <param name="Discription">Описание элемента(Annotation->Documentation)</param>
        /// <param name="Type">Тип элемента(может быть SimpleType)</param>
        /// <param name="SchemaItemsChildren">Список дочерних элементов</param>
        public SeSchemaItem(string Name, string Discription, string Type, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
            this.SchemaItemsChildren = SchemaItemsChildren;
        }
        public SeSchemaItem(string Name, string Discription, string Type,SeSchemaItem Parent, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
            this.Parent = Parent;
            this.SchemaItemsChildren = SchemaItemsChildren;
        }
        public SeSchemaItem(string Name, string Discription, string Type, SeSchemaItem Parent)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
            this.Parent = Parent;
        }
        public SeSchemaItem(string Name, string Discription, string Type)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
        }
        /// <summary>
        /// Запись в класс SeSchemaItem описания из Annotation в XSD
        /// </summary>
        /// <param name="schemaElement">Элемент из List(SeShemaItem)</param>
        /// <returns>Содержимое Annotation->Documentation из файла XSD (string)</returns>
        public string GetAnnotation(XmlSchemaObject schemaElement)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
            //прикрепление XmlSchemaAnnotation к текущему объекту
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
            //Чтение содержимого Annotation 
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
        /// Добавление ограничения элемента XSD в список класса SeSchemaItem
        /// </summary>
        /// <param name="schemaTypeInST">Список, куда добавится facet</param>
        /// <param name="facet">Ограничение</param>
        public void GetFacet(List<SeSchemaItem> schemaTypeInST, XmlSchemaFacet facet)
        {
            schemaTypeInST.Add(new SeSchemaItem("SimpleType", facet.ToString().Split('.')[3], facet.Value));

        }
        /// <summary>
        /// Считывание SimpleType из XSD
        /// </summary>
        /// <param name="schemaTypeInST">Список facetов внутри SimpleType</param>
        /// <param name="childElement">Читаемый element</param>
        /// <returns>Тип считываемого элемента</returns>
        public string GetSimpleType(List<SeSchemaItem> schemaTypeInST, XmlSchemaElement childElement)
        {
            string type;
            XmlSchemaSimpleType simpleType = childElement.ElementSchemaType as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                XmlSchemaSimpleTypeRestriction restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction; // <xsd:all>
                if (restriction != null)
                {
                    type = restriction.BaseTypeName.Name;

                    var enumFacets = restriction.Facets.OfType<XmlSchemaFacet>();

                    if (enumFacets.Any())
                    {

                        foreach (var facet in enumFacets)
                        {
                            GetFacet(schemaTypeInST, facet);
                        }
                    }
                    return type;
                }

            }
            return null;
        }

        /// <summary>
        ///Чтение дочерних элементов класса SeSchemaIyem и запись их в класс SeSchemaIyem
        /// </summary>
        /// <param name="childElement">Считанный дочерний элемент считанного XSD</param>
        public void ReadXSD(XmlSchemaObject childElement)
        {
            List<SeSchemaItem> schemaTypeInCT;
            List<SeSchemaItem> schemaTypeInCT2;
            XmlSchemaElement schemaElement = null;
            SeSchemaItem seSchemaItemTable = null;
            schemaElement = childElement as XmlSchemaElement;

            SeSchemaItem schemaItem = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, this, schemaTypeInCT = new List<SeSchemaItem>());
            SchemaItemsChildren.Add(schemaItem);

            XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
            if (complexType != null)
            {
                XmlSchemaAll all = complexType.Particle as XmlSchemaAll; // <xsd:all>
                if (all != null)
                {
                    foreach (XmlSchemaElement childElement2 in all.Items)
                    {

                        if (childElement2.ElementSchemaType is XmlSchemaComplexType)
                        {
                            XmlSchemaComplexType schemaType = childElement2.ElementSchemaType as XmlSchemaComplexType;
                            seSchemaItemTable = new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), childElement2.SchemaTypeName.Name.ToString(),this, schemaTypeInCT2 = new List<SeSchemaItem>());

                            XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                            try
                            {
                                foreach (XmlSchemaElement childElement3 in sequence.Items)
                                {
                                    seSchemaItemTable.ReadXSD(childElement3);
                                }
                            }
                            catch
                            {
                            }

                            if (seSchemaItemTable != null)
                                schemaTypeInCT.Add(seSchemaItemTable);
                        }
                        else
                        {
                            if (childElement2.SchemaTypeName.Name.ToString() != "")
                            {
                                schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), childElement2.SchemaTypeName.Name.ToString(), schemaItem));
                            }
                            else
                            {
                                List<SeSchemaItem> schemaTypeInST = new List<SeSchemaItem>();
                                SeSchemaItem ssi = new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), GetSimpleType(schemaTypeInST, childElement2), schemaItem, schemaTypeInST);
                                schemaTypeInCT.Add(ssi);

                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Переопределенный метод ToString() для корректного вывода в TreeView
        /// </summary>
        /// <returns>Строку, содержащую сведения о SeSchemaItem</returns>
        public override string ToString()
        {
            if (Type != "" && Type != null)
            {
                if (Discription != "" && Discription != null)
                    return Name + " (" + Discription + ") - " + Type;//this.tostring()
                else
                    return Name + " - " + Type;
            }
            else if (Discription != "" && Discription != null)
                return Name + " (" + Discription + ")";
            else
                return Name;
        }



        /// <summary>
        /// Запись из класса в treeView
        /// </summary>
        /// <param name="treeNodes">Текущая ветка</param>
        public void ClassToTreeView(TreeNodeCollection treeNodes)
        {
            TreeNode newTreeNode;
            
            newTreeNode = treeNodes.Add(ToString());
            //рекурсия (в случае, если у текущего элемента есть дочерние)
            if (SchemaItemsChildren != null)
            {
                foreach (SeSchemaItem schemaElement in SchemaItemsChildren)
                {
                  
                    schemaElement.ClassToTreeView(newTreeNode.Nodes);
                
                }
            }

        }


        /// <summary>
        /// Запись в новый XSD файл содержимого класса SeSchemaItem
        /// </summary>
        /// <param name="newElement1">Новый элемент в схеме</param>
        /// <param name="xs1">Новый экземпляр схемы</param>
        public void SaveXSD(XmlSchemaElement newElement1, XmlSchema xs1)
        {
            XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
            newElement1.SchemaType = newSchemaType;
            XmlSchemaAll newAll = new XmlSchemaAll();
            newSchemaType.Particle = newAll;
            foreach (SeSchemaItem ssi in SchemaItemsChildren)
            {
                XmlSchemaElement newElement = new XmlSchemaElement();
                newElement.Name = ssi.Name;
                if (ssi.Discription != null)
                {
                    newElement.Annotation = SetAnnotation(ssi);
                }
                if (ssi.SchemaItemsChildren != null)
                {
                    if (ssi.SchemaItemsChildren[0].Name == "SimpleType")
                    {
                        XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
                        newElement.SchemaType = simpleType;
                        XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction();
                        simpleType.Content = restriction;
                        restriction.BaseTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                        foreach (SeSchemaItem sstc in ssi.SchemaItemsChildren)
                        {

                            if (sstc.Discription == "XmlSchemaMaxLengthFacet")
                            {
                                XmlSchemaMaxLengthFacet ml = new XmlSchemaMaxLengthFacet();
                                restriction.Facets.Add(ml);
                                ml.Value = sstc.Type;
                            }
                            if (sstc.Discription == "XmlSchemaTotalDigitsFacet")
                            {
                                XmlSchemaTotalDigitsFacet ml = new XmlSchemaTotalDigitsFacet();
                                restriction.Facets.Add(ml);
                                ml.Value = sstc.Type;
                            }
                            if (sstc.Discription == "XmlSchemaFractionDigitsFacet")
                            {
                                XmlSchemaFractionDigitsFacet ml = new XmlSchemaFractionDigitsFacet();
                                restriction.Facets.Add(ml);
                                ml.Value = sstc.Type;
                            }
                        }

                    }
                }
                else newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                newAll.Items.Add(newElement);
            }

        }


        /// <summary>
        /// Запись Annotation в новый файл XSD
        /// </summary>
        /// <param name="newschemaItem">Текущий экземпляр класса SeSchemaItem(список List(SeSchemaItem) класса SeSchemaItem</param>
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
