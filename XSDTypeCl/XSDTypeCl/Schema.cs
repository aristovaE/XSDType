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
        /// <summary>
        /// Класс SeSchema 
        /// </summary>
        public string Name { get; set; }
        
        public List<SeSchemaItem> schemaItems;
        
        public SeSchema(XmlSchema schema)
        {
            schemaItems = new List<SeSchemaItem>();
            foreach (var sChemaItem in schema.Items)
            {
                ReadXSD(sChemaItem);
            }
        }

        /// <summary>
        /// Запись в класс SeSchemaItem описания из Annotation в XSD
        /// </summary>
        /// <param name="schemaElement">Элемент из List(SeShemaItem)</param>
        /// <returns>Содержимое Annotation->Documentation из файла XSD (string)</returns>
        public string GetAnnotation(XmlSchemaObject schemaElement)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();

            if (schemaElement is XmlSchemaElement)
            {
                XmlSchemaElement schemaElementAnn = schemaElement as XmlSchemaElement;
                discriptionAnn = schemaElementAnn.Annotation;
            }
            else
            {
                XmlSchemaComplexType schemaElementAnn = schemaElement as XmlSchemaComplexType;
                discriptionAnn = schemaElementAnn.Annotation;
            }

            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            if (discriptionAnn != null)
            {
                foreach (XmlSchemaDocumentation MarkupSchemaItem in discriptionAnn.Items)
                {
                    if (MarkupSchemaItem.Markup[0] != null)
                    {
                        return MarkupSchemaItem.Markup[0].Value.ToString();

                    }
                }
            }
            return null;
        }
        
        public void ReadXSD(XmlSchemaObject sChemaItem)
        {
            XmlSchemaElement schemaElement = null;
            XmlSchemaComplexType schemaType = null;
            SeSchemaItem seSchemaItemTable = null;

            if (sChemaItem is XmlSchemaElement)
            {
                List<SeSchemaItem> schemaTypeInCT;
                schemaElement = sChemaItem as XmlSchemaElement;
                Name = schemaElement.Name;

                schemaType = schemaElement.ElementSchemaType as XmlSchemaComplexType;
                seSchemaItemTable = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, schemaTypeInCT = new List<SeSchemaItem>());

                XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                try
                {
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {
                        seSchemaItemTable.ReadXSD(childElement);
                    }
                }
                catch
                { }
            }
            else if (sChemaItem is XmlSchemaComplexType)
            {
                List<SeSchemaItem> schemaTypeInCT;

                schemaType = sChemaItem as XmlSchemaComplexType;
                seSchemaItemTable = new SeSchemaItem(schemaType.Name, GetAnnotation(schemaType), "", schemaTypeInCT = new List<SeSchemaItem>());
                XmlSchemaSequence sequence = schemaType.ContentTypeParticle as XmlSchemaSequence;
                try
                {
                    foreach (XmlSchemaElement childElement in sequence.Items)
                    {
                        seSchemaItemTable.ReadXSD(childElement);
                    }
                }
                catch
                { }

            }

            if (seSchemaItemTable != null)
                schemaItems.Add(seSchemaItemTable);

        }

        public void ClassToTreeView(TreeNodeCollection treeNodes)
        {
            TreeNode newTreeNode = treeNodes.Add(Name);
            foreach (SeSchemaItem schemaItem in schemaItems)
            {
                schemaItem.ClassToTreeView(newTreeNode.Nodes);
                //if (schemaItem.Type != "")
                //{
                //    TreeNode[] tt = (TreeNode[])treeNodes.Find(schemaItem.Type, true).Clone();
                   
                //    if (tt != null)
                //    {
                //        foreach (TreeNode tn in tt)
                //        {
                //            newTreeNode.Nodes.Add(tn);
                //        }
                //    }
                //}
            }
            
            


        }

        public void SaveXSD(XmlSchema xs1)
        {
            xs1.AttributeFormDefault = XmlSchemaForm.Unqualified;
            xs1.ElementFormDefault = XmlSchemaForm.Qualified;
            xs1.Namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");
            foreach (SeSchemaItem newschemaItem in schemaItems)
            {
                XmlSchemaElement newElement = new XmlSchemaElement();
                
                XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
               
                if (newschemaItem.Type != "")
                {
                    XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
                    newElement.Name = newschemaItem.Name;
                    newElement.SchemaTypeName = new XmlQualifiedName(newschemaItem.Type);
                    newElement.Annotation = SetAnnotation(newschemaItem,discriptionAnn);
                    xs1.Items.Add(newElement);

                }
                else
                {
                    XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
                    newSchemaType.Name = newschemaItem.Name;

                    XmlSchemaSequence newSeq= new XmlSchemaSequence();
                    newSchemaType.Particle = newSeq;
                    XmlSchemaElement newElement1 = new XmlSchemaElement();
                  
                    newSchemaType.Annotation = SetAnnotation(newschemaItem, discriptionAnn);
                    if (newschemaItem.SchemaItemsChildren[0].Discription != null)
                    {
                        XmlSchemaAnnotation discriptionAnnEl = new XmlSchemaAnnotation();
                        newElement1.Annotation = SetAnnotation(newschemaItem.SchemaItemsChildren[0], discriptionAnnEl);
                    }
                    foreach (SeSchemaItem seqItem in newschemaItem.SchemaItemsChildren)
                    {
                       
                        newElement1.Name = seqItem.Name;
                        newSeq.Items.Add(newElement1);
                       
                        seqItem.SaveXSD(newElement1, xs1);
                    }
                    xs1.Items.Add(newSchemaType);


                }
            }
        }
        public static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] {doc.CreateTextNode(text)};
        }

        public XmlSchemaAnnotation SetAnnotation(SeSchemaItem newschemaItem, XmlSchemaAnnotation discriptionAnn)
        {
            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            discriptionAnn.Items.Add(discriptionDoc);
            discriptionDoc.Markup = TextToNodeArray(newschemaItem.Discription);
            return discriptionAnn;
           
        }
    }

}
