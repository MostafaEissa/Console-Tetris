using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTetris
{
	/// <summary>
	/// This class is responsible for drawing the game board and the game pieces to the screen.
	/// </summary>
	class GameBoard
	{
		private int _score;
		/// <summary>
		/// Gets the current game score.
		/// </summary>
		public int Score 
		{
			get
			{
				return _score;
			}
			private set
			{
				_score = value;
				UpdateScore();
			}
		}

		private int _lines;
		/// <summary>
		/// Gets the number of cleared lines.
		/// </summary>
		public int Lines
		{
			get
			{
				return _lines;
			}
			private set
			{
				_lines = value;
				UpdateLines();
			}
		}

		private Tetriminos _nextPiece;

		/// <summary>
		/// Gets the next Tetriminos block.
		/// </summary>
		public Tetriminos NextPiece 
		{
			get
			{
				return _nextPiece;
			}
			private set
			{
				_nextPiece = value;
				DrawNextBlock();
			}
		}

		private Tetriminos _currentPiece;

		/// <summary>
		/// Gets the current Tetriminos block.
		/// </summary>
		public Tetriminos CurrentPiece
		{
			get
			{
				return _currentPiece;
			}
			private set
			{
				_currentPiece = value;
				DrawCurrentBlock();
			}
		}

		/// <summary>
		/// Updates the score achieved.
		/// </summary>
		private void UpdateScore()
		{
			Console.SetCursorPosition(11, 5);
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.Write(Score);
			Console.ResetColor();
		}

		/// <summary>
		/// Updates the number of lines achieved.
		/// </summary>
		private void UpdateLines()
		{
			Console.SetCursorPosition(11, 7);
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.Write(Lines);
			Console.ResetColor();
		}

		private const int _boardDepth = 36;
		private const int _boardWidth = 20;
		private const int _boardStartX = 20;
		private const int _centerX = 29;
		private const int _centerY = 1;
		private const int _nextX = 4;
		private const int _nextY = 11;

		/// <summary>
		/// Draws the grid for the first time.
		/// </summary>
		private void DrawGrid()
		{
			Console.ForegroundColor = ConsoleColor.DarkBlue;

			for (int i = 0; i < _boardDepth; i++)
			{

				Console.Write("".PadLeft(_boardStartX,' '));
				Console.Write("*");
				Console.Write("".PadLeft(_boardWidth, ' '));
				Console.Write("*");
				Console.WriteLine();

			}

			Console.Write("".PadLeft(_boardStartX, ' '));
			Console.WriteLine("".PadLeft(_boardWidth + 2, '*'));      
			Console.ResetColor();

			//draw score
			Console.SetCursorPosition(4, 5);
			Console.Write("SCORE:");
		   
			//draw lines
			Console.SetCursorPosition(4, 7);
			Console.Write("LINES: ");

			//draw next
			Console.SetCursorPosition(4, 9);
			Console.WriteLine("NEXT\n");
		   

		}

		/// <summary>
		/// Draws the current block on the specified location.
		/// </summary>
		private void DrawCurrentBlock()
		{
			if (_currentPieceXPosition != _currentPieceNextXPosition
				|| _currentPieceYPosition != _currentPieceNextYPosition)
			{
				_currentPiece.Clear(_currentPieceXPosition, _currentPieceYPosition);
				CurrentPiece.Draw(_currentPieceNextXPosition, _currentPieceNextYPosition);
				_currentPieceXPosition = _currentPieceNextXPosition;
				_currentPieceYPosition = _currentPieceNextYPosition;
			}
		}

		/// <summary>
		/// Draws the next block on the specified location.
		/// </summary>
		private void DrawNextBlock()
		{
			Console.MoveBufferArea(
				_nextX,
				_nextY + 4,
				8,
				4,
				_nextX,
				_nextY); 
			if (NextPiece != null)
				NextPiece.Draw(_nextX, _nextY);
		}

		/// <summary>
		/// Draws the help text on the right side of the screen.
		/// </summary>
		private void DrawHelp()
		{
			Console.SetCursorPosition(51, 5);
			Console.WriteLine("H E L P");
			Console.SetCursorPosition(50, 7);
			Console.Write("\u2190".PadRight(5,' '));
			Console.Write("Left");

			Console.SetCursorPosition(50, 9);
			Console.Write("\u2192".PadRight(5, ' '));  
			Console.Write("Right");

			Console.SetCursorPosition(50, 11);
			Console.Write("\u2191".PadRight(5, ' '));  
			Console.Write("Rotate");

			Console.SetCursorPosition(50, 13);
			Console.Write("\u2193".PadRight(5, ' '));  
			Console.Write("Speed Up");

			Console.SetCursorPosition(50, 15);
			Console.Write("P".PadRight(5, ' '));
			Console.WriteLine("Pause");

			Console.SetCursorPosition(50, 17);
			Console.Write("ESC".PadRight(5, ' ')); 
			Console.WriteLine("Exit");
		}

		/// <summary>
		/// Draws the game board on the screen.
		/// </summary>
		private void DrawGameBoard()
		{
			Console.MoveBufferArea(
				0,
				0,
				Console.WindowWidth,
				Console.WindowHeight,
				0,
				0);
		}

		private int _rotationAngle = 0;
		/// <summary>
		/// Advances the game based on the pressed key.
		/// </summary>
		/// <param name="consoleKeyInfo">The key the user pressed.</param>
		private void HandleKey(ConsoleKey consoleKey)
		{
			if(_paused)
			{
				if(consoleKey == ConsoleKey.P)
					Pause();
				return;
			}

			switch (consoleKey)
			{
				case ConsoleKey.LeftArrow:
					ChangePosition(Direction.X, Change.Decrement);
					ValidateMove();
					break;
				case ConsoleKey.RightArrow:
					ChangePosition(Direction.X, Change.Increment);
					ValidateMove();
					break;
				case ConsoleKey.UpArrow: 
					switch (_currentPiece.Type)
					{
						case BlockType.I:
						case BlockType.T:
						case BlockType.S:
						case BlockType.Z:
						case BlockType.J:
						case BlockType.L:							 
							 _rotationAngle = (_rotationAngle + 1) % 4;
							 var oldSeq = CurrentPiece.BlockSequence;
							if (_rotationAngle == 0 || _rotationAngle == 2)
								_currentPieceNextXPosition -= 2;
							else if (_rotationAngle == 1 || _rotationAngle == 3)
								_currentPieceNextXPosition += 2;
							CurrentPiece.Rotate(_rotationAngle);
							if(ValidateRotation(oldSeq))
							{
								CurrentPiece.Rotate((_rotationAngle + 3) % 4);
								CurrentPiece.Clear(_currentPieceXPosition, _currentPieceYPosition);
								CurrentPiece.Rotate(_rotationAngle);
							}
	
							break;
					}
					break;
				case ConsoleKey.DownArrow:
					ChangePosition(Direction.Y, Change.Increment);
					ValidateMove();
					break;
				case ConsoleKey.Escape:
					Environment.Exit(0);
					break;
				case ConsoleKey.Q:
					GenerateRandomPiece();
					break;
				case ConsoleKey.P:
					Pause();
					break;
			}
		}

		private bool _paused = false;
		private bool _visible = false;
		/// <summary>
		/// Pauses or unpaused the game.
		/// </summary>
		private void Pause()
		{
			_paused = !_paused;
			Console.MoveBufferArea(5, 21, 11, 1, 5, 20);
			_visible = true;
		}


		/// <summary>
		/// Draws the game board for the first time.
		/// </summary>
		public void Initialize()
		{
			Console.Clear();
			Console.WindowWidth = 65;
			Console.WindowHeight = 40;
			Console.WriteLine();
			Console.Title = "Play Tetris!";

			DrawGrid();
			DrawNextBlock();
			DrawHelp();

			Console.SetCursorPosition(0, 0);
			Console.CursorVisible = false;

			_current = _previous = DateTime.Now.Millisecond;
			_currentPieceXPosition = _currentPieceNextXPosition = _centerX;
			_currentPieceYPosition = _currentPieceNextYPosition = _centerY;
		}

		Random _random = new Random();

		/// <summary>
		/// Generates a new random  piece and assigns it to the next block.
		/// </summary>
		private void GenerateRandomPiece()
		{
			BlockType block = (BlockType)(_random.Next(7));
			NextPiece = new Tetriminos(block);
		}

		private int _currentPieceXPosition;
		private int _currentPieceYPosition;
		private int _currentPieceNextXPosition;
		private int _currentPieceNextYPosition;

		enum Direction { X, Y };
		enum Change {Increment, Decrement};

		private void ChangePosition(Direction direction, Change change)
		{
			if(direction == Direction.X)
			{
				if(change == Change.Increment)
				{
					if (_boardStartX + _boardWidth - (_currentPieceNextXPosition + CurrentPiece.Width) >= 1)
						_currentPieceNextXPosition+=2;
				}
				else if(change == Change.Decrement)
				{
					if (_currentPieceNextXPosition   > _boardStartX + 2)
						_currentPieceNextXPosition-=2;
				}
			}

			else if(direction == Direction.Y)
			{
				if (change == Change.Increment)
				{
					if (_currentPieceNextYPosition + CurrentPiece.Height + 2  <= _boardDepth + 1  )
						_currentPieceNextYPosition+=2;
				}
			}
		}


		/// <summary>
		/// Handles the game loop.
		/// </summary>
		public void Run()
		{
			GenerateRandomPiece();
			CurrentPiece = NextPiece;
			GenerateRandomPiece();
			DrawCurrentBlock();

			while (true)
			{
   
				 if (Console.KeyAvailable)
				 {
					 HandleKey(Console.ReadKey(true /* do not display*/).Key);
				 }
				 if (DeltaTime())
				 {
					 if (_paused)
					 {
						 string alert = _visible ? "Game Paused" : "";
						 _visible = !_visible;
						 Console.MoveBufferArea(5, 21, 11, 1, 5, 20);
						 Console.SetCursorPosition(5, 20);
						 Console.BackgroundColor = ConsoleColor.DarkGreen;
						 Console.WriteLine(alert);
						 Console.ResetColor();
					 }
					 else
					 {
						 ChangePosition(Direction.Y, Change.Increment);
						 ValidateMove();
					 }
				 }
				 DrawCurrentBlock();
				 MaximumNavigation();
			   
				 DetectGameOver();
			}
		}

		#region Game Helper Functions

		bool[] _game = new bool[720];

		/// <summary>
		/// Determines wether the applied rotation is valid or not.
		/// </summary>
		/// <returns>True if the move is valid otherwise false.</returns>
		private bool ValidateRotation(int[] oldSeq)
		{
			bool success = true;
			if (_currentPieceXPosition != _currentPieceNextXPosition
				|| _currentPieceYPosition != _currentPieceNextYPosition)
			{

				//check if still in grid
				if (!(_boardStartX + _boardWidth - (_currentPieceNextXPosition + CurrentPiece.Width) >= 1
					&& _currentPieceNextXPosition > _boardStartX + 2
					&& _currentPieceNextYPosition + CurrentPiece.Height + 2 <= _boardDepth + 1
                    && _currentPieceNextYPosition > 1))
				{
					success = false;
					_currentPieceNextXPosition = _currentPieceXPosition;
					_currentPieceNextYPosition = _currentPieceYPosition;
					_rotationAngle = (_rotationAngle + 3) % 4;
					CurrentPiece.Rotate(_rotationAngle);

					return success;
				}

			
			 //clear block from current position
			 int j = _currentPieceXPosition - _boardStartX - 1;
			 int i = (_currentPieceYPosition - 1);
			 int start;
			 var arr = oldSeq;
			 for (int k = 0; k < 4; k++)
			 {
				 start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
				 _game[start] = false;
				 _game[start + 1] = false;
				 _game[start + _boardWidth] = false;
				 _game[start + _boardWidth + 1] = false;
			 }
	   
			 arr = CurrentPiece.BlockSequence;
			 j = _currentPieceNextXPosition - _boardStartX - 1;
			 i = (_currentPieceNextYPosition - 1);
			 start = 0;
			 for (int k = 0; k < 4; k++)
			 {
				 start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
				 if (_game[start])
				 {
					 success = false;
					 break;
				 }
			 }
	   
	   
			 if (success)
			 {
				 for (int k = 0; k < 4; k++)
				 {
					 start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
					 _game[start] = true;
					 _game[start + 1] = true;
					 _game[start + _boardWidth] = true;
					 _game[start + _boardWidth + 1] = true;
				 }
			 }
			 else
			 {
				 _currentPieceNextXPosition = _currentPieceXPosition;
				 _currentPieceNextYPosition = _currentPieceYPosition;
				 _rotationAngle = (_rotationAngle + 3) % 4;
				 CurrentPiece.Rotate(_rotationAngle);
			 }
		 }

			
			return success;
		}

		/// <summary>
		/// Determines wether the applied move is valid or not.
		/// </summary>
		/// <returns>True if the move is valid otherwise false.</returns>
		private bool ValidateMove()
		{
			bool success = true;
			if (_currentPieceXPosition != _currentPieceNextXPosition
				|| _currentPieceYPosition != _currentPieceNextYPosition)
			{
				//clear block from current position
				int j = _currentPieceXPosition - _boardStartX - 1;
				int i = (_currentPieceYPosition - 1);
				int start;
				var arr = CurrentPiece.BlockSequence;
				for (int k = 0; k < 4; k++)
				{
					start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
					_game[start] = false;
					_game[start + 1] = false;
					_game[start + _boardWidth] = false;
					_game[start + _boardWidth + 1] = false;
				}

				j = _currentPieceNextXPosition - _boardStartX - 1;
				i = (_currentPieceNextYPosition - 1);
				start = 0;
				for (int k = 0; k < 4; k++)
				{
					start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
					if (_game[start])
					{
						success = false;
						break;
					}	
				}

				if(success)
				{
					for (int k = 0; k < 4; k++)
					{
						start = (i + arr[2 * k + 1]) * _boardWidth + (arr[2 * k] + j);
						_game[start] = true;
						_game[start + 1] = true;
						_game[start + _boardWidth] = true;
						_game[start + _boardWidth - 1] = true;
					}
				}
				else
				{
					_currentPieceNextXPosition = _currentPieceXPosition;
					_currentPieceNextYPosition = _currentPieceYPosition;
				}

			}
			return success;
		}

		/// <summary>
		/// Detects if a gameover is reached.
		/// </summary>
		private void DetectGameOver()
		{

		}

		/// <summary>
		/// Detects lines, removes them and advances the score.
		/// </summary>
		private void DetectLines()
		{
			List<int> lines = new List<int>();
			for(int i = 0; i < _boardDepth; i+=2)
			{
				bool line = true;
				int k = i * _boardWidth;
				for (int j = 0; j < _boardWidth; j+=2)
				{
					line &= _game[k + j];
				}
				if(line)
				{
					lines.Add(i);
					for (int j = 0; j < _boardWidth; j+=2)
					{
						_game[k + j] = false;
						_game[k + j + 1] = false;
						_game[k + _boardWidth + j] = false;
						_game[k + _boardWidth + j - 1] = false;
					}
				}
			}
			foreach (var item in lines)
			{
					Lines++;
					Score += 20;
					Console.MoveBufferArea(_boardStartX + 1, 1, _boardWidth, item - 1, _boardStartX + 1, 4);
			}
		}

		/// <summary>
		/// Detects if the current piece cannot advance anymore. If so it genetrates a new block.
		/// </summary>
		private void MaximumNavigation()
		{

			if (_currentPieceNextYPosition + CurrentPiece.Height == _boardDepth + 1)
			{
				DetectLines();
				SwapBlocks();
				return;
			}

			var arr = CurrentPiece.BlockSequence.ToArray();
			int j = _currentPieceXPosition - _boardStartX;
			int i = (_currentPieceYPosition - 1);
			int start = 0;
			int maxx = -1;
			List<int> list = new List<int>();
			for (int k = 0; k < 4; k++)
			{
				start = (i + arr[2 * k + 1]) * _boardWidth;
				if (start > maxx)
				{
					maxx = start;
				}
			}

			for (int k = 0; k < 4; k++)
			{
				start = (i + arr[2 * k + 1]) * _boardWidth;
				if (start == maxx)
					list.Add(maxx + (arr[2 * k] + j));
			}

			bool condition = false;
			foreach (var item in list)
			{
				condition |= _game[item + 2*_boardWidth];
			}

			System.Diagnostics.Debug.WriteLine(_currentPieceNextYPosition + CurrentPiece.Height == _boardDepth + 1
				|| condition);
			if ( condition) //reached the end.
			{
				DetectLines();
				SwapBlocks();
			}

		}

		/// <summary>
		/// Assign the next block to the current block. Move it to the top of the screen and generate a new next block.
		/// </summary>
		private void SwapBlocks()
		{
			_currentPieceXPosition = _currentPieceNextXPosition = _centerX;
			_currentPieceYPosition = _currentPieceNextYPosition = _centerY;
			CurrentPiece = _nextPiece;
			GenerateRandomPiece();
		}

		#endregion
		
		private int _previous;
		private int _current;

		/// <summary>
		/// Determines if enough time have passed to update the game.
		/// </summary>
		/// <returns></returns>
		private bool DeltaTime()
		{
			bool result = (_current - _previous) > 500
						 || (_previous - _current) > 500;

			_previous = _current;
			_current = DateTime.Now.Millisecond;

			return result;
		}	
	}
}
