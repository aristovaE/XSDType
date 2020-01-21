using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections;

namespace XSDTypeCl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void readXsd_Click(object sender, EventArgs e)
        {
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            XmlDocument doc = new XmlDocument();
           // string filePath = @"...\..\..\..\xsd\Passport.xsd";

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));


            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();

            schemas = new XmlSchemas();
            foreach (var fi in diXsd.GetFiles("Passport.xsd"))
            {
                using (var sr = new StreamReader(fi.FullName))
                {
                    xs = XmlSchema.Read(sr, ValidationErrorHandler);
                    xss.Add(xs);
                    schemas.Add(xs);
                }

            }

            xss.Compile();

            XmlSchemaElement schemaElement = null;
            Schema xsdSchema = new Schema();
            SchemaItem xsdSchemaItem = new SchemaItem();
            foreach (XmlSchema schema in xss.Schemas())
            {
                foreach (var sChemaItem in schema.Items)
                {
                    schemaElement = sChemaItem as XmlSchemaElement;
                    if (schemaElement != null)
                    {
                       // xsdSchema( имя головного элемента, annotation?,(имя ,тип,список элементов???,);
                    }
                }
            }
        }
    }
}
