using ShadesTriangles.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadesTriangles.Extenstions
{
	public static class PointExtension
	{
		public static bool isInside(this Point p, Triangle tri)
		{
			double A = GetArea(tri.P1, tri.P2, tri.P3);

			double A1 = GetArea(p, tri.P2, tri.P3);

			double A2 = GetArea(tri.P1, p, tri.P3);

			double A3 = GetArea(tri.P1, tri.P2, p);

			return (A == A1 + A2 + A3);
		}
		private static double GetArea(Point p1, Point p2, Point p3)
		{
			return Math.Abs((p1.X * (p2.Y - p3.Y) + p2.X * (p3.Y - p1.Y) + p3.X * (p1.Y - p2.Y)) / 2.0);
		}
	}
}
