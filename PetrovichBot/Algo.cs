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
					if (field[j, i] == Cell.Empty)
						accum++;
					else
						accum = 0;
				}

				if (accum >= size)
					yield return i;
			}
		}

		//TODO: Убрать все номера кроме тех, что идут подряд count раз
		public static IEnumerable<int> FilterNumbersBySeqCount(int[] arr, int count)
		{
			var seqNumbers = new List<int>()
			{
				arr[0]
			};

			for (int i = 1; i < arr.Length; i++)
			{
				if (arr[i - 1] + 1 == arr[i])
				{
					seqNumbers.Add(arr[i]);
				}
				else
				{
					seqNumbers = new List<int>
					{
						arr[i]
					};
				}

				if (seqNumbers.Count == count)
				{
					foreach (var number in seqNumbers)
						yield return number;

					seqNumbers = new List<int>
					{
						arr[i]
					};
				}
			}
		}
	}
}
