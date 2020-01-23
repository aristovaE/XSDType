using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchemaItem:SeISchema
    {
        public string name;
        public string discription;
        public string type;
        public List<SeSchemaItem> schemaItems;

        public SeSchemaItem()
        {
            name = "";
            discription = "";
            type = "";
            schemaItems = new List<SeSchemaItem>();
        }
        public SeSchemaItem(string name, string type, List<SeSchemaItem> schemaItems)
        {
            this.name = name;
            this.type = type;
            this.schemaItems = schemaItems;
        }
        public SeSchemaItem(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
        public SeSchemaItem(string name, string discription, string type)
        {
            this.name = name;
            this.discription = discription;
            this.type = type;
        }
        public SeSchemaItem(string name)
        {
            this.name = name;
        }
        public SeSchemaItem(string name,List<SeSchemaItem> schemaItems)
        {
            this.name = name;
            this.schemaItems = schemaItems;
        }
        public void ReadXSD(XmlSchemaElement SchemaElement)
        {

        }
    }

}
