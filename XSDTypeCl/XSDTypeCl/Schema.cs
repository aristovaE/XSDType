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
        public string name;
        public string discription;
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
        
        public void ReadXSD(XmlSchemaElement schemaElement)
        {
          //  PassportItem(complexType.Name, PassportItem = new List<SchemaItem>()));
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
