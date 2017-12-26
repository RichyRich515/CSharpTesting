using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2.Objects
{
	class Tetromino
	{
		// TODO: move these two variables to Tetris.cs??
		public int[,,] l_tetro;
		public Tetromino_Type tetroType;
		
		public Tetromino(Tetromino_Type type)
		{
			tetroType = type;

			switch (tetroType)
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
					l_tetro = TetrominoFactory.Tetromino_L; // TODO
					break;
				case Tetromino_Type.J:
					l_tetro = TetrominoFactory.Tetromino_J; // TODO
					break;
				case Tetromino_Type.S:
					l_tetro = TetrominoFactory.Tetromino_S; // TODO
					break;
				case Tetromino_Type.Z:
					l_tetro = TetrominoFactory.Tetromino_Z; // TODO
					break;
				default:
					break;
			}
		}
		public Tetromino(Tetromino og)
		{
			this.l_tetro = og.l_tetro;
			this.tetroType = og.tetroType;
		}

		static public Color GetColorFromType(Tetromino_Type type)
		{
			switch (type)
			{
				case Tetromino_Type.O:
					return Color.Yellow;
				case Tetromino_Type.I:
					return Color.Cyan;
				case Tetromino_Type.T:
					return Color.Purple;
				case Tetromino_Type.L:
					return Color.Orange;
				case Tetromino_Type.J:
					return Color.Blue;
				case Tetromino_Type.S:
					return Color.Green;
				case Tetromino_Type.Z:
					return Color.Red;
				case Tetromino_Type.Hl:
					return new Color(200, 200, 200);
				case Tetromino_Type.Hh:
					return new Color(254, 254, 254);
				default:
					return new Color(10, 10, 10);
			}
		}

		static public void DrawTetro(SpriteBatch spriteBatch, Texture2D texture, Tetromino_Type type, int x, int y)
		{
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			spriteBatch.Draw(texture, new Rectangle(G.FieldOffsetX + x * G.TetrominoWidth, G.FieldOffsetY + y * G.TetrominoHeight, G.TetrominoWidth, G.TetrominoHeight), GetColorFromType(type));
			spriteBatch.End();
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D texture, int fieldx, int fieldy, int r)
		{
			for (int x = 0; x < l_tetro.GetLength(2); x++)
			{
				for (int y = 0; y < l_tetro.GetLength(1); y++)
				{
					if (l_tetro[r, y, x] > 0)
					{
						DrawTetro(spriteBatch, texture, this.tetroType, fieldx + x, fieldy + y);
					}
				}
			}
		}
	}
}
