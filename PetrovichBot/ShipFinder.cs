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
		private readonly int _rectangleSize;
		public ShipFinder(int size)
		{
			_size = size;
			_rectangleSize = size + 2;
		}

		public Cell HitShipByBoundaries(Cell[,] field)
		{
			throw new NotImplementedException();
		}

		private Point FindCellByVertical(Cell[,] field)
		{
			var verticals = Algo.GetFreeVerticals(field, _size).ToArray();
			var filteredVerticals = Algo.GroupNumbers(verticals).ToArray();

			foreach (var verticalGroup in filteredVerticals)
			{
				var point = GetPointByVerticalGroup(field, verticalGroup);
				if (point.X != -1 && point.Y != -1)
					return point;
			}

			return new(-1, -1);
		}

		private Point GetPointByVerticalGroup(Cell[,] field, int[] verticalGroup)
		{
			var bottomIndex = GetBottomIndexByVerticalGroup(field, verticalGroup);
			var topIndex = _rectangleSize - bottomIndex;
			var middleIndex = (bottomIndex - topIndex) / 2;

			if (_rectangleSize % 2 == 0)
			{
				if (field[middleIndex, 1] == Cell.Empty)
					return new (middleIndex, 1);

				if (field[middleIndex - 1, 1] == Cell.Empty)
					return new (middleIndex - 1, 1);
			}
			else
			{ 
				if (field[middleIndex + 1, 1] == Cell.Empty)
					return new (middleIndex + 1, 1);
			}

			return new (-1, -1);
		}

		private int GetBottomIndexByVerticalGroup(Cell[,] field, int[] verticalGroup)
		{
			var checkCount = 0;

			for (int i = 1; i < field.Length - 1; i++)
			{
				var count = 3;
				foreach (var verticalIndex in verticalGroup)
				{
					var cell = field[i, verticalIndex];
					if (cell == Cell.Hit || cell == Cell.Wound)
						break;

					count--;
				}

				if (count == 0)
					checkCount++;

				if (checkCount == _rectangleSize)
					return i;
			}

			return -1;
		}
	}
}
