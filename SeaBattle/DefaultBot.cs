using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle
{
	public class DefaultBot : IBot
	{
		public string Name { get; set; }

		public DefaultBot(string name = "DefaultBot")
		{
			Name = name;
		}

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