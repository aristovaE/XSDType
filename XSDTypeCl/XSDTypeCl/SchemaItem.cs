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
        /// 
        /// </summary>
        public string Name;
        public string Discription;
        public string Type;
        public List<SeSchemaItem> SchemaItemsChildren;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Discription"></param>
        /// <param name="Type"></param>
        /// <param name="SchemaItemsChildren"></param>
        public SeSchemaItem(string Name, string Discription, string Type, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
            this.SchemaItemsChildren = SchemaItemsChildren;
        }

        public SeSchemaItem(string Name, string Discription, string Type)
        {
            this.Name = Name;
            this.Discription = Discription;
            this.Type = Type;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaElement"></param>
        /// <returns></returns>
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

        public void GetFacet(List<SeSchemaItem> schemaTypeInST, XmlSchemaFacet facet, XmlSchemaElement childElement)
        {
            schemaTypeInST.Add(new SeSchemaItem("SimpleType", facet.ToString().Split('.')[3], facet.Value));

        }
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
                            GetFacet(schemaTypeInST, facet, childElement);
                        }
                    }
                    return type;
                }

            }
            return null;
        }
        public void ReadXSD(XmlSchemaObject childElement)
        {
            List<SeSchemaItem> schemaTypeInCT;
            XmlSchemaElement schemaElement = null;
            schemaElement = childElement as XmlSchemaElement;
            SchemaItemsChildren.Add(new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, schemaTypeInCT = new List<SeSchemaItem>()));

            XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
            if (complexType != null)
            {
                XmlSchemaAll all = complexType.Particle as XmlSchemaAll; // <xsd:all>
                if (all != null)
                {
                    foreach (XmlSchemaElement childElement2 in all.Items)
                    {

                        if (childElement2.SchemaTypeName.Name.ToString() != "")
                        {

                            schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), childElement2.SchemaTypeName.Name.ToString()));
                        }
                        else
                        {
                            List<SeSchemaItem> schemaTypeInST = new List<SeSchemaItem>();
                            schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), GetSimpleType(schemaTypeInST, childElement2), schemaTypeInST));

                        }

                    }

                }

            }

        }
        //public TreeNode FindNode()
        //{

        //}
        public void ClassToTreeView(TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode;
            if (Type != "")
            {
                if (Discription != null)
                    newTreeNode = treeNodes.Add(Name + " (" + Discription + ") type - " + Type);
                else
                    newTreeNode = treeNodes.Add(Name + " type - " + Type);
            }
            else
                newTreeNode = treeNodes.Add(Name + " (" + Discription + ")");

            

            if (SchemaItemsChildren != null)
            {
                foreach (SeSchemaItem schemaElement in SchemaItemsChildren)
                {
                    schemaElement.ClassToTreeView(newTreeNode.Nodes);
                }
            }

        }


        public void SaveXSD(XmlSchemaElement newElement1, XmlSchema xs1)
        {

            XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
            newElement1.SchemaType = newSchemaType;
            XmlSchemaAll newAll = new XmlSchemaAll();
            newSchemaType.Particle = newAll;
            foreach (SeSchemaItem ssi in SchemaItemsChildren)
            {
                XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
                XmlSchemaElement newElement = new XmlSchemaElement();
                newElement.Name = ssi.Name;
               

                if (ssi.Discription != null)
                {
                    newElement.Annotation = SetAnnotation(ssi, discriptionAnn);
                }
                if (ssi.SchemaItemsChildren != null)
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
                            ml.Value =sstc.Type;
                        }
                        else if (sstc.Discription == "XmlSchemaTotalDigitsFacet")
                        {
                            XmlSchemaTotalDigitsFacet ml = new XmlSchemaTotalDigitsFacet();
                            restriction.Facets.Add(ml);
                            ml.Value = sstc.Type;
                        }
                        else if (sstc.Discription == "XmlSchemaFractionDigitsFacet")
                        {
                            XmlSchemaFractionDigitsFacet ml = new XmlSchemaFractionDigitsFacet();
                            restriction.Facets.Add(ml);
                            ml.Value = sstc.Type;
                        }
                    }
                }
                newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                newAll.Items.Add(newElement);
            }

        }
        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }

        public XmlSchemaAnnotation SetAnnotation(SeSchemaItem newschemaItem, XmlSchemaAnnotation discriptionAnn)
        {
            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            discriptionAnn.Items.Add(discriptionDoc);
            discriptionDoc.Markup = TextToNodeArray(newschemaItem.Discription);
            return discriptionAnn;

        }
    }
}
