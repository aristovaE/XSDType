﻿using System;
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

        public XmlSchemaSet ReadXSD()
        {
            XmlSchemaSet xss = null;
            XmlSchema xs = null;
            XmlSchemas schemas = null;
            ValidationEventHandler ValidationErrorHandler = null;

            DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\"));
            // DirectoryInfo diXsd = new DirectoryInfo(Path.Combine(Application.StartupPath, @"..\..\..\..\xsd\new\")); //для проверки новых схем


            treeView1.Nodes.Clear();
            xss = new XmlSchemaSet();
            xss.ValidationEventHandler += ValidationErrorHandler;
            xss.XmlResolver = new XmlUrlResolver();
            schemas = new XmlSchemas();
            try
            {
                foreach (var fi in diXsd.GetFiles())
                {
                    using (var sr = new StreamReader(fi.FullName))
                    {
                        xs = XmlSchema.Read(sr, ValidationErrorHandler);
                        xss.Add(xs);
                        schemas.Add(xs);
                    }
                    // MessageBox.Show("Schema " + fi.Name + " read successfully ");

                }
                xss.Compile();
            }
            catch { }
            return xss;
        }


        private void BtnToTV_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            treeView1.Nodes.Clear();

            if (seSchema != null)
                seSchema.ClassToTreeView(treeView1.Nodes);

            // treeView1.Nodes[0].ExpandAll();


        }

        private void BtnXSDToSeSChema_Click(object sender, EventArgs e)
        {
            XmlSchemaSet xss = ReadXSD();
            SeSchema seSchema;
            List<SeSchema> seSchemaList = null;
            foreach (XmlSchema schema in xss.Schemas())
            {
                seSchema = new SeSchema(schema);
                seSchemaList = seSchemaList ?? new List<SeSchema>();
                seSchemaList.Add(seSchema);

                seSchema.ClassToTreeView(treeView1.Nodes);

            }

            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = seSchemaList;
            comboBox1.DataSource = bindingSource1.DataSource;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";

            //MessageBox.Show("Все доступные схемы прочитаны и добавлены в ComboBox");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;
            string str = e.Node.Text.Split(' ')[0];
            textBox1.Text = str;

        }



        private void BtnSave_Click(object sender, EventArgs e)
        {
            SeSchema seSchema = (SeSchema)comboBox1.SelectedItem;

            ////чтение выбранной схемы
            //XmlSchema xs = null;
            //ValidationEventHandler ValidationErrorHandler = null;
            //xs = XmlSchema.Read(new StreamReader(@"..\..\..\..\xsd\"+seSchema.Name + ".xsd"), ValidationErrorHandler);

            XmlSchema xs1 = new XmlSchema();
            seSchema.SaveXSD(xs1);

            using (FileStream fs = new FileStream(@"..\..\..\..\xsd\new\" + seSchema.Name + "NEW.xsd", FileMode.Create, FileAccess.ReadWrite))
            {
                using (XmlTextWriter tw = new XmlTextWriter(fs, new UTF8Encoding()))
                {
                    tw.Formatting = Formatting.Indented;
                    xs1.Write(tw);
                }
                fs.Close();
            }
            //создание новой схемы



        }
    }
}

