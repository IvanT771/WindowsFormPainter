using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace FormPainter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSize();
        }

        private class ArrayPoint
        {
            private int index = 0;
            private Point[] points;
            public ArrayPoint(int size)
            {
                if (size <= 0) { size = 2;}
                points = new Point[size];
            }

            public void SetPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }
                points[index] = new Point(x,y);
                index++;
            }
            public void ResetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }

        private bool isMouse = false;
        private ArrayPoint arrayPoints = new ArrayPoint(2);

        Bitmap map = new Bitmap(100,100);
        Graphics g;

        private Pen pen = new Pen(Color.Black, 1f);

        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width,rectangle.Height);
            g = Graphics.FromImage(map);

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

        }


       



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
            arrayPoints.ResetPoints();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouse) { return;}

            arrayPoints.SetPoint(e.X,e.Y);
            if (arrayPoints.GetCountPoints() >= 2)
            {
                g.DrawLines(pen, arrayPoints.GetPoints());
                pictureBox1.Image = map;
                arrayPoints.SetPoint(e.X, e.Y);
            }
            
        }


        //button clear
        private void button1_Click(object sender, EventArgs e)
        {
          g.Clear(pictureBox1.BackColor);
          pictureBox1.Image = map;
        }

        //buttons set color
        private void button2_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        //button choice color pen
        private void button11_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter  = "JPG(*.JPG)|*.jpg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(pictureBox1.Image != null)
                {

                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = ((Button)sender).BackColor;

            Region region = new Region(new Rectangle(0,0, map.Width, map.Height));
            SolidBrush brush = new SolidBrush(pictureBox1.BackColor);

            g.FillRegion(brush,region);
            pictureBox1.Image = map;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog2.Color;
                ((Button)sender).BackColor = colorDialog2.Color;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }
    }
}
