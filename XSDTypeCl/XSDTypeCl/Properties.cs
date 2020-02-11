using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace XSDTypeCl
{
    public class SeProperties
    {
        public bool HasMinOccurs { get; set; }
        public bool HasMaxOccurs { get; set; }
        public bool HasNillable { get; set; }

        public string MinOccurs { get; set; }
        public string MaxOccurs { get; set; }
        public bool IsNillable { get; set; }

        public SeProperties(XmlSchemaElement element)
        {
            if (element.MinOccursString == null)
                HasMinOccurs = false;
            else
            {
                HasMinOccurs = true;
                MinOccurs = element.MinOccursString;
            }


            if (element.MaxOccursString == null)
                HasMaxOccurs = false;
            else
            {
                HasMaxOccurs = true;
                MaxOccurs = element.MaxOccursString;
            }
            IsNillable = element.IsNillable;
        }
    }
}
