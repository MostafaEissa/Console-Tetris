using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTetris
{
    class Program
    {
        static void Main(string[] args)
        {
            var gb = new GameBoard();
            gb.Initialize();
            gb.Run();
        }
    }
}
