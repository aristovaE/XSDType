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
        [Browsable(false)]  //?
        public bool HasMinOccurs { get; set; }
        [Browsable(false)]  //?
        public bool HasMaxOccurs { get; set; }
        [Browsable(false)]  //?
        private bool HasNillable { get; set; }

        public string MinOccurs { get; set; }
        public string MaxOccurs { get; set; }
        public bool IsNillable { get; set; }

        [Browsable(false)]  //?
        public string MinOccursAll { get; set; }

        public SeProperties()
        {
            HasMinOccurs = false;
            HasMaxOccurs = false;
            HasNillable = false;
        }

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
        public void SetPropAll(XmlSchemaAll newAll)
        {
            newAll.MinOccursString = MinOccursAll;
        }
    }
}
