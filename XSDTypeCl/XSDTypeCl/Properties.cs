using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeProperties
    {
        public string MinOccurs { get; set; }
        public string MaxOccurs { get; set; }
        public bool IsNillable { get; set; }
        public string MinOccursAll { get; set; }

        public SeProperties()
        {
            MinOccurs = null;
            MaxOccurs = null;
            IsNillable = false;
            MinOccursAll = null;
        }

        public SeProperties(XmlSchemaElement element)
        {
            if (element.MinOccursString == null)
                MinOccurs = null;
            else
            MinOccurs = element.MinOccursString;
            
            if (element.MaxOccursString == null)
                MaxOccurs = null;
            else
            MaxOccurs = element.MaxOccursString;
            
            IsNillable = element.IsNillable;
        }
        public void SetPropAll(XmlSchemaAll newAll)
        {
            newAll.MinOccursString = MinOccursAll;
        }
    }
}
