using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2.Objects
{

	public enum Tetromino_Type
	{
		N = 0, // Null type
		O,
		I,
		T,
		L,
		J,
		S,
		Z,
		Hl, // Highlight type
		Hh, // Highlight type
	}

	public enum Kick_Offsets
	{
		N = -1,
		ZR = 0,
		RZ,
		RT,
		TR,
		TL,
		LT,
		LZ,
		ZL
	}

	class Tetris
	{
		/* JLSTZ Tetromino Kick Data */
		public static readonly int[,,] JLSTZKickTable =
		{
/*0->R*/    { { 0, 0}, {-1, 0}, {-1, 1}, { 0,-2}, {-1,-2} },
/*R->0*/    { { 0, 0}, {+1, 0}, {+1,-1}, { 0,+2}, {+1,+2} },
/*R->2*/    { { 0, 0}, {+1, 0}, {+1,-1}, { 0,+2}, {+1,+2} },
/*2->R*/	{ { 0, 0}, {-1, 0}, {-1,+1}, { 0,-2}, {-1,-2} },
/*2->L*/    { { 0, 0}, {+1, 0}, {+1,+1}, { 0,-2}, {+1,-2} },
/*L->2*/    { { 0, 0}, {-1, 0}, {-1,-1}, { 0,+2}, {-1,+2} },
/*L->0*/    { { 0, 0}, {-1, 0}, {-1,-1}, { 0,+2}, {-1,+2} },
/*0->L*/    { { 0, 0}, {+1, 0}, {+1,+1}, { 0,-2}, {+1,-2} }
		};

		/* I Tetromino Kick Data */
		public static readonly int[,,] IKickTable =
		{
/*0->R*/    { { 0, 0}, {-2, 0}, {+1, 0}, {-2,-1}, {+1,+2} },
/*R->0*/    { { 0, 0}, {+2, 0}, {-1, 0}, {+2,+1}, {-1,-2} },
/*R->2*/    { { 0, 0}, {-1, 0}, {+2, 0}, {-1,+2}, {+2,-1} },
/*2->R*/    { { 0, 0}, {+1, 0}, {-2, 0}, {+1,-2}, {-2,+1} },
/*2->L*/    { { 0, 0}, {+2, 0}, {-1, 0}, {+2,+1}, {-1,-2} },
/*L->2*/    { { 0, 0}, {-2, 0}, {+1, 0}, {-2,-1}, {+1,+2} },
/*L->0*/    { { 0, 0}, {+1, 0}, {-2, 0}, {+1,-2}, {-2,+1} },
/*0->L*/    { { 0, 0}, {-1, 0}, {+2, 0}, {-1,+2}, {+2,-1} }
		};

		private List<Tetromino> TetrominoQueue;
		public Texture2D tetroTexture;
		public Texture2D highlightTexture;

		public int[,] l_field;

		public int currentX = 0;
		public int currentY = 0;

		public int rotation = 0;

		public double moveTickTimeStart = 0.5;
		public double moveTickTimeSpeed = 0.025; // seconds
		public double moveTickTime = 0.5; // seconds
		public double moveClock = 0;
		public double spawnTimer = 0.5;
		public double spawnDelayClock = 0;
		public Tetris()
		{
			l_field = new int[G.FieldWidth, G.FieldHeight];
			for (int i = 0; i < G.FieldWidth; i++)
			{
				for (int j = 0; j < G.FieldHeight; j++)
				{
					l_field[i, j] = 0;
				}
			}

			TetrominoQueue = new List<Tetromino>();
			do
			{
				TetrominoQueue.Clear();
				EnqueueTetrominos(TetrominoQueue);
				// Can't start on S or Z
			}
			while (TetrominoQueue.ElementAt(0).tetroType == Tetromino_Type.S || TetrominoQueue.ElementAt(0).tetroType == Tetromino_Type.Z);
			if (TetrominoQueue.ElementAt(0).tetroType == Tetromino_Type.O)
			{
				currentX = 4;
			}
			else
			{
				currentX = 3;
			}
			// dequeue
			// clear
			// peek
		}

		// TODO: this but better
		private void EnqueueTetrominos(List<Tetromino> tl)
		{
			List<Tetromino> bag7 = new List<Tetromino>();
			// Generate the 7 different pieces
			for (int i = 1; i < 8; i++)
			{
				bag7.Add(new Tetromino((Tetromino_Type)i));
			}

			// place them into the array in random order
			for (int i = 0; i < 7; i++)
			{
				int idx = G.Randomi(0, bag7.Count);
				tl.Add(bag7.ElementAt(idx));
				bag7.RemoveAt(idx);
			}
		}

		private bool CheckTetromino(Tetromino t, int fx, int fy)
		{
			for (int x = 0; x < t.l_tetro.GetLength(2); x++)
			{
				for (int y = 0; y < t.l_tetro.GetLength(1); y++)
				{
					if (t.l_tetro[rotation, y, x] != 0)
					{
						if (fy + y + 1 >= G.FieldHeight || l_field[fx + x, fy + y + 1] != 0)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// TODO: can move function, checks if able to move to location
		private bool CanMove(Tetromino t, int fx, int fy, int r)
		{
			for (int x = 0; x < t.l_tetro.GetLength(2); x++)
			{
				for (int y = 0; y < t.l_tetro.GetLength(1); y++)
				{
					if (t.l_tetro[r, y, x] != 0)
					{
						if (fx + x >= 0 && fx + x < G.FieldWidth && fy + y >= 0 && fy + y < G.FieldHeight)
						{
							if (l_field[fx + x, fy + y] != 0)
							{
								return false;
							}
						}
						else
						{
							return false;
						}
						
					}
					
				}
			}
			return true;
		}

		private void PlaceTetromino()
		{
			for (int x = 0; x < TetrominoQueue.ElementAt(0).l_tetro.GetLength(2); x++)
			{
				for (int y = 0; y < TetrominoQueue.ElementAt(0).l_tetro.GetLength(1); y++)
				{
					if (TetrominoQueue.ElementAt(0).l_tetro[rotation, y, x] != 0)
					{
						l_field[currentX + x, currentY + y] = (int)TetrominoQueue.ElementAt(0).tetroType;
					}
				}
			}

			// Check for rows to clear
			// TODO: move to function
			bool[] rowsToClear = new bool[G.FieldHeight];
			int rowsCleared = G.FieldHeight;
			for (int row = 0; row < G.FieldHeight; row++)
			{
				rowsToClear[row] = true;
				for (int col = 0; col < G.FieldWidth; col++)
				{
					if (l_field[col, row] == 0)
					{
						rowsToClear[row] = false;
						rowsCleared--;
						break;
					}
				}
			}
			if (rowsCleared > 0)
			{
				for (int row = 0; row < G.FieldHeight; row++)
				{
					if (rowsToClear[row])
					{
						for (int row2 = row; row2 > 0; row2--)
						{
							for (int col = 0; col < G.FieldWidth; col++)
							{
								l_field[col, row2] = l_field[col, row2 - 1];
							}
						}
					}
				}
			}

			// TODO: reset function
			TetrominoQueue.RemoveAt(0);
			rotation = 0;
			currentX = 4;
			currentY = 0;
			moveClock = 0;
			spawnDelayClock = 0;
			if (TetrominoQueue.Count <= 1)
			{
				EnqueueTetrominos(TetrominoQueue);
			}
		}

		public void Update(GameTime gameTime)
		{
			if (spawnDelayClock >= spawnTimer)
			{
				if (moveClock >= moveTickTime)
				{
					moveClock = 0;
					if (CheckTetromino(TetrominoQueue.ElementAt(0), currentX, currentY))
					{
						PlaceTetromino();
					}
					currentY++;
				}
				else
				{
					moveClock += gameTime.ElapsedGameTime.TotalSeconds;
				}
			}
			else
			{
				spawnDelayClock += gameTime.ElapsedGameTime.TotalSeconds;
			}
		}
		
		private void DrawGhost(SpriteBatch spriteBatch)
		{
			Tetromino hTetromino = new Tetromino(TetrominoQueue.ElementAt(0))
			{
				tetroType = Tetromino_Type.Hh
			};

			for (int y = currentY; y < G.FieldHeight; y++)
			{
				if (CheckTetromino(hTetromino, currentX, y))
				{
					if (y == currentY)
						break;
					hTetromino.Draw(spriteBatch, tetroTexture, currentX, y, rotation);
					break;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < G.FieldWidth; i++)
			{
				for (int j = 0; j < G.FieldHeight; j++)
				{
					Tetromino.DrawTetro(spriteBatch, tetroTexture, (Tetromino_Type)l_field[i, j], i, j);
				}
			}
			DrawGhost(spriteBatch);
			TetrominoQueue.ElementAt(0).Draw(spriteBatch, tetroTexture, currentX, currentY, rotation);
			TetrominoQueue.ElementAt(1).Draw(spriteBatch, tetroTexture, 15, 0, 0);
		}
		
		public void Move(int direction)
		{
			for (int x = 0; x < TetrominoQueue.ElementAt(0).l_tetro.GetLength(2); x++)
			{
				for (int y = 0; y < TetrominoQueue.ElementAt(0).l_tetro.GetLength(1); y++)
				{
					if (TetrominoQueue.ElementAt(0).l_tetro[rotation, y, x] != 0)
					{
						if (currentX + x + direction < 0 || currentX + x + direction >= G.FieldWidth)
						{
							return;
						}
						else if (l_field[currentX + x + direction, currentY + y] != 0)
						{
							return;
						}
					}
				}
			}
			currentX += direction;
		}

		public void Rotate(int direction)
		{
			if (TetrominoQueue.ElementAt(0).tetroType != Tetromino_Type.O)
			{
				int newr = -1;
				if (direction > 0 && rotation >= TetrominoQueue.ElementAt(0).l_tetro.GetLength(0) - 1)
				{
					newr = 0;
				}
				else if (direction > 0)
				{
					newr = rotation + 1;
				}
				else if (direction < 0 && rotation <= 0)
				{
					newr = TetrominoQueue.ElementAt(0).l_tetro.GetLength(0) - 1;
				}
				else if (direction < 0)
				{
					newr = rotation - 1;
				}

				if (newr >= 0)
				{
					// TODO: this but better?
					Kick_Offsets ko = Kick_Offsets.N;
					switch(rotation)
					{
						case 0:
							switch(newr)
							{
								case 1:
									ko = Kick_Offsets.ZR;
									break;
								case 3:
									ko = Kick_Offsets.ZL;
									break;
							}
							break;
						case 1:
							switch (newr)
							{
								case 2:
									ko = Kick_Offsets.RZ;
									break;
								case 0:
									ko = Kick_Offsets.RT;
									break;
							}
							break;
						case 2:
							switch (newr)
							{
								case 3:
									ko = Kick_Offsets.TR;
									break;
								case 1:
									ko = Kick_Offsets.TL;
									break;
							}
							break;
						case 3:
							switch (newr)
							{
								case 0:
									ko = Kick_Offsets.LT;
									break;
								case 2:
									ko = Kick_Offsets.LZ;
									break;
							}
							break;
					}
					if (ko >= 0)
					{
						Tetromino_Type tt = TetrominoQueue.ElementAt(0).tetroType;
						int offsetindex = -1;
						int x = -1;
						int y = -1;
						// TODO: hardcoded 5 kick checks
						for (int i = 0; i < 5; i++)
						{
							switch (tt)
							{
								case Tetromino_Type.I:
									x = currentX + IKickTable[(int)ko, i, 0];
									y = currentY + IKickTable[(int)ko, i, 1];
									break;
								default:
									x = currentX + JLSTZKickTable[(int)ko, i, 0];
									y = currentY + JLSTZKickTable[(int)ko, i, 1];
									break;
							}
							if (CanMove(TetrominoQueue.ElementAt(0), x, y, newr))
							{
								offsetindex = i;
								break;
							}
						}
						if (offsetindex >= 0)
						{
							switch (tt)
							{
								case Tetromino_Type.I:
									currentX += IKickTable[(int)ko, offsetindex, 0];
									currentY += IKickTable[(int)ko, offsetindex, 1];
									break;
								default:
									currentX += JLSTZKickTable[(int)ko, offsetindex, 0];
									currentY += JLSTZKickTable[(int)ko, offsetindex, 1];
									break;
							}
							rotation = newr;
							// Reset move clock means infinite clock if a piece can rotate
							moveClock = 0;
						}
					}
				}
			}
		}
		
		public void Slam()
		{
			for (int i = currentY; i < G.FieldHeight; i++)
			{
				if (CheckTetromino(TetrominoQueue.ElementAt(0), currentX, currentY))
				{
					PlaceTetromino();
					break;
				}
				currentY++;
			}
		}
		
		public void Speed(bool b)
		{
			// running in the 90s
			if (b)
				moveTickTime = moveTickTimeSpeed;
			else
				moveTickTime = moveTickTimeStart;
		}
	}
}
