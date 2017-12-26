using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2.Objects
{
	class TetrominoFactory
	{
		public static readonly int[,,] Tetromino_O = 
			{ {
				{ 1, 1 },
				{ 1, 1 }
			} };

		public static readonly int[,,] Tetromino_I =
			{ {
				{ 0, 0, 0, 0 },
				{ 1, 1, 1, 1 },
				{ 0, 0, 0, 0 },
				{ 0, 0, 0, 0 }
			},{
				{ 0, 0, 1, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 1, 0 }
			},{
				{ 0, 0, 0, 0 },
				{ 0, 0, 0, 0 },
				{ 1, 1, 1, 1 },
				{ 0, 0, 0, 0 }
			},{
				{ 0, 1, 0, 0 },
				{ 0, 1, 0, 0 },
				{ 0, 1, 0, 0 },
				{ 0, 1, 0, 0 }
			} };

		public static readonly int[,,] Tetromino_T = 
			{ { 
				{ 0, 1, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 0 }
			},{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 1, 0 }
			},{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 1, 0 }
			},{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 0 }
			} };

		public static readonly int[,,] Tetromino_L =
			{ {
				{ 0, 0, 1 },
				{ 1, 1, 1 },
				{ 0, 0, 0 }
			},{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 1 }
			},{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 1, 0, 0 }
			},{
				{ 1, 1, 0 },
				{ 0, 1, 0 },
				{ 0, 1, 0 }
			} };

		public static readonly int[,,] Tetromino_J =
			{ {
				{ 1, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 0 }
			},{
				{ 0, 1, 1 },
				{ 0, 1, 0 },
				{ 0, 1, 0 }
			},{
				{ 0, 0, 0 },
				{ 1, 1, 1 },
				{ 0, 0, 1 }
			},{
				{ 0, 1, 0 },
				{ 0, 1, 0 },
				{ 1, 1, 0 }
			} };

		public static readonly int[,,] Tetromino_S =
			{ {
				{ 0, 1, 1 },
				{ 1, 1, 0 },
				{ 0, 0, 0 }
			},{
				{ 0, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 0, 1 }
			},{
				{ 0, 0, 0 },
				{ 0, 1, 1 },
				{ 1, 1, 0 }
			},{
				{ 1, 0, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 0 }
			} };

		public static readonly int[,,] Tetromino_Z =
			{ {
				{ 1, 1, 0 },
				{ 0, 1, 1 },
				{ 0, 0, 0 }
			},{
				{ 0, 0, 1 },
				{ 0, 1, 1 },
				{ 0, 1, 0 }
			},{
				{ 0, 0, 0 },
				{ 1, 1, 0 },
				{ 0, 1, 1 }
			},{
				{ 0, 1, 0 },
				{ 1, 1, 0 },
				{ 1, 0, 0 }
			} };
	}
}
