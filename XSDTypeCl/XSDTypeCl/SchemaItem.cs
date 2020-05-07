using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeSchemaItem : SeISchema
    {
        /// <summary>
        /// Элемент списка SchemaItems класса SeSchema
        /// </summary>
        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Name of SchemaItem")]
        public string Name { get; set; }

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Descriprion of SchemaItem")]
        public string Description { get; set; }

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("Type of SchemaItem")]
        [TypeConverter(typeof(MyConverter))]
        public string Type { get; set; }
        [Flags]
        public enum SimpleType
        {
            String = 1,
            integer = 2,
            Decimal = 4, //SimpleType.Decimal.ToString().ToLower()
            dateTime = 8
        }
        public class MyConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext
            context)
            {
                //true means show a combobox
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext
            context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }

            public override
            StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                List<string> str = new List<string>();
                str.Add("");
                foreach (SimpleType simpletype in Enum.GetValues(typeof(SimpleType)))
                {
                    str.Add(simpletype.ToString().ToLower());
                }
                SeSchemaItem ssi = (SeSchemaItem)context.Instance;
                while (ssi.Parent is SeSchemaItem)
                {
                    ssi = (SeSchemaItem)ssi.Parent;
                }

                SeSchema ss = (SeSchema)ssi.Parent;
                foreach (SeSchemaItem ssiTable in ss.SchemaItems)
                {
                    if (ssiTable.Type == "")
                    {
                        str.Add(ssiTable.Name);
                    }
                }

                return new StandardValuesCollection(str);
            }

        }
        [ReadOnly(true)]
        [Category("Properties")]
        [Description("Parent of this SeSchemaItem")]
        public object Parent { get; set; } //тип object для SeSchemaItem И SeSchema

        [ReadOnly(false)]
        [Category("Properties")]
        [Description("SchemaItems of SchemaItem")]
        public List<SeSchemaItem> SchemaItemsChildren { get; set; }

        [Browsable(false)]
        private SeProperties Properties { get; set; }

        /// <summary>
        /// Конструктор класса SeSchemaItem
        /// </summary>
        /// <param name="Name">Имя элемента</param>
        /// <param name="Description">Описание элемента(Annotation->Documentation)</param>
        /// <param name="Type">Тип элемента(может быть ComplexType)</param>  
        /// <param name="Parent">Родитель класса</param>  
        /// <param name="HasComplexType">Имеет или не имеет ComplexType (Для корректного сохранения)</param>
        /// <param name="SchemaItemsChildren">Список дочерних элементов</param>

        public SeSchemaItem(string Name, string Description, string Type, object Parent, List<SeSchemaItem> SchemaItemsChildren)
        {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.Parent = Parent;
            this.SchemaItemsChildren = SchemaItemsChildren;

        }

        private SeSchemaItem(string Name, string Description, string Type, object Parent, List<SeSchemaItem> SchemaItemsChildren, SeProperties Properties)
        {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.Parent = Parent;
            this.SchemaItemsChildren = SchemaItemsChildren;
            this.Properties = Properties;
        }
        /// <summary>
        /// Конструктор класса SeSchemaItem для определения SimpleType
        /// </summary>
        /// <param name="Name">"SimpleType"</param>
        /// <param name="Discription">Полное название ограничения</param>
        /// <param name="Type">Значение ограничения</param>  
        /// <param name="Parent">Класс-владелец Simple Type</param>  
        private SeSchemaItem(string Name, string Description, string Type, object Parent)
        {
            this.Name = Name;
            this.Description = Description;
            this.Type = Type;
            this.Parent = Parent;

        }

        public SeSchemaItem()
        {
            Name = "Untitled";
            Description = null;
            Type = "string";
            SchemaItemsChildren = new List<SeSchemaItem>();
            Properties = new SeProperties();
        }
        /// <summary>
        /// Запись в класс SeSchemaItem описания из <xsd:Annotation>
        /// </summary>
        /// <param name="schemaElement">Элемент из List(SeShemaItem)</param>
        /// <returns>Содержимое Annotation->Documentation из файла XSD (string)</returns>
        public string GetAnnotation(XmlSchemaObject schemaElement)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
            //прикрепление XmlSchemaAnnotation к текущему объекту
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
            //Чтение содержимого Annotation 
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

        /// <summary>
        /// Добавление свойств элемента XSD в список класса SeSchemaItem
        /// </summary>
        /// <param name="schemaTypeInST">Список, куда добавится facet</param>
        /// <param name="facet">Ограничение</param>
        private void GetFacet(List<SeSchemaItem> schemaTypeInST, XmlSchemaFacet facet)
        {
            schemaTypeInST.Add(new SeSchemaItem("SimpleType", facet.ToString().Split('.')[3] + "-" + facet.Value, "", this));

        }
        /// <summary>
        /// Считывание SimpleType из XSD
        /// </summary>
        /// <param name="schemaTypeInST">Список facetов внутри SimpleType</param>
        /// <param name="childElement">Читаемый element</param>
        /// <returns>Тип считываемого элемента</returns>
        private string GetSimpleType(List<SeSchemaItem> schemaTypeInST, XmlSchemaElement childElement)
        {
            string type;
            XmlSchemaSimpleType simpleType = childElement.ElementSchemaType as XmlSchemaSimpleType;
            if (simpleType != null)
            {
                XmlSchemaSimpleTypeRestriction restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
                if (restriction != null)
                {
                    type = restriction.BaseTypeName.Name;

                    var enumFacets = restriction.Facets.OfType<XmlSchemaFacet>();

                    if (enumFacets.Any())
                    {

                        foreach (var facet in enumFacets)
                        {
                            GetFacet(schemaTypeInST, facet);
                        }
                    }
                    return type;
                }

            }
            return null;
        }

        /// <summary>
        ///Чтение дочерних элементов класса SeSchemaIyem и запись их в класс SeSchemaIyem
        /// </summary>
        /// <param name="childElement">Считанный дочерний элемент считанного XSD</param>
        public void ReadXSD(XmlSchemaObject childElement)
        {
            List<SeSchemaItem> schemaTypeInCT;
            List<SeSchemaItem> schemaTypeInChoice;
            XmlSchemaElement schemaElement = null;
            XmlSchemaSequence seq = null;
            if (childElement is XmlSchemaChoice)
            {
                SchemaItemsChildren.Add(new SeSchemaItem("CHOICE", "", "", this, schemaTypeInChoice = new List<SeSchemaItem>(), null));
                var choice = (XmlSchemaChoice)childElement;
                if (choice.Items[0] is XmlSchemaSequence)
                {
                    seq = (XmlSchemaSequence)choice.Items[0];
                    foreach (XmlSchemaElement childElement2 in seq.Items)
                    {
                        ReadXSD(childElement2);
                    }
                }
                if (seq == null)
                {
                    foreach (XmlSchemaObject childElement2 in choice.Items)
                    {

                        if (childElement2 is XmlSchemaElement)
                        {
                            schemaElement = childElement2 as XmlSchemaElement;
                            SeProperties seProp2 = new SeProperties(schemaElement);
                            schemaTypeInChoice.Add(new SeSchemaItem(schemaElement.Name, GetAnnotation(childElement2), schemaElement.SchemaTypeName.Name, this, schemaTypeInCT = new List<SeSchemaItem>(), seProp2));
                        }
                        else
                        {
                            seq = (XmlSchemaSequence)childElement2;
                            foreach (XmlSchemaElement childElementSeq in seq.Items)
                            {
                                ReadXSD(childElementSeq);
                            }
                        }
                    }
                }

            }
            else if (childElement is XmlSchemaElement)
            {
                schemaElement = childElement as XmlSchemaElement;
                SeProperties seProp = new SeProperties(schemaElement);
                //<xsd:element name="..._ITEM">
                SeSchemaItem schemaItem = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), schemaElement.SchemaTypeName.Name, this, schemaTypeInCT = new List<SeSchemaItem>(), seProp);

                if (schemaElement.ElementType is XmlSchemaSimpleType)
                {
                    List<SeSchemaItem> schemaTypeInST = new List<SeSchemaItem>();
                    SeSchemaItem ssi = new SeSchemaItem(schemaElement.Name, GetAnnotation(schemaElement), GetSimpleType(schemaTypeInST, schemaElement), this, schemaTypeInST, seProp);
                    SchemaItemsChildren.Add(ssi);
                }
                else
                {
                    SchemaItemsChildren.Add(schemaItem);
                    schemaItem.ReadXSDParticle(schemaElement);
                }
            }

        }
        /// <summary>
        /// Чтение содержимого внутри элемента
        /// </summary>
        /// <param name="childElement">Считываемый элемент</param>
        private void ReadXSDParticle(XmlSchemaElement childElement)
        {

            if (childElement.ElementSchemaType is XmlSchemaComplexType)
            {
                XmlSchemaComplexType complexType = childElement.ElementSchemaType as XmlSchemaComplexType;
                if (complexType.Particle is XmlSchemaAll)
                {
                    XmlSchemaAll all = complexType.Particle as XmlSchemaAll; // <xsd:all>
                    Properties.MinOccursAll = all.MinOccurs.ToString();
                    foreach (XmlSchemaElement childElement2 in all.Items)
                    {
                        ReadXSD(childElement2);
                    }
                }
                if (complexType.Particle is XmlSchemaSequence)
                {
                    XmlSchemaSequence seq2 = complexType.Particle as XmlSchemaSequence; // <xsd:all>

                    foreach (XmlSchemaObject childElement3 in seq2.Items)
                    {
                        ReadXSD(childElement3);
                    }
                }


            }

        }

        /// <summary>
        /// Переопределенный метод ToString() для корректного вывода в TreeView
        /// </summary>
        /// <returns>Строка, содержащую сведения о SeSchemaItem</returns>
        public override string ToString()
        {
            //if (Type != "" && Type != null)
            //{
            //    if (Description != "" && Description != null)
            //        return Name + " (" + Description + ") - " + Type;//this.tostring()
            //    else
            //        return Name + " - " + Type;
            //}
            //else

            if (Description != "" && Description != null)
                return Name + " (" + Description + ")";
            else
                return Name;
        }

        /// <summary>
        /// Запись из класса в treeView
        /// </summary>
        /// <param name="treeNodes">Текущая ветка</param>
        public void ClassToTreeView(TreeNodeCollection treeNodes)
        {
            TreeNode newTreeNode;

            newTreeNode = treeNodes.Add(ToString());
            newTreeNode.Tag = this;
            newTreeNode.Name = ToString() + Parent.ToString();
           
            //рекурсия (в случае, если у текущего элемента есть дочерние)
            if (SchemaItemsChildren != null)
            {
                foreach (SeSchemaItem schemaElement in SchemaItemsChildren)
                {
                    schemaElement.ClassToTreeView(newTreeNode.Nodes);
                }
            }

            if (Name == "SimpleType") newTreeNode.ImageIndex = 3;
            else if (Name == "CHOICE") newTreeNode.ImageIndex = 4;
                else newTreeNode.ImageIndex = 1;


        }
        /// <summary>
        /// Запись в новую схему свойств из класса
        /// </summary>
        /// <param name="newElement">Элемент, свойства которого записываются</param>
        private void SetProperties(XmlSchemaElement newElement)
        {
            if (Properties != null)
            {
                if (Properties.HasMinOccurs == true)
                    newElement.MinOccursString = Properties.MinOccurs;
                if (Properties.HasMaxOccurs == true)
                    newElement.MaxOccursString = Properties.MaxOccurs;
                newElement.IsNillable = Properties.IsNillable;
            }
        }
        /// <summary>
        /// Запись в новый XSD файл содержимого класса SeSchemaItem
        /// </summary>
        /// <param name="newElement1">Новый элемент в схеме</param>
        /// <param name="xs1">Новый экземпляр схемы</param>
        public void SaveXSD(XmlSchemaElement newElement1, XmlSchema xs1)
        {
            // var allowedSimpleType = SimpleType.String | SimpleType.integer | SimpleType.Decimal | SimpleType.dateTime;
            SetProperties(newElement1);
            if (Type == "")
            {
                XmlSchemaComplexType newSchemaType = new XmlSchemaComplexType();
                newElement1.SchemaType = newSchemaType;
                XmlSchemaAll newAll = new XmlSchemaAll();
                Properties.SetPropAll(newAll);
                newSchemaType.Particle = newAll;
                foreach (SeSchemaItem ssi in SchemaItemsChildren)
                {
                    XmlSchemaElement newElement = new XmlSchemaElement();
                    newElement.Name = ssi.Name;
                    ssi.SetProperties(newElement);
                    if (ssi.Description != null && ssi.Description != "")
                    {
                        newElement.Annotation = SetAnnotation(ssi);
                    }
                    if (ssi.SchemaItemsChildren != null && ssi.SchemaItemsChildren.Count != 0)
                    {
                        if (ssi.Name == "ActLiquidation_WasteGoods")
                        { }
                        if (ssi.SchemaItemsChildren[0].Name == "SimpleType")
                        {
                            XmlSchemaSimpleType simpleType = new XmlSchemaSimpleType();
                            newElement.SchemaType = simpleType;
                            XmlSchemaSimpleTypeRestriction restriction = new XmlSchemaSimpleTypeRestriction();
                            simpleType.Content = restriction;
                            restriction.BaseTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                            foreach (SeSchemaItem sstc in ssi.SchemaItemsChildren)
                            {
                                string facetName = sstc.Description.Split('-')[0];
                                string facetValue = sstc.Description.Split('-')[1];
                                if (facetName == "XmlSchemaMaxLengthFacet")
                                {
                                    XmlSchemaMaxLengthFacet ml = new XmlSchemaMaxLengthFacet();
                                    restriction.Facets.Add(ml);
                                    ml.Value = facetValue;
                                }
                                if (facetName == "XmlSchemaTotalDigitsFacet")
                                {
                                    XmlSchemaTotalDigitsFacet ml = new XmlSchemaTotalDigitsFacet();
                                    restriction.Facets.Add(ml);
                                    ml.Value = facetValue;
                                }
                                if (facetName == "XmlSchemaFractionDigitsFacet")
                                {
                                    XmlSchemaFractionDigitsFacet ml = new XmlSchemaFractionDigitsFacet();
                                    restriction.Facets.Add(ml);
                                    ml.Value = facetValue;
                                }
                            }

                        }

                        else
                        {
                            //<element ... type="xsd:...">
                            if (ssi.Type == SimpleType.Decimal.ToString().ToLower() || ssi.Type == SimpleType.String.ToString().ToLower() || ssi.Type == SimpleType.integer.ToString() || ssi.Type == SimpleType.dateTime.ToString())
                                newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                            else
                                //<element ... type="...">
                                newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type);
                        }
                    }
                    else
                    {
                        //<element ... type="xsd:...">
                        if (ssi.Type == SimpleType.Decimal.ToString().ToLower() || ssi.Type == SimpleType.String.ToString().ToLower() || ssi.Type == SimpleType.integer.ToString() || ssi.Type == SimpleType.dateTime.ToString())
                            newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type, "http://www.w3.org/2001/XMLSchema");
                        else
                            //<element ... type="...">
                            newElement.SchemaTypeName = new XmlQualifiedName(ssi.Type);
                    }
                    newAll.Items.Add(newElement);
                }
            }
            else newElement1.SchemaTypeName = new XmlQualifiedName(Type, "http://www.w3.org/2001/XMLSchema");


        }

        /// <summary>
        /// Запись Annotation в новый файл XSD
        /// </summary>
        /// <param name="newschemaItem">Текущий экземпляр класса SeSchemaItem(список List(SeSchemaItem) класса SeSchemaItem</param>
        /// <param name="discriptionAnn">Новый элемент XmlSchemaAnnotation</param>
        /// <returns>Измененный элемент XmlSchemaAnnotation</returns>
        public XmlSchemaAnnotation SetAnnotation(SeSchemaItem newschemaItem)
        {
            XmlSchemaAnnotation discriptionAnn = new XmlSchemaAnnotation();
            XmlSchemaDocumentation discriptionDoc = new XmlSchemaDocumentation();
            discriptionAnn.Items.Add(discriptionDoc);
            discriptionDoc.Markup = TextToNodeArray(newschemaItem.Description);
            return discriptionAnn;
        }
        private static XmlNode[] TextToNodeArray(string text)
        {
            XmlDocument doc = new XmlDocument();
            return new XmlNode[1] { doc.CreateTextNode(text) };
        }

        /// <summary>
        /// Получение родительского класса SeSchema
        /// </summary>
        /// <param name="ssi">Элемент, родительский класс SeSchema которого нужно найти</param>
        /// <returns>Родительский клас SeSchema</returns>
        public SeSchema GetSchema(SeSchemaItem ssi)
        {
            while (ssi.Parent is SeSchemaItem)
            {
                ssi = (SeSchemaItem)ssi.Parent;
            }
            return (SeSchema)ssi.Parent;
        }
        /// <summary>
        /// Изменение элементов при изменении имени ComplexType
        /// </summary>
        /// <param name="oldValue">Старое значение поля Name</param>
        /// <param name="newValue">Новое значение поля Name</param>
        public void ChangeNewComplexType(object oldValue, object newValue)
        {
            SeSchema ss = (SeSchema)Parent;
            foreach (SeSchemaItem ssiElement in ss.SchemaItems)
            {
                if (ssiElement.Type == oldValue.ToString())
                {
                    ssiElement.Type = newValue.ToString();
                }
                if (ssiElement.SchemaItemsChildren != null)
                    foreach (SeSchemaItem ssiElementEach in ssiElement.SchemaItemsChildren)
                    {
                        if (ssiElementEach.Type == oldValue.ToString())
                        {
                            ssiElementEach.Type = newValue.ToString();
                        }
                        foreach (SeSchemaItem ssiElementEach2 in ssiElementEach.SchemaItemsChildren)
                        {
                            if (ssiElementEach2.Type == oldValue.ToString())
                            {
                                ssiElementEach2.Type = newValue.ToString();
                            }

                        }
                    }
            }
        }
        /// <summary>
        /// Поиск элементов, ComplexType которого - текущий элемент
        /// </summary>
        /// <returns>Список элементов с искомым ComplexType</returns>
        public List<SeSchemaItem> FindAllTypes()
        {
            List<SeSchemaItem> elementsOfType = new List<SeSchemaItem>();
            SeSchema first = GetSchema(this);
            foreach (SeSchemaItem ssiElement in first.SchemaItems)
            {
                if (ssiElement.Type == Name)
                {
                    elementsOfType.Add(ssiElement);
                }
                if (ssiElement.SchemaItemsChildren.Count != 0)
                    foreach (SeSchemaItem ssiElemen2t in ssiElement.SchemaItemsChildren[0].SchemaItemsChildren)
                    {
                        if (ssiElemen2t.Type == Name)
                        {
                            elementsOfType.Add(ssiElemen2t);
                        }

                    }
            }
            return elementsOfType;
        }

    }
}
