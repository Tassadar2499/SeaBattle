using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public static class VerticalRectangleLogic
	{
		public static IEnumerable<int> GetFreeVerticals(Cell[,] field, int size)
		{
			var length = field.Length;
			for (int i = 0; i < length; i++)
			{
				var accum = 0;
				for (int j = 0; j < length; j++)
				{
					var cell = field[j, i];
					if (cell == Cell.Empty || cell == Cell.Miss)
						accum++;
					else
						accum = 0;

					if (accum == size + 2)
					{
						yield return i;
						break;
					}
				}
			}
		}

		public static Point GetPointByVerticalGroup(Cell[,] field, int[] verticalGroup, int rectangleSize)
		{
			var bottomIndex = GetBottomIndexByVerticalGroup(field, verticalGroup, rectangleSize);
			var topIndex = rectangleSize - bottomIndex;
			var middleIndex = (bottomIndex - topIndex) / 2;

			if (rectangleSize % 2 == 0)
			{
				if (field[middleIndex, 1] == Cell.Empty)
					return new(middleIndex, 1);

				if (field[middleIndex - 1, 1] == Cell.Empty)
					return new(middleIndex - 1, 1);
			}
			else
			{
				if (field[middleIndex + 1, 1] == Cell.Empty)
					return new(middleIndex + 1, 1);
			}

			return new(-1, -1);
		}

		private static int GetBottomIndexByVerticalGroup(Cell[,] field, int[] verticalGroup, int rectangleSize)
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

				if (checkCount == rectangleSize)
					return i;
			}

			return -1;
		}
	}
}
