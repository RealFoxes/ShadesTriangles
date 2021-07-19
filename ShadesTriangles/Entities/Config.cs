using Newtonsoft.Json;
using ShadesTriangles.Entities;
using ShadesTriangles.Extenstions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadesTriangles.Entities
{
	public class Config
	{
		private Config() { }
		private static string path = AppDomain.CurrentDomain.BaseDirectory + "config.json";
		private static bool IsExist() => File.Exists(path);
		public static Config GetConfig(out bool isCreated)
		{
			var config = new Config();
			isCreated = !IsExist();

			if (IsExist())
			{
				config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
				config.Triangles.RemoveAt(0);
			}
			else
				File.WriteAllText(path,JsonConvert.SerializeObject(config, Formatting.Indented));
			
			return config;
		}
		public Color BaseColor { get; set; } = Color.FromArgb(0, 125, 0);
		public ushort TrianglesCount { get; set; } = 1;
		public List<Triangle> Triangles { get; set; } = new List<Triangle> { new Triangle { 
			P1 = new Point(100,100),
			P2 = new Point(300,100),
			P3 = new Point(300,300)
		}};
	}
	
}
