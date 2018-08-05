using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Polygon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int PT_RAD = 2;
        const int PT_WID = PT_RAD * 2 + 1;

        private List<PointF> m_Points = new List<PointF>();

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Рисование полигона
            if (m_Points.Count >= 3)
            {
                e.Graphics.DrawPolygon(Pens.Black, m_Points.ToArray());
            }
            else if (m_Points.Count == 2)
            {
                // Рисование линии
                e.Graphics.DrawLines(Pens.Black, m_Points.ToArray());
            }

            // Рисование точек
            if (m_Points.Count > 0)
            {
                foreach (PointF pt in m_Points)
                {
                    e.Graphics.FillEllipse(Brushes.Black, pt.X - PT_RAD, pt.Y - PT_RAD, PT_WID, PT_WID);
                    e.Graphics.DrawEllipse(Pens.Black, pt.X - PT_RAD, pt.Y - PT_RAD, PT_WID, PT_WID);
                }
            }

            EnableMenus();
        }

        private void EnableMenus()
        {
            bool enabled = (m_Points.Count >= 3);
            mnuTestsCentroid.Enabled = enabled;
        }

        // Очистка поля
        private void mnuTestsClear_Click(object sender, EventArgs e)
        {
            m_Points = new List<PointF>();
            EnableMenus();
            this.Invalidate();
            label1.Text = "Радиус";
        }

        // Сохранение координат вершины
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            m_Points.Add(new PointF(e.X, e.Y));

            this.Invalidate();
        }

        // Рисовка окружности
        private void mnuTestsCentroid_Click(object sender, EventArgs e)
        {
            Polygon pgon = new Polygon(m_Points.ToArray());

            PointF pt = pgon.FindCentroid();
            float r = pgon.FindRad();

            Rectangle rect = new Rectangle((int)pt.X - 3, (int)pt.Y - 3, 7, 7);
            using (Graphics gr = this.CreateGraphics())
            {
                gr.FillEllipse(Brushes.Red, rect);
                gr.DrawEllipse(Pens.Red, rect);
                gr.DrawEllipse(Pens.Red, (int)pt.X - r, (int)pt.Y - r, (int)r * 2, (int)r * 2);
            }

            label1.Text = "Радиус " + ((int)r).ToString();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
        }
    }
}
