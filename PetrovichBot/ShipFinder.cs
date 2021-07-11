using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public class ShipFinder
	{
		private readonly int _size;
		public ShipFinder(int size) => _size = size;

		private Cell FindCellByVerticalMaxArea(Cell[,] field)
		{
			var verticals = Algo.GetFreeVerticals(field, _size + 2).ToArray();
			var filteredVerticals = Algo.FilterNumbersBySeqCount(verticals, 3);

			throw new NotImplementedException();
		}
	}
}
