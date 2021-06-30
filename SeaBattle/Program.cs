using System;
using System.Linq;
using System.Collections.Generic;

namespace SeaBattle
{
	public class Program
	{
		const int FIELD_SIZE = 10;
		public static readonly IEnumerable<(int length, int count)> DefaultShips = new[]
		{
			(4, 1),
			(3, 2),
			(2, 3),
			(1, 4),
		};

		private static void PrintField<T>(T[,] field, Func<T, string> action)
		{
			for (int j = 0; j <= field.GetUpperBound(1); j++)
			{
				Console.WriteLine();

				for (int i = 0; i <= field.GetUpperBound(0); i++)
					Console.Write(action(field[i, j]));
			}

			Console.WriteLine();
		}

		public static void PrintField(bool[,] field)
			=> PrintField(field, a => a ? "O " : "~ ");

		public static void PrintField(Cell[,] field)
			=> PrintField(field, a => a switch
				{
					Cell.Empty => "~ ",
					Cell.Miss => "* ",
					Cell.Hit => "+ ",
					_ => "~ ",
				});

		public static T[,] CopyArray<T>(T[,] array, int size)
			where T : struct
		{
			if (array.GetUpperBound(0) + 1 != size || array.GetUpperBound(1) + 1 != size)
				throw new ArgumentException(nameof(array));

			var newArray = new T[size, size];

			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					newArray[i, j] = array[i, j];

			return newArray;
		}

		public static bool CheckWin(Cell[,] botPlayField)
		{
			var count = 0;

			for (var i = 0; i < FIELD_SIZE; i++)
				for (var j = 0; j < FIELD_SIZE; j++)
					if (botPlayField[i, j] == Cell.Hit)
						count++;

			return count >= DefaultShips.Sum(a => a.length * a.count);
		}

		private static void InitBot(DefaultBot bot2, out Cell[,] bot2EnemyField, out bool[,] bot2ShipsPosition)
		{
			bot2EnemyField = new Cell[FIELD_SIZE, FIELD_SIZE];
			bot2ShipsPosition = CopyArray(bot2.CreateStartPos(FIELD_SIZE, DefaultShips), FIELD_SIZE);
			Console.WriteLine($"{bot2.Name} field:");
			PrintField(bot2ShipsPosition);
			Console.WriteLine();
		}

		private static bool InvokeBotRound(IBot bot, Cell[,] botPlayField, bool[,] enemyBotShipsPosition)
		{

			Console.WriteLine("\n***************************");
			Console.WriteLine($"{bot.Name} step: ");

			var (x, y) = bot.MakeStep(CopyArray(botPlayField, FIELD_SIZE));

			Console.WriteLine($"x = {x}; y = {y}");

			if (x < 0 || y < 0 || x >= FIELD_SIZE | y >= FIELD_SIZE)
				throw new Exception("ARE YOU OHUELY TAM?");

			if (botPlayField[x, y] != Cell.Empty)
				return false;

			botPlayField[x, y] = enemyBotShipsPosition[x, y]
				? Cell.Hit
				: Cell.Miss;

			return enemyBotShipsPosition[x, y];
		}

		private static bool PlayRound(DefaultBot bot1, Cell[,] bot1PlayField, bool[,] bot2ShipsPosition)
		{
			while (true)
			{
				var isHit = InvokeBotRound(bot1, bot1PlayField, bot2ShipsPosition);
				PrintField(bot1PlayField);

				Console.WriteLine(isHit ? "Hit!" : "Miss!");

				if (!isHit)
					return false;

				if (CheckWin(bot1PlayField))
					return true;
			}
		}

		static void Main(string[] args)
		{
			Console.WriteLine("SeaBattle v0.01");

			var bot1 = new DefaultBot("Bot 1");
			var bot2 = new DefaultBot("Bot 2");

			InitBot(bot1, out Cell[,] bot1PlayField, out bool[,] bot1ShipsPosition);
			InitBot(bot2, out Cell[,] bot2PlayField, out bool[,] bot2ShipsPosition);

			while (true)
			{
				if (PlayRound(bot1, bot1PlayField, bot2ShipsPosition))
					Console.WriteLine($"{bot1.Name} WIN!");

				if (PlayRound(bot2, bot2PlayField, bot1ShipsPosition))
					Console.WriteLine($"{bot2.Name} WIN!");
			}
		}
	}
}