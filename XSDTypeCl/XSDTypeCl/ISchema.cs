using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Xml;


namespace XSDTypeCl
{
    public interface SeISchema
    {
        void ReadXSD(XmlSchemaObject SchemaElement);
        string GetAnnotation(XmlSchemaObject SchemaElement);
        void ClassToTreeView(TreeNodeCollection treeNodes);
        XmlSchemaAnnotation SetAnnotation(SeSchemaItem newschemaItem);
        string ToString();
        string GetSimpleType(List<SeSchemaItem> schemaTypeInST, XmlSchemaObject schemaSType);
    }
}
