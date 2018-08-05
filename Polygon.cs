using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Polygon
{
    public class Polygon
    {
        public Polygon()
        {
        }
        public Polygon(PointF[] points)
        {
            Points = points;
        }

        public PointF[] Points;
        public float[] rads;
        public float rad;

        // Поиск центра полигона
        public PointF FindCentroid()
        {
            int numPoints = Points.Length;
            PointF[] pts = new PointF[numPoints];
            List<PointF> sortedPoints = new List<PointF>();
            PointF up;
            PointF down;
            PointF left;
            PointF right;
            float centerX;
            float centerY;

            rads = new float[numPoints];
            Points.CopyTo(pts, 0);

            // Сортировка координат вершин по X
            sortedPoints = pts.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            sortedPoints.CopyTo(pts, 0);

            // Самая левая и самая правая точки полигона
            left = pts[0];
            right = pts[pts.Length - 1];
            centerX = (right.X + left.X) / 2;

            // Сортировка координат вершин по Y
            sortedPoints = pts.OrderBy(p => p.Y).ThenBy(p => p.X).ToList();
            sortedPoints.CopyTo(pts, 0);

            // Самая верхняя и самая нижняя точки полигона
            up = pts[0];
            down = pts[pts.Length - 1];           
            centerY = (up.Y + down.Y) / 2;

            // Расчёт длин радиусов от центра до каждой вершины
            for (int i = 0; i < numPoints; i++)
            {
               rads[i] = (float)Math.Sqrt(Math.Pow(pts[i].X - centerX, 2) + Math.Pow(pts[i].Y - centerY, 2));
            }

            return new PointF(centerX, centerY);
        }

        public float FindRad()
        {
            // Сортировка массива с радиусами
            Array.Sort(rads);
            Array.Reverse(rads);

            // Самый большой радиус
            rad = rads[0];

            return rad;
        }
    }
}
