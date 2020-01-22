using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSDTypeCl
{
    public class SchemaItem
    {
        public string name;
        public string type;
        public SchemaItem schemaItem;

        public SchemaItem()
        {

        }
        public SchemaItem(string name, string type, SchemaItem schemaItem)
        {
            this.name = name;
            this.type = type;
            this.schemaItem = schemaItem;
        }
        public SchemaItem(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
        public SchemaItem(string name)
        {
            this.name = name;
        }
    }
}
