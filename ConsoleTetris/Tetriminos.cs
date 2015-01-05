using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
	/// <summary>
	/// The possible shapes of a Tetriminos.
	/// </summary>
	enum BlockType
	{
		I, J, L, O, S, T, Z
	}

   
	/// <summary>
	/// This class represent a block in the Tetris game.
	/// </summary>
	class Tetriminos
	{
		/// <summary>
		/// Gets the Tetriminos width.
		/// </summary>
		public int Width { get; private set; }


		/// <summary>
		/// Gets the Tetriminos height;
		/// </summary>
		public int Height { get; private set; }

		private BlockType _type;
		/// <summary>
		/// Gets the Tetriminos type.
		/// </summary>
		public BlockType Type 
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				_rotationAngle = 0;
				_blockSequence = Sequence(_rotationAngle);
			}
		}
		/// <summary>
		/// Gets the structure of the current block.
		/// </summary>
		public int[] BlockSequence
		{
			get
			{
                var arr = new int[8];
				Array.Copy(_blockSequence, arr , 8);
                return arr;
			}
		}

		private int[] _blockSequence;

		/// <summary>
		/// Initializes a new instanse of the Tetriminos class.
		/// </summary>
		/// <param name="type">The type of the Tetriminos to be initialized.</param>
		public Tetriminos(BlockType type)
		{
			Type = type;
			_blockSequence = Sequence(0);
		}

		/// <summary>
		/// Draws the Tetriminos block on the screen.
		/// </summary>
		/// <param name="left">The start position y-coordinate.</param>
		/// <param name="top">The start position x-coordinate.</param>
		public void Draw(int left, int top)
		{
			for (int i = 0; i < 4; i++)
			{
				DrawPortion(left + _blockSequence[2 * i], top + _blockSequence[2 * i + 1]);
			}
			
		}

		/// <summary>
		/// Clears the Tetriminos block on the screen.
		/// </summary>
		/// <param name="left">The start position y-coordinate.</param>
		/// <param name="top">The start position x-coordinate.</param>
		public void Clear(int left, int top)
		{
			for (int i = 0; i < 4; i++)
			{
				Console.MoveBufferArea(
					0,
					0,
					2,
					2,
					left + _blockSequence[2 * i],
					top + _blockSequence[2 * i + 1]);
			}
		}

		private int _rotationAngle = 0;

		/// <summary>
		///  Rotate the tetriminos block 90 degrees clockwise. It adds 90 degrees to the last rotation done.
		/// </summary>
		public void Rotate()
		{
			Rotate((_rotationAngle + 1) % 4);
		}
		/// <summary>
		///  Rotate the tetriminos block 90 degrees clockwise.
		/// </summary>
		/// <param name="rotationAngle">The rotation angle. A value from (0,1,2,3) which maps to (0, 90, 180, 270).</param>
		public void Rotate(int rotationAngle)
		{
			_rotationAngle = rotationAngle;

			_blockSequence = Sequence(_rotationAngle);


		}
		/// <summary>
		/// Draws one fourth of a Tetriminos block on the screen.
		/// </summary>
		/// <param name="left">The start position y-coordinate.</param>
		/// <param name="top">The start position x-coordinate.</param>
		private void DrawPortion(int left, int top)
		{
			Console.BackgroundColor = Color();

			Console.SetCursorPosition(left, top);
			Console.Write("  ");
			Console.SetCursorPosition(left, top + 1);
			Console.Write("  ");

			Console.ResetColor();
		}


		#region Rotation Sequences

		/// <summary>
		/// Returns the structure of the Tetriminos block.
		/// </summary>
		/// <returns>The structure of the Tetriminos block.</returns>
		private int[] Sequence_0()
		{

			switch (Type)
			{
				case BlockType.I:
					Width = 8;
					Height = 2;
					return new int[] {  0, 0 ,  2, 0 ,  4, 0 ,  6, 0  };
				case BlockType.J:
					Width = 6;
					Height = 4;
					return new int[] {  0, 0 ,  0, 2 ,  2, 2 ,  4, 2  };
				case BlockType.L:
					Width = 6;
					Height = 4;
					return new int[] {  0, 2 ,  2, 2 ,  4, 2 ,  4, 0  };
				case BlockType.O:
					Width = 4;
					Height = 4;
					return new int[] {  0, 0 ,  0, 2 ,  2, 0 ,  2, 2  };
				case BlockType.S:
					Width = 6;
					Height = 4;
					return new int[] {  0, 2 , 2, 0 ,  2, 2 ,  4, 0 };
				case BlockType.T:
					Width = 6;
					Height = 4;
					return new int[] {  0, 2 ,  2, 0,  2, 2 ,  4, 2  };
				case BlockType.Z:
					Width = 6;
					Height = 4;
					return new int[] {  0, 0 ,  2, 0 ,  2, 2 ,  4, 2  };
			}
			return null;
		}

		/// <summary>
		/// Returns the structure of the Tetriminos block rotated by 90 degrees.
		/// </summary>
		/// <returns>The structure of the Tetriminos block.</returns>
		private int[] Sequence_1()
		{

			switch (Type)
			{
				case BlockType.I:
					Width = 2;
					Height = 8;
					return new int[] { 0, 0, 0, 2, 0, 4, 0, 6 };
				case BlockType.J:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 0, 2, 0, 4, 2, 0 };
				case BlockType.L:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 0, 2, 0, 4, 2, 4 };
				case BlockType.O:
					Width = 4;
					Height = 4;
					return new int[] { 0, 0, 0, 2, 2, 0, 2, 2 };
				case BlockType.S:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 0, 2, 2, 2, 2, 4 };
				case BlockType.T:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 0, 2, 0, 4, 2, 2 };
				case BlockType.Z:
					Width = 4;
					Height = 6;
					return new int[] { 0, 2, 0, 4, 2, 2, 2, 0 };
			}
			return null;
		}

		/// <summary>
		/// Returns the structure of the Tetriminos block rotated by 180 degrees.
		/// </summary>
		/// <returns>The structure of the Tetriminos block.</returns>
		private int[] Sequence_2()
		{

			switch (Type)
			{
				case BlockType.I:
					Width = 8;
					Height = 2;
					return new int[] { 0, 0, 2, 0, 4, 0, 6, 0 };
				case BlockType.J:
					Width = 6;
					Height = 4;
					return new int[] { 0, 0, 2, 0, 4, 0, 4, 2 };
				case BlockType.L:
					Width = 6;
					Height = 4;
					return new int[] { 0, 0, 0, 2, 2, 0, 4, 0 };
				case BlockType.O:
					Width = 4;
					Height = 4;
					return new int[] { 0, 0, 0, 2, 2, 0, 2, 2 };
				case BlockType.S:
					Width = 6;
					Height = 4;
					return new int[] {0, 0, 2, 0, 2, 2, 4, 2 }; 
				case BlockType.T:
					Width = 6;
					Height = 4;
					return new int[] { 0, 0, 2, 0, 2, 2, 4, 0 };
				case BlockType.Z:
					Width = 6;
					Height = 4;
					return new int[] { 0, 2, 2, 0, 2, 2, 4, 0 };
			}
			return null;
		}

		/// <summary>
		/// Returns the structure of the Tetriminos block rotated by 270 degrees.
		/// </summary>
		/// <returns>The structure of the Tetriminos block.</returns>
		private int[] Sequence_3()
		{

			switch (Type)
			{
				case BlockType.I:
					Width = 2;
					Height = 8;
					return new int[] { 0, 0, 0, 2, 0, 4, 0, 6 };
				case BlockType.J:
					Width = 4;
					Height = 6;
					return new int[] { 0, 4, 2, 0, 2, 2, 2, 4 };
				case BlockType.L:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 2, 0, 2, 2, 2, 4 };
				case BlockType.O:
					Width = 4;
					Height = 4;
					return new int[] { 0, 0, 0, 2, 2, 0, 2, 2 };
				case BlockType.S:
					Width = 4;
					Height = 6;
					return new int[] { 0, 2, 0, 4, 2, 2, 2, 0};
				case BlockType.T:
					Width = 4;
					Height = 6;
					return new int[] { 0, 2, 2, 0, 2, 2, 2, 4 };
				case BlockType.Z:
					Width = 4;
					Height = 6;
					return new int[] { 0, 0, 0, 2, 2, 2, 2, 4 };
			}
			return null;
		}
		/// <summary>
		/// Returns the structure of the Tetriminos block.
		/// </summary>
		/// <returns>The structure of the Tetriminos block.</returns>
		private int[] Sequence(int degree)
		{
			if (degree == 0)
				return Sequence_0();
			if (degree == 1)
				return Sequence_1();
			if (degree == 2)
				return Sequence_2();
			if (degree == 3)
				return Sequence_3();

			return null;
		}

		#endregion

		/// <summary>
		/// Retrives the color of the Tetriminos block.
		/// </summary>
		/// <returns>The color of the Tetriminos block.</returns>
		private ConsoleColor Color()
		{
			switch (Type)
			{
				case BlockType.I:
					return ConsoleColor.Cyan;
				case BlockType.J:
					return ConsoleColor.Blue;
				case BlockType.L:
					return ConsoleColor.DarkYellow;
				case BlockType.O:
					return ConsoleColor.Yellow;
				case BlockType.S:
					return ConsoleColor.Green;
				case BlockType.T:
					return ConsoleColor.Magenta;
				case BlockType.Z:
					return ConsoleColor.Red;
			}
			return ConsoleColor.Black;
		}

	
	}
}
