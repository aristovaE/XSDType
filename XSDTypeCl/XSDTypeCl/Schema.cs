using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSDTypeCl
{
    class Schema
    {
        public string name;
        public string discription;
        public List<SchemaItem> schemaItems;

        public Schema()
        {
            
        }
        public Schema(string name,string discription, List<SchemaItem> schemaItems)
        {
            this.name = name;
            this.discription = discription;
            this.schemaItems = schemaItems;
        }
        public Schema(string name, List<SchemaItem> schemaItems)
        {
            this.name = name;
            this.schemaItems = schemaItems;
        }
        public void ReadXSD()
        {

        }
    }
  
}
