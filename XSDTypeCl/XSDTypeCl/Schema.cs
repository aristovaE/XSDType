using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace XSDTypeCl
{
    public class SeSchema : SeISchema
    {

        public string Name { get; set; }
        public string Discription { get; set; }
        public List<SeSchemaItem> schemaItems;
        
        public SeSchema(XmlSchema schema)
        {
            schemaItems = new List<SeSchemaItem>(); //без определения schemaItems не работает
            foreach (var sChemaItem in schema.Items)
            {
                ReadXSD(sChemaItem);
            }
        }

        public void ReadXSD(XmlSchemaObject sChemaItem)
        {
            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;
            SeSchemaItem seSchemaItemTable = null;
            SeSchemaItem seSchemaItemTable2 = null;

            if (sChemaItem is XmlSchemaElement)
            {//annotation

                schemaElement = sChemaItem as XmlSchemaElement;
                Name = schemaElement.Name;
                seSchemaItemTable = new SeSchemaItem(schemaElement.Name, schemaElement.SchemaTypeName.ToString());
                
            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;
                seSchemaItemTable = new SeSchemaItem(schemaType.Name, schemaTypeInCT = new List<SeSchemaItem>());
                seSchemaItemTable2 = new SeSchemaItem(schemaType.Name, schemaTypeInCT = new List<SeSchemaItem>());
                XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                foreach (XmlSchemaElement childElement in sequence.Items)
                {
                    seSchemaItemTable.ReadXSD(childElement);
                }

            }
            
            if(seSchemaItemTable!=null)
                schemaItems.Add(seSchemaItemTable);
           
        }

        public void FillTree(TreeNodeCollection treeNodes)
        {
            // TreeNode newTreeNode = treeNodes.Add(xmlNode.LocalName); 
        }

    }

}
