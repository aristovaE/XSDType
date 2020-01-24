using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchemaItem : SeISchema
    {
        public string Name;
        public string Discription;
        public string Type;
        public List<SeSchemaItem> SchemaItemsChildren;


        public SeSchemaItem(string Name, string Type)
        {
            this.Name = Name;
            this.Type = Type;
        }
        public SeSchemaItem(string Name)
        {
            this.Name = Name;
        }

        public SeSchemaItem(string Name, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.SchemaItemsChildren = SchemaItemsChildren;
        }
        public SeSchemaItem(string Name, string Type, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.Type = Type;
            this.SchemaItemsChildren = SchemaItemsChildren;
        }
        public void ReadXSD(XmlSchemaObject childElement)
        {
            List<SeSchemaItem> schemaTypeInCT;
            XmlSchemaElement schemaElement = null;
            schemaElement = childElement as XmlSchemaElement;
            SchemaItemsChildren.Add(new SeSchemaItem(schemaElement.Name, schemaElement.SchemaTypeName.ToString(),schemaTypeInCT = new List<SeSchemaItem>()));

            XmlSchemaComplexType complexType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
            if (complexType != null)
            {
                XmlSchemaAll all = complexType.Particle as XmlSchemaAll; // <xsd:all>
                if (all != null)
                {
                    foreach (XmlSchemaElement childElement2 in all.Items)
                    {
                        
                        schemaTypeInCT.Add(new SeSchemaItem(childElement2.Name, childElement2.SchemaTypeName.ToString()));
                    }

                }

            }

        }
        public void ReadXSD2(XmlSchemaAll childElement)
        {
            
              //      schemaItemsChildren.Add(new SeSchemaItem(childElement2.Name, childElement2.SchemaTypeName.ToString()));
         
          
        }

    }
}
