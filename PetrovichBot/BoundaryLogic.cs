using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public static class BoundaryLogic
	{
		public static bool IsShipCanBeAtLine(Cell[] line, int size)
		{
			var accum = 0;
			foreach (var cell in line)
			{
				if (cell == Cell.Empty)
					accum++;
				else
					accum = 0;

				if (accum == size)
					return true;
			}

			return false;
		}

		public static int GetIndexOfShootByBoundary(Cell[] line, int size)
		{
			var accum = new List<int>();
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == Cell.Empty)
					accum.Add(i);
				else
					accum = new List<int>();

				if (accum.Count == size)
				{
					var middle = size / 2;
					if (size % 2 == 0)
						return middle;
					else
						return middle + 1;
				}
			}

			return -1;
		}
	}
}
