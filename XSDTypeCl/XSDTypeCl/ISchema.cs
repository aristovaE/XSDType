using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Schema;

namespace XSDTypeCl
{
    public interface SeISchema
    {
        void ReadXSD(XmlSchemaElement SchemaElement);

    }
}
