using SeaBattleInterfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SeaBattle
{
	public static class CompetitiveModeExecutor
	{
		public const string BOTS_PATH = "Bots";

		public static void Run()
		{
			Console.WriteLine();

			var assemblyInfos = Directory.GetFiles(BOTS_PATH)
				.Select(s => new FileInfo(s))
				.Where(f => f.Extension == ".dll")
				.ToArray();

			if (assemblyInfos.Length != 2)
			{
				Console.WriteLine("Count of assemblies is not equals 2");
				return;
			}

			var bots = assemblyInfos.Select(f => f.FullName)
				.Select(GetBotFromAssembly)
				.ToArray();

			var bot1 = bots[0];
			var bot2 = bots[1];

			var gameSession = new GameSession(bot1, bot2, Program.FIELD_SIZE, Program.DefaultShips);
			gameSession.Start();
		}

		private static IBot GetBotFromAssembly(string assemblyPath)
		{
			var asm = Assembly.LoadFrom(assemblyPath);

			var botClass = asm.GetTypes().Where(t => t.IsClass).FirstOrDefault(IsBotClass);
			if (botClass == null)
				throw new Exception($"Cannot find bot class in asssembly - {assemblyPath}");

			var assemblyFileInfo = new FileInfo(assemblyPath);
			var botInstance = (IBot)Activator.CreateInstance(botClass);
			var fileInfo = new FileInfo(assemblyPath);
			var botName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
			botInstance.Name = botName;

			return botInstance;
		}

		private static bool IsBotClass(Type type)
			=> type.GetInterfaces().Any(t => t == typeof(IBot));
	}
}