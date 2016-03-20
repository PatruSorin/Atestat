using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Generator_bonuri
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            button2.Enabled = false;
            
          
        }

        //Globale
        XmlDocument xdoc;
        string path;
        //Close
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
            
        
        }
        //Open
        private void button3_Click(object sender, EventArgs e)
        {


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {path = ofd.FileName;
            xdoc = new XmlDocument();
            xdoc.Load(path);
            button2.Enabled = true;
            
            }
            
        }
        //Adauga
        private void button2_Click(object sender, EventArgs e)
        {
            XmlNode ex  = xdoc.CreateElement("ex");
            XmlNode ex1 = xdoc.CreateElement("ex1");
            XmlNode ex2 = xdoc.CreateElement("ex2");

            ex1.InnerText = dateTimePicker1.Value.ToString();
            ex2.InnerText = dateTimePicker2.Value.ToString();

            ex.AppendChild(ex1);
            ex.AppendChild(ex2);

            xdoc.DocumentElement.AppendChild(ex);

            xdoc.Save(path);

            string ss = dateTimePicker1.Value.Date.ToString();
            string[] words = ss.Split(' ');
            string sss = dateTimePicker2.Value.Date.ToString();
            string[] wordss = sss.Split(' ');

            
            label3.Text = words[0] + " ---> " + wordss[0];
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Creare
            SaveFileDialog sfd =new SaveFileDialog();
            sfd.Filter = "XML|*.xml";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                
                path=sfd.FileName;
                BinaryWriter bw = new BinaryWriter(File.Create(path));
                bw.Write("<larg></larg>");
                bw.Close();
                xdoc = new XmlDocument();
                xdoc.Load(path);

                
                button2.Enabled = true;
            }

        }
    }
}
