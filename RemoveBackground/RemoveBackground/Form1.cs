using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveBackground
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            button2.Enabled = true;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            Bitmap image = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = image;
            pictureBox1.Size = image.Size;
            this.Height = pictureBox1.Size.Height+200;
            this.Width = pictureBox1.Size.Width + 200;
        }


       

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Bitmap clickcolor;
            Color clickcolor;
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap newimage = new Bitmap(pictureBox1.Image);
            clickcolor = image.GetPixel(e.X,e.Y);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    newimage.SetPixel(i, j, clickcolor);

            pictureBox2.Image = newimage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            Bitmap turgetimage=new Bitmap(pictureBox2.Image);
            Color pixelcolor=new Color();
            Color turgetcolor=new Color();
            turgetcolor=turgetimage.GetPixel(0,0);
            for(int i = 0 ;i < image.Width;i++)
                for (int j = 0; j < image.Height; j++)
                {
                    pixelcolor = image.GetPixel(i, j);
                    if(Math.Abs(pixelcolor.R-turgetcolor.R)<int.Parse(textBox1.Text)&&
                       Math.Abs(pixelcolor.G-turgetcolor.G)<int.Parse(textBox1.Text)&&
                       Math.Abs(pixelcolor.B-turgetcolor.B)<int.Parse(textBox1.Text))
                    {
                        Color clearcolor=new Color();
                        clearcolor=Color.FromArgb(0,pixelcolor);
                        image.SetPixel(i, j, clearcolor);
                    }
                }
            pictureBox1.Image = image;
        }
        int a=0;
        private void button3_Click(object sender, EventArgs e)
        {
            
            a++;
            if (a % 4 == 1)
                this.BackColor = Color.Red;
            else if (a % 4 == 2)
                this.BackColor = Color.Green;
            else if (a % 4 == 0)
                this.BackColor = Color.Blue;
            else if(a%4==3)
                this.BackColor = System.Drawing.SystemColors.Control;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            image.Save(@"output.png");
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            using (MemoryStream pngMemStream = new MemoryStream())
                if (e.KeyCode == Keys.V && e.Control == true)
            {
                if(Clipboard.ContainsImage())
                {
                    IDataObject iData = Clipboard.GetDataObject();
                    Bitmap image = (Bitmap)iData.GetData(DataFormats.Bitmap);
                    pictureBox1.Image = image;
                    pictureBox1.Size = image.Size;
                    this.Height = pictureBox1.Size.Height + 200;
                    this.Width = pictureBox1.Size.Width + 200;
                    button2.Enabled = true;
                }
            }
            else if (e.KeyCode == Keys.C && e.Control == true)
            {
                Clipboard.SetImage(pictureBox1.Image);
            }
        }



    }
}
