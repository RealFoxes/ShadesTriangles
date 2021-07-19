using ShadesTriangles.Extenstions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadesTriangles.Entities
{
	public class Triangle
	{
		public int Depth { get; set; }
		public Point P1 { get; set; } // a
		public Point P2 { get; set; } // b
		public Point P3 { get; set; } // c
		public Point[] GetPoints()
		{
			return new Point[] { P1, P2, P3 };
		}
		public double GetPerimeter()
		{
			var ab = Math.Sqrt(Math.Pow((P1.X - P2.X), 2) + Math.Pow((P1.Y - P2.Y), 2));
			var bc = Math.Sqrt(Math.Pow((P2.X - P3.X), 2) + Math.Pow((P2.Y - P3.Y), 2));
			var ca = Math.Sqrt(Math.Pow((P3.X - P1.X), 2) + Math.Pow((P3.Y - P1.Y), 2));

			var p = (ab + bc + ca) / 2;

			return p;
		}

		public bool IsInTriangle(Triangle triangle)
		{
			foreach (var point in GetPoints())
				if (point.isInside(triangle))
					return true;

			return false;
		}
	}
}
