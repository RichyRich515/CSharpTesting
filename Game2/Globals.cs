using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2
{
	public static class G
	{
		private static int _screenWidth = 1920;
		private static int _screenHeight = 1080;
		private static int _fieldWidth = 10;
		private static int _fieldHeight = 20;
		private static int _fieldOffsetX = 400;
		private static int _fieldOffsetY = 70;
		private static int _tetrominoWidth = 42;
		private static int _tetrominoHeight = 42;
		private static Random _random = new Random();
		public static int ScreenWidth { get { return _screenWidth; } }
		public static int ScreenHeight { get { return _screenHeight; } }
		public static int FieldWidth { get { return _fieldWidth; } }
		public static int FieldHeight { get { return _fieldHeight; } }
		public static int FieldOffsetX { get { return _fieldOffsetX; } }
		public static int FieldOffsetY { get { return _fieldOffsetY; } }
		public static int TetrominoWidth { get { return _tetrominoWidth; } }
		public static int TetrominoHeight { get { return _tetrominoHeight; } }
		public static Random Rand { get { return _random; } }
		public static float Randomf(float h, float l) { return (float)Rand.NextDouble() * (h - l) + l; } // TODO: l, h
		public static int Randomi(int l, int h) { return Rand.Next(l, h); }
		public static float ToRadian(float d) { return (d * (float)Math.PI / 180); }
	}
}
