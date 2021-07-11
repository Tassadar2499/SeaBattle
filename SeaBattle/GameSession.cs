using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaBattle
{
	public class GameSession
	{
		private readonly int _fieldSize;
		private readonly IEnumerable<(int length, int count)> _ships;
		private readonly IBot _bot1, _bot2;

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
				throw new ArgumentException(null, nameof(array));

			var newArray = new T[size, size];

			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++)
					newArray[i, j] = array[i, j];

			return newArray;
		}

		private bool CheckWin(Cell[,] botPlayField)
		{
			var count = 0;

			for (var i = 0; i < _fieldSize; i++)
				for (var j = 0; j < _fieldSize; j++)
					if (botPlayField[i, j] == Cell.Hit)
						count++;

			return count >= _ships.Sum(a => a.length * a.count);
		}

		private void InitBot(IBot bot, out Cell[,] botEnemyField, out bool[,] botShipsPosition)
		{
			botEnemyField = new Cell[_fieldSize, _fieldSize];
			botShipsPosition = CopyArray(bot.CreateStartPos(_fieldSize, _ships), _fieldSize);
			Console.WriteLine($"{bot.Name} field:");
			PrintField(botShipsPosition);
			Console.WriteLine();
		}

		private bool InvokeBotTurn(IBot bot, Cell[,] botPlayField, bool[,] enemyBotShipsPosition)
		{
			Console.WriteLine("\n***************************");
			Console.WriteLine($"{bot.Name} step: ");

			var (x, y) = bot.MakeStep(CopyArray(botPlayField, _fieldSize));

			Console.WriteLine($"x = {x}; y = {y}");

			if (x < 0 || y < 0 || x >= _fieldSize | y >= _fieldSize)
				throw new Exception("ARE YOU OHUELY TAM?");

			if (botPlayField[x, y] != Cell.Empty)
				return false;

			botPlayField[x, y] = enemyBotShipsPosition[x, y]
				? Cell.Hit
				: Cell.Miss;

			return enemyBotShipsPosition[x, y];
		}

		private bool InvokeBotRound(IBot bot1, Cell[,] bot1PlayField, bool[,] bot2ShipsPosition)
		{
			while (true)
			{
				var isHit = InvokeBotTurn(bot1, bot1PlayField, bot2ShipsPosition);
				PrintField(bot1PlayField);

				Console.WriteLine(isHit ? "Hit!" : "Miss!");

				if (!isHit)
					return false;

				if (CheckWin(bot1PlayField))
					return true;
			}
		}

		public void Start()
		{
			InitBot(_bot1, out Cell[,] bot1PlayField, out bool[,] bot1ShipsPosition);
			InitBot(_bot2, out Cell[,] bot2PlayField, out bool[,] bot2ShipsPosition);

			while (true)
			{
				if (InvokeBotRound(_bot1, bot1PlayField, bot2ShipsPosition))
					Console.WriteLine($"{_bot1.Name} WIN!");

				if (InvokeBotRound(_bot2, bot2PlayField, bot1ShipsPosition))
					Console.WriteLine($"{_bot2.Name} WIN!");
			}
		}

		public GameSession(IBot bot1, IBot bot2, int fieldSize, IEnumerable<(int length, int count)> ships)
		{
			_bot1 = bot1;
			_bot2 = bot2;
			_fieldSize = fieldSize;
			_ships = ships;
		}
	}
}