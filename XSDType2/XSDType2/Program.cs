using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Threading.Tasks;

namespace XSDType2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader("XSDType1.xsd");
                XmlSchema myschema = XmlSchema.Read(reader, ValidationCallback);
                myschema.Write(Console.Out);
                FileStream file = new FileStream("new.xsd", FileMode.Create, FileAccess.ReadWrite);
                XmlTextWriter xwriter = new XmlTextWriter(file, new UTF8Encoding());
                xwriter.Formatting = Formatting.Indented;
                myschema.Write(xwriter);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: "+e);

                Console.ReadLine();
            }
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);

            Console.ReadLine();
        }
    }
    }

