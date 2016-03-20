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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
            textBox3.Enabled = false;
            textBox3.MaxLength = 13;
        }
        string path2;
        string path;


        FolderBrowserDialog fbd = new FolderBrowserDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                string cnp = textBox3.Text;
                string nume= textBox1.Text;
                string prenume = textBox2.Text;
                double nr_p ;
                string nr = textBox4.Text;
                string serie = textBox5.Text;
                DateTime dt2 = dateTimePicker2.Value;
                DateTime dt = dateTimePicker1.Value;
                DateTime ex1, ex2;

                nr_p = (dt2 - dt).TotalDays;
                
                string[] zp=new string[400];
                string[] zp1=new string[400];
                string ss;
                string[] words =new string[400];
                int n=1;


                XmlDocument xdoc= new XmlDocument();
                xdoc.Load(path);
                int ko = 0;

                
                
                
                //maths
                while (nr_p > 0)
                {
                    foreach (XmlNode node in xdoc.SelectNodes("larg/ex"))
                    {   

                        ex1 = DateTime.ParseExact(node.SelectSingleNode("ex1").InnerText, "dd.MM.yyyy HH:mm:ss", null);
                        ex2 = DateTime.ParseExact(node.SelectSingleNode("ex2").InnerText, "dd.MM.yyyy HH:mm:ss", null);

                        while (ex1.Date.Subtract(new TimeSpan(1, 0, 0, 0)) < dt.Date && dt.Date < ex2.Date.Add(new TimeSpan(1, 0, 0, 0)))
                        {
                            dt = dt.AddDays(1);
                            nr_p--;
                        }
                        // MessageBox.Show(dt.ToString() );

                        // daca sambata sau duminica sari peste 
                        if (dt.DayOfWeek == DayOfWeek.Saturday)
                        { dt = dt.AddDays(2); nr_p-=2; }
                        else if (dt.DayOfWeek == DayOfWeek.Sunday)
                        { dt = dt.AddDays(1); nr_p--; }
                    }
                    // daca sambata sau duminica sari peste 
                    if (dt.DayOfWeek == DayOfWeek.Saturday)
                    { dt = dt.AddDays(2); nr_p-=2; }
                    else if (dt.DayOfWeek == DayOfWeek.Sunday)
                    { dt = dt.AddDays(1); nr_p--; }


                    if (dt.DayOfWeek == DayOfWeek.Monday)
                    {
                       ss = dt.ToString();
                       words = ss.Split(' ');
                        zp[n] = words[0];
                        n++;
                        //zp[n][2] = "1";
                        zp1[n] = "1";
                        dt = dt.AddDays(1);
                        nr_p--;

                       

                    }
                    else if (dt.DayOfWeek == DayOfWeek.Friday)
                    {

                        ss = dt.ToString();
                        words = ss.Split(' ');
                        zp[n] = words[0];
                        n++;
                        //zp[n][2] = "2";
                        zp1[n] = "2";
                        dt = dt.AddDays(1);
                        nr_p--;
                    
                    }


                    else
                    {
                         ss = dt.ToString();
                        words = ss.Split(' ');

                        zp[n] = words[0];
                        n++;
                        zp1[n] = "0";
                        dt = dt.AddDays(1);
                        nr_p--;
                        
                    }






                }



               //GRAFICA

                Bitmap bt = new Bitmap(830, 1170);
                Graphics g = Graphics.FromImage(bt);
                Pen p = new Pen(Color.Black);
                SolidBrush s = new SolidBrush(Color.Black);
                SolidBrush al = new SolidBrush(Color.White);

                FontFamily ff = new FontFamily("Arial");
                System.Drawing.Font font = new System.Drawing.Font(ff, 12);


                int nr_foaie = 1;

                //coordonate pentru desenat
                int cor_x = 15,stas_x1=15,stas_x2=430;
                int cor_y = 30;
                int nr_rand=1;
                int bon_tip = 0;
                



                for (int i = 1; i <= n;i++ )
                {
                    if (ko == 0)
                    {
                        
                         cor_x = stas_x1;
                         g.DrawRectangle(p, 15, 30, 390, 160);

                        //Titlul
                        g.DrawString("COLEGIUL NATIONAL AL. I. CUZA", font, s, new PointF(100, 32));

                        //Localitate
                        g.DrawString("Localitate: " + "Corabia ", font, s, new PointF(20, 50));


                        //Cartela pentru masa si serie
                        g.DrawString("CARTELA PENTRU MASA ", font, s, new PointF(120, 72));
                        g.DrawString("Seria "+serie + " nr: " + nr, font, s, new PointF(160, 87));

                        //Nume Prenume
                        g.DrawString("Nume: " + " " + nume + " " + "Prenume: " + prenume, font, s, new PointF(20, 110));

                        //Semnatura 
                        g.DrawString("Semnatura persoanei care elibereaza cartela", font, s, new PointF(20, 140));
                        g.DrawString(".......................................................................", font, s, new PointF(20, 170));

                        nr_rand++;
                        ko++; cor_x = stas_x2;
                    }
                        
                        //words = ss.Split(' ');

                    else //g.DrawRectangle(p, 15, 30, 390, 160);----bon stanga 1
                    {
                        
                        g.DrawRectangle(p, cor_x, cor_y, 390, 160);
                        
                        //Scrie in primul Dreptunghi {Seara}
                        g.DrawRectangle(p, cor_x+10, cor_y+10, 113, 140);

                        if (int.Parse(zp1[i])!=2)//int.Parse(zp[i][2]) != 2
                        {
                            g.DrawString("CINA ", font, s, new PointF(cor_x+42, cor_y+15));
                            g.DrawString("    DATA" + "\r\n" + zp[i-1], font, s, new PointF(cor_x+22, cor_y+107));
                        }
                        //Scrie in al doilea Dreptunghi {Pranz}
                        g.DrawRectangle(p, cor_x+133, cor_y + 10, 113, 140);

                        g.DrawString("PRANZ ", font, s, new PointF(cor_x+163, cor_y+15));
                        g.DrawString("    DATA" + "\r\n" + zp[i-1], font, s, new PointF(cor_x+150, cor_y+107));

                        //Scrie in al treilea Dreptunghi {Dimineata}
                        g.DrawRectangle(p, cor_x+256, cor_y + 10, 113, 140);

                        if (int.Parse(zp1[i])!=1)//int.Parse(zp[i][2]) != 1
                        {
                            g.DrawString("MIC DEJUN ", font, s, new PointF(cor_x+268, cor_y+15));
                            g.DrawString("    DATA" + "\r\n" + zp[i-1], font, s, new PointF(cor_x+273, cor_y+107));
                        } 
                        //----------------------------------------------
                        if (nr_rand == 1)
                        { nr_rand++; cor_x = stas_x2; }
                        else { nr_rand = 1; cor_x = stas_x1; cor_y = cor_y + 160; bon_tip = bon_tip + 2; }
                        //----------------------------------------------
                        if (bon_tip == 14)
                        {
                           
                            
                            bon_tip = 0;
                            bt.Save(fbd.SelectedPath + "\\Bon" + " " + nume + " " + prenume + " " + nr_foaie + ".png");
                        cor_x = 15;
                        cor_y = 30;
                        //bt = new Bitmap(830, 1170);
                        g.FillRectangle(al,0,0,820, 1170);
                        nr_foaie++;

                        }
                        else if (n==i)
                        {
                            MessageBox.Show("Operatiune incheiata!"," ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            bt.Save(fbd.SelectedPath + "\\Bon" + " " + nume + " " + prenume + " " + nr_foaie + ".png");
                        }





                    
                    }//sfarsit else


                }//sfarsit for
               
            
                



            }

            catch
            {
               MessageBox.Show("A avut loc o eroare! \r\n Verificati daca au fost introduse corect toate datele si daca ati selectat folderul unde sa fie salvate tichetele.","Eroare",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            finally
            {
                button1.Enabled = true;
                button2.Enabled = true;
               button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {// deschidere modificator baza de date
            Form2 f=new Form2();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {//deschidere baza de date
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                path = ofd.FileName;
                button1.Enabled = true;
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            
            //sfd.Filter = "File folde";

            if (fbd.ShowDialog() == DialogResult.OK)
            {

               // path2 = fbd.SelectedPath;
                
                
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       
    }
}
