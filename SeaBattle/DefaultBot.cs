using System;
using System.Linq;
using System.Collections.Generic;

namespace SeaBattle
{
	public class DefaultBot : IBot
	{
		public DefaultBot(string name = "DefaultBot")
		{
			_name = name;
		}

		private string _name;
		public string Name => _name;

		public bool[,] CreateStartPos(int fieldSize, IEnumerable<(int length, int count)> ships)
		{
			return new[,]
			{
				{ false, false, false, false, false, false, false, false, false, false },
				{ false, true , false, true , true , true , false, true , true , false },
				{ false, true , false, false, false, false, false, false, false, false },
				{ false, true , false, true , true , true , false, true , true , false },
				{ false, true , false, false, false, false, false, false, false, false },
				{ false, false, false, false, false, false, false, true , true , false },
				{ false, false, false, false, false, false, false, false, false, false },
				{ false, true , false, true , false, false, false, false, false, false },
				{ false, false, false, false, false, false, false, false, false, false },
				{ false, true , false, true , false, false, false, false, false, false },
			};
		}

		public (int x, int y) MakeStep(Cell[,] field)
		{
			Console.WriteLine("Write pos: ");
			var text = Console.ReadLine();
			var pos = text.Split(' ').Select(int.Parse).ToArray();

			return (pos[0], pos[1]);
		}
	}
}
