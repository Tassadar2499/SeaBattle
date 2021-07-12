using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public static class Algo
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

		public static IEnumerable<int[]> GroupNumbers(int[] arr)
		{
			var length = arr.Length;
			if (length < 2)
				yield break;

			var groupQueue = new Queue<int>();
			for (int i = 0; i < length; i++)
			{
				groupQueue.Enqueue(arr[i]);
				if (groupQueue.Count == 3)
				{
					var groupArr = groupQueue.ToArray();
					if (groupArr[0] + 1 == groupArr[1] && groupArr[1] + 1 == groupArr[2])
					{
						yield return groupArr;
						groupQueue.Clear();
					}
					else
					{
						groupQueue.Dequeue();
					}
				}
			}
		}

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
