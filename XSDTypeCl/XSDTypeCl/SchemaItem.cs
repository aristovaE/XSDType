using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchemaItem : SeISchema
    {
        public string Name;
        public string Discription;
        public string Type;
        public List<SeSchemaItem> SchemaItemsChildren;

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

        public string GetSimpleType(XmlSchemaElement childElement)
        {
            string type;
            XmlSchemaSimpleType simpleType = childElement.ElementSchemaType as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                XmlSchemaSimpleTypeRestriction restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction; // <xsd:all>
                if (restriction != null)
                {
                    type = restriction.BaseTypeName.Name;
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
                            schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), childElement2.SchemaTypeName.Name.ToString()));
                        else schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, GetAnnotation(childElement2), GetSimpleType(childElement2)));

                    }

                }

            }

        }
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


    }
}
