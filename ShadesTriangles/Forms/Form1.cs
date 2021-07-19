using Newtonsoft.Json;
using ShadesTriangles.Entities;
using ShadesTriangles.Extensions;
using ShadesTriangles.Extenstions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadesTriangles.Forms
{
	public partial class Form1 : Form
	{
		private Config config;
		
		public Form1()
		{
			InitializeComponent();
			config = Config.GetConfig(out bool isCreated);

			if (isCreated)
				MessageBox.Show("Был создан конфигурационный файл, используются стандартные значения");
		}
		private bool CheckAllTriangles()
		{
			foreach (var triangle in config.Triangles)
			{
				foreach (var triangle2 in config.Triangles)
				{
					if (triangle == triangle2) continue;
					if (TriangleTriangleCollision(triangle, triangle2)) return true;
				}
			}
			return false;
		}
		// Recursive method that marks depth
		private void AddDepthToTriangles(List<Triangle> triangles, int iteration)
		{
			foreach (var triangle in triangles)
			{
				if (triangle.Depth != 0) continue;

				var trianglesInner = GetTrianglesInner(triangle);

				if (trianglesInner.Count != 0)
					AddDepthToTriangles(trianglesInner, iteration + 1);

				triangle.Depth = iteration;

				if (Convert.ToInt32(Text) < iteration) Text = iteration.ToString();
			}
		}

		private List<Triangle> GetTrianglesInner(Triangle CurTriangle)
		{
			List<Triangle> triangles = new List<Triangle>();

			foreach (var triangle in config.Triangles)
			{
				if (triangle.IsInTriangle(CurTriangle)) triangles.Add(triangle);
			}
			triangles.Remove(CurTriangle);
			return triangles;
		}

		// Проверяет два треугольника на пересечение.
		private bool TriangleTriangleCollision(Triangle a, Triangle b)
		{
			var aPoints = a.GetPoints();
			var bPoints = b.GetPoints();

			// Если хотя бы одна пара ребер разных треугольников
			// пересекается, то треугольники пересекаются.
			for (int i = 0; i < 3; i++)
				for (int j = 0; j < 3; j++)
				{
					int i_next = (i + 1) % 3;
					int j_next = (j + 1) % 3;
					if (EdgeEdgeCollision(aPoints[i], aPoints[i_next], bPoints[j], bPoints[j_next]))
						return true;
				}

			// Иначе - нет пересечения.
			return false;
		}

		// Проверяет два отрезка A и B на пересечение
		private bool EdgeEdgeCollision(Point a1, Point a2, Point b1, Point b2)
		{
			return GetWinding(a1, a2, b1) * GetWinding(a1, a2, b2) <= 0 &&
				   GetWinding(b1, b2, a1) * GetWinding(b1, b2, a2) <= 0;
		}

		// Знак возвращаемого значения показывает, с какой стороны (слева или справа)
		// точка C находится от вектора AB.
		private float GetWinding(Point a, Point b, Point c)
		{
			// Находятся вектора AB и AC. Дополняются до трехмерных добавлением Z = 0.
			// Вычисляется их векторное произведение, от которого берется только
			// Z-координата.
			float x1 = b.X - a.X, y1 = b.Y - a.Y;
			float x2 = c.X - a.X, y2 = c.Y - a.Y;
			return x1 * y2 - x2 * y1;
		}
		private void Form1_Shown(object sender, EventArgs e)
		{
			// Sort triangles from more to less for current draw 
			config.Triangles = config.Triangles.OrderByDescending(t => t.GetPerimeter()).ToList();

			Text = "0";

			if (CheckAllTriangles())
			{
				DrawError();
				return;
			}

			AddDepthToTriangles(config.Triangles, 1);
			Text = "Количество оттенков: " + Text;

			Draw();
		}

		private void Draw()
		{
			var g = this.CreateGraphics();
			var color = config.BaseColor;
			//Drawing all triangles
			for (int i = 0; i < config.TrianglesCount; i++)
			{
				g.FillPolygon(new SolidBrush(color.GetBrightnessByDepth(config.Triangles[i].Depth)), config.Triangles[i].GetPoints());
			}
			
		}
		private void DrawError()
		{
			var g = this.CreateGraphics();
			var font = new Font(FontFamily.GenericMonospace, 100.0F, FontStyle.Regular, GraphicsUnit.Pixel);
			g.DrawString("ERROR", font, new SolidBrush(Color.Red),0,0);
		}

		
	}
}
