using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle
{
	public interface IBot
	{
		bool[,] CreateStartPos(int fieldSize, IEnumerable<(int length, int count)> ships);

		(int x, int y) MakeStep(Cell[,] field);
	}
}
