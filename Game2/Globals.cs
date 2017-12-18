using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
	public static class Globals
	{
		private static int _screenWidth = 1920;
		private static int _screenHeight = 1080;
		private static Random _random = new Random();
		public static int ScreenWidth { get { return _screenWidth; } }
		public static int ScreenHeight { get { return _screenHeight; } }
		public static Random rand { get { return _random; } }
		public static float randomf(float h, float l) { return (float)rand.NextDouble() * (h - l) + l; }
		public static float ToRadian(float d) { return (d * (float)Math.PI / 180); }
	}
}
