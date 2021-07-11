using System;
using System.Collections.Generic;

namespace SeaBattle
{
	public class Program
	{
		public const int FIELD_SIZE = 10;

		public static readonly IEnumerable<(int length, int count)> DefaultShips = new[]
		{
			(4, 1),
			(3, 2),
			(2, 3),
			(1, 4),
		};

		private static void Main(string[] args)
		{
			Console.WriteLine("SeaBattle v0.01");
			Console.WriteLine("Is competitive mode y/n?");

			var isCompetitiveModeKeyInfo = Console.ReadKey();
			if (isCompetitiveModeKeyInfo.Key == ConsoleKey.Y)
				CompetitiveModeExecutor.Run();
			else
				RunAutoMode();
		}

		private static void RunAutoMode()
		{
			var bot1 = new DefaultBot("Bot 1");
			var bot2 = new DefaultBot("Bot 2");

			var gameSession = new GameSession(bot1, bot2, FIELD_SIZE, DefaultShips);
			gameSession.Start();
		}
	}
}