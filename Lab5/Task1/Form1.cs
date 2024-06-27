using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawBezierCurve()
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            Pen p = new Pen(Color.Black);
            g.DrawLine(p, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            g.DrawLine(p, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));

            for (int i = (int)-Math.Max((pictureBox1.Width - pictureBox1.Width / 2), pictureBox1.Width / 2); i <= Math.Max((pictureBox1.Width - pictureBox1.Width / 2), pictureBox1.Width / 2); i++)
            {
                g.DrawLine(p, new Point(pictureBox1.Width / 2 + 10 * i, pictureBox1.Height / 2 - 5), new Point(pictureBox1.Width / 2 + 10 * i, pictureBox1.Height / 2 + 5));
            }
            for (int i = (int)-Math.Max((pictureBox1.Height - pictureBox1.Height / 2), pictureBox1.Height / 2); i <= Math.Max((pictureBox1.Height - pictureBox1.Height / 2), pictureBox1.Height / 2); i++)
            {
                g.DrawLine(p, new Point(pictureBox1.Width / 2 - 5, pictureBox1.Height / 2 + 10 * i), new Point(pictureBox1.Width / 2 + 5, pictureBox1.Height / 2 + 10 * i));
            }
            try
            {
                PointF P1 = new PointF(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                PointF P2 = new PointF(int.Parse(textBox3.Text), int.Parse(textBox4.Text));
                PointF P3 = new PointF(int.Parse(textBox5.Text), int.Parse(textBox6.Text));
                PointF P4 = new PointF(int.Parse(textBox7.Text), int.Parse(textBox8.Text));

                DrawBezier(g, P1, P2, P3, P4);
            }
            catch (Exception)
            {
                MessageBox.Show("Wrong input");
            }

        }

        private void DrawBezier(Graphics g, PointF P1, PointF P2, PointF P3, PointF P4)
        {
            int steps = 100;
            PointF[] points = new PointF[steps + 1];

            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                points[i] = CalculateBezierPoint(t, P1, P2, P3, P4);
            }
            Pen dashed = new Pen(Color.Black);
            dashed.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
            P1.X += pictureBox1.Width / 2;
            P2.X += pictureBox1.Width / 2;
            P3.X += pictureBox1.Width / 2;
            P4.X += pictureBox1.Width / 2;
            P1.Y = pictureBox1.Height / 2 - P1.Y;
            P2.Y = pictureBox1.Height / 2 - P2.Y;
            P3.Y = pictureBox1.Height / 2 - P3.Y;
            P4.Y = pictureBox1.Height / 2 - P4.Y;
            g.DrawLine(dashed, P1, P2);
            g.DrawLine(dashed, P2, P3);
            g.DrawLine(dashed, P3, P4);
            g.DrawLine(dashed, P4, P1);
            g.DrawLines(Pens.Black, points);
        }

        private PointF CalculateBezierPoint(float t, PointF P1, PointF P2, PointF P3, PointF P4)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            float x = uuu * P1.X; // (1-t)^3 * P1
            x += 3 * uu * t * P2.X; // 3(1-t)^2 * t * P2
            x += 3 * u * tt * P3.X; // 3(1-t) * t^2 * P3
            x += ttt * P4.X; // t^3 * P4

            float y = uuu * P1.Y; // (1-t)^3 * P1
            y += 3 * uu * t * P2.Y; // 3(1-t)^2 * t * P2
            y += 3 * u * tt * P3.Y; // 3(1-t) * t^2 * P3
            y += ttt * P4.Y; // t^3 * P4

            return new PointF(pictureBox1.Width / 2 + x, pictureBox1.Height / 2 - y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrawBezierCurve();
        }
    }
}
