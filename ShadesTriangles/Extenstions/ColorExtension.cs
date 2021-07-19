using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadesTriangles.Extensions
{
	public static class ColorExtension
	{

		public static Color GetBrightnessByDepth(this Color color, double depth)
		{
			return Color.FromArgb(color.A, 
				(int)(color.R / depth),
				(int)(color.G / depth),
				(int)(color.B / depth));
		}
	}
}
