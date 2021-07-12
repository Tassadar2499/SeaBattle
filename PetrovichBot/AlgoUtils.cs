using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public static class AlgoUtils
	{
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
	}
}
