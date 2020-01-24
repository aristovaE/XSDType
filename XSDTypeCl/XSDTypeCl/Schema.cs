using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XSDTypeCl
{
    public class SeSchema : SeISchema,IEnumerable<SeSchema>
    {
        List<SeSchema> schemas;
        public string name { get; set; }
        public string discription { get; set; }
        public List<SeSchemaItem> schemaItems;

        public SeSchema()
        {
            name = "";
            discription = "";
            schemaItems = new List<SeSchemaItem>();
        }
        public SeSchema(string name,string discription, List<SeSchemaItem> schemaItems)
        {
            this.name = name;
            this.discription = discription;
            this.schemaItems = schemaItems;
        }
        public SeSchema(string name, List<SeSchemaItem> schemaItems)
        {
            this.name = name;
            this.schemaItems = schemaItems;
        }
        public SeSchema(List<SeSchemaItem> schemaItems)
        {
            this.schemaItems = schemaItems;
        }
        public void ReadXSD(XmlSchemaObject sChemaItem)
        {
            int i;
            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;
            SeSchemaItem seSchemaItemTable = null;

            if (sChemaItem is XmlSchemaElement)
            {//annotation

                schemaElement = sChemaItem as XmlSchemaElement;
                name = schemaElement.Name;
                seSchemaItemTable = new SeSchemaItem(schemaElement.Name, schemaElement.SchemaTypeName.ToString());
               
                
            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;
                seSchemaItemTable = new SeSchemaItem(schemaType.Name, schemaTypeInCT = new List<SeSchemaItem>());
              
                XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                foreach (XmlSchemaElement childElement in sequence.Items)
                {
                    seSchemaItemTable.ReadXSD(childElement);
                    
                    // schemaTypeInCT.ReadXSD();
                    // -> complexType -> all -> element

                }
                //nodeIndex = 0;
                // ClassToTreeView(seSchema, i);


            }
            schemaItems.Add(seSchemaItemTable);
        }

        //fillTree
        public IEnumerator<SeSchema> GetEnumerator()
        {
            return schemas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
  
}
