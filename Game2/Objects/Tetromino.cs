using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Objects
{
	public enum Tetromino_Type
	{
		N = 0,
		O,
		I,
		T,
		L,
		J,
		S,
		Z
	}
	class Tetromino
	{
		public int[,,] l_tetro;

		public Tetromino_Type tetrotype;

		public Tetromino(Tetromino_Type type)
		{
			this.tetrotype = type;
			switch (type)
			{
				case Tetromino_Type.O:
					l_tetro = TetrominoFactory.Tetromino_O;
					break;
				case Tetromino_Type.I:
					l_tetro = TetrominoFactory.Tetromino_I;
					break;
				case Tetromino_Type.T:
					l_tetro = TetrominoFactory.Tetromino_T;
					break;
				case Tetromino_Type.L:
					l_tetro = TetrominoFactory.Tetromino_O; //
					break;
				case Tetromino_Type.J:
					l_tetro = TetrominoFactory.Tetromino_O; // 
					break;
				case Tetromino_Type.S:
					l_tetro = TetrominoFactory.Tetromino_O; //
					break;
				case Tetromino_Type.Z:
					l_tetro = TetrominoFactory.Tetromino_O; //
					break;
				default:
					break;
			}
		}


	}
}
