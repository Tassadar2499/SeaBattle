using System;
using System.Collections.Generic;

namespace SeaBattle
{
	class Program
	{
		const int FIELD_SIZE = 10;
		public static readonly IEnumerable<(int length, int count)> DefaultShips = new []
		{
			(4, 1),
			(3, 2),
			(2, 3),
			(1, 4),
		};

		static void Main(string[] args)
		{
			Console.WriteLine("SeaBattle v0.01");

			IBot bot1 = null;
			IBot bot2 = null;

			var bot1ShipsPosition = bot1.CreateStartPos(FIELD_SIZE, DefaultShips);
			var bot2ShipsPosition = bot2.CreateStartPos(FIELD_SIZE, DefaultShips);

			while (true)
			{

			}
		}
	}
}
