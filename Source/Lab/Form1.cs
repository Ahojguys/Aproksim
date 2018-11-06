using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Approximation;

namespace Lab
{
    public partial class Form1 : Form
    {
        Graphics gr;
        Bitmap bitmap;


        private int px;
        private int py;

        double[] coefs;
        List<Approximation.Point> points;

        public Form1()
        {
            InitializeComponent();
            var startPoints = new[]
            {
                new Approximation.Point(2, 2),
                new Approximation.Point(3, 5),
                new Approximation.Point(6, 7.15),
                new Approximation.Point(-8, -3),
                new Approximation.Point(0, 0.221)
            };

            points = startPoints.ToList();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            GetAllInfo();

            CreateFormGraphics();
            SetZero(450, 320);

            UpdateDraw();
            

            this.pictureBox1.MouseMove += PictureBox1_MouseMove;
            this.pictureBox1.MouseDown += PictureBox1_MouseDown;
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {                
                points.Add(new Approximation.Point((double)(e.X - px) / 40, (double)(py - e.Y) / 40));
            }

            if(e.Button == MouseButtons.Right)
            {
                var startPoints = new[]
                {
                new Approximation.Point(2, 2),
                new Approximation.Point(3, 5),
                new Approximation.Point(6, 7.15),
                new Approximation.Point(-8, -3),
                new Approximation.Point(0, 0.221)
                };

                points = startPoints.ToList();
            }


            GetAllInfo();
            UpdateDraw();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            gr.Clear(Color.White);
            UpdateDraw();

            gr.DrawLine(new Pen(Color.Yellow), new System.Drawing.Point(e.X, 0), new System.Drawing.Point(e.X, pictureBox1.Height));
            gr.DrawLine(new Pen(Color.Yellow), new System.Drawing.Point(0, e.Y), new System.Drawing.Point(pictureBox1.Width, e.Y));


            Approximation.Point tmp = new Approximation.Point((double)(e.X - px) / 40, (double)(py - e.Y) / 40);
            gr.DrawString($"Mouse x coord: {tmp.X}", SystemFonts.MenuFont, Brushes.Red, new Approximation.Point(10, 10));
            gr.DrawString($"Mouse y coord: {tmp.Y}", SystemFonts.MenuFont, Brushes.Red, new Approximation.Point(10, 30));
        }

        void UpdateDraw()
        {
            DrawCoordinateSystem();
            DrawFunction();
            DrawPoints();
            pictureBox1.Image = bitmap;
        }

        void SetZero(int x, int y)
        {
            px = x;
            py = y;
        }

        void CreateFormGraphics()
        {
            bitmap = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            gr = Graphics.FromImage(bitmap);

            gr.Clear(Color.White);

            
        }

        void DrawCoordinateSystem()
        {
            //Ось ординат
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(-380,0)), TransformCoords(new Approximation.Point(500, 0)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(490, 5)), TransformCoords(new Approximation.Point(490, -5)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(490, 5)), TransformCoords(new Approximation.Point(500, 0)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(490, -5)), TransformCoords(new Approximation.Point(500, 0)));
            gr.DrawString("X", SystemFonts.MenuFont, Brushes.Red, TransformCoords(new Approximation.Point(490, -8)));

            //Ось абцисс
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(0, 300)), TransformCoords(new Approximation.Point(0, -300)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(5, 290)), TransformCoords(new Approximation.Point(-5, 290)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(0, 300)), TransformCoords(new Approximation.Point(5, 290)));
            gr.DrawLine(new Pen(Color.Red), TransformCoords(new Approximation.Point(0, 300)), TransformCoords(new Approximation.Point(-5, 290)));
            gr.DrawString("Y", SystemFonts.MenuFont, Brushes.Red, TransformCoords(new Approximation.Point(8, 300)));

            gr.DrawString("0", SystemFonts.MenuFont, Brushes.Red, TransformCoords(new Approximation.Point(2, -2)));

            for (int i = -9; i < 10; i++)
            {
                if (i == 0)
                    continue;

                gr.DrawLine(new Pen(Color.Red), ScaleTransform(new Approximation.Point(i, 0.1)), ScaleTransform(new Approximation.Point(i, -0.1)));
                gr.DrawString(i.ToString(), SystemFonts.MenuFont, Brushes.Red, ScaleTransform(new Approximation.Point(i - 0.2, -0.2)));
            }

            for (int i = -7; i < 8; i++)
            {
                if (i == 0)
                    continue;

                gr.DrawLine(new Pen(Color.Red), ScaleTransform(new Approximation.Point(0.1, i)), ScaleTransform(new Approximation.Point(-0.1, i)));
                gr.DrawString(i.ToString(), SystemFonts.MenuFont, Brushes.Red, ScaleTransform(new Approximation.Point(-0.6, i + 0.2)));
            }
        }


        Approximation.Point TransformCoords(Approximation.Point p)
        {
            return new Approximation.Point(p.X + px, py - p.Y);
        }


        Approximation.Point ScaleTransform(Approximation.Point p)
        {
            return new Approximation.Point(p.X * 40 + px, py - p.Y * 40);
        }

        void DrawFunction()
        {
            gr.DrawLine(new Pen(Color.Green), ScaleTransform(new Approximation.Point(-9, GetFuncY(-9))), ScaleTransform(new Approximation.Point(9, GetFuncY(9))));
        }

        double GetFuncY(double x)
        {
            return x * coefs[0] + coefs[1];
        } 

        void DrawPoints()
        {
            for (int i = 0; i < points.Count; i++)
            {
                var p = (PointF)ScaleTransform(points[i]);
                gr.FillRectangle(Brushes.Blue, p.X-2, p.Y-2, 4, 4);
            }
        }

        void GetAllInfo()
        {
            
            coefs = RunLib.Calculate(points.ToArray());
            PointCountLabel.Text = $"Points count: {points.Count.ToString()}   y = {coefs[0]}x + {coefs[1]}";
        }
    }
}
