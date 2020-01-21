using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSDTypeCl
{
    class Schema
    {
        string name;
        string discription;
        SchemaItem schemaItem;

        public Schema()
        {
            
        }
        public Schema(string name,string discription,SchemaItem schemaItem)
        {
            this.name = name;
            this.discription = discription;
            this.schemaItem = schemaItem;
        }
        public Schema(string name,  SchemaItem schemaItem)
        {
            this.name = name;
            this.schemaItem = schemaItem;
        }
        public void ReadXSD()
        {

        }
    }
  
}
