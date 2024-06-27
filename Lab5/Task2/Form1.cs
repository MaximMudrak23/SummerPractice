using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        private int depth = 4;
        private float size = 200;

        public Form1()
        {
            InitializeComponent();
            this.buttonDraw.Click += new EventHandler(this.ButtonDraw_Click);
            this.pictureBox1.Paint += new PaintEventHandler(this.OnPaint);
        }

        private void ButtonDraw_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out depth) && float.TryParse(textBox2.Text, out size))
            {
                if (depth < 0 || size <= 0 || size > pictureBox1.Width || size > pictureBox1.Height)
                {
                    MessageBox.Show("Please enter a valid depth and size (size must fit within the PictureBox).");
                    return;
                }
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for depth and size.");
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (depth >= 0 && size > 0)
            {
                Graphics g = e.Graphics;
                Pen pen = new Pen(Color.Black);
                float offsetX = (pictureBox1.Width - size) / 2;
                float offsetY = (pictureBox1.Height - size) / 2;

                PointF start = new PointF(offsetX, offsetY);
                PointF end = new PointF(offsetX + size, offsetY);

                DrawKochCurve(g, pen, start, end, depth);
                start = new PointF(offsetX + size, offsetY);
                end = new PointF(offsetX + size, offsetY + size);
                DrawKochCurve(g, pen, start, end, depth);
                start = new PointF(offsetX + size, offsetY + size);
                end = new PointF(offsetX, offsetY + size);
                DrawKochCurve(g, pen, start, end, depth);
                start = new PointF(offsetX, offsetY + size);
                end = new PointF(offsetX, offsetY);
                DrawKochCurve(g, pen, start, end, depth);
            }
        }

        private void DrawKochCurve(Graphics g, Pen pen, PointF start, PointF end, int depth)
        {
            if (depth == 0)
            {
                g.DrawLine(pen, start, end);
            }
            else
            {
                float dx = end.X - start.X;
                float dy = end.Y - start.Y;
                PointF p1 = new PointF(start.X + dx / 3, start.Y + dy / 3);
                PointF p2 = new PointF((start.X + end.X) / 2 - (float)(Math.Sqrt(3) * (start.Y - end.Y) / 6),
                                       (start.Y + end.Y) / 2 + (float)(Math.Sqrt(3) * (start.X - end.X) / 6));
                PointF p3 = new PointF(start.X + 2 * dx / 3, start.Y + 2 * dy / 3);

                DrawKochCurve(g, pen, start, p1, depth - 1);
                DrawKochCurve(g, pen, p1, p2, depth - 1);
                DrawKochCurve(g, pen, p2, p3, depth - 1);
                DrawKochCurve(g, pen, p3, end, depth - 1);
            }
        }
    }
}
