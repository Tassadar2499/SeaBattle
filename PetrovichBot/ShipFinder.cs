using SeaBattleInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public class ShipFinder
	{
		private readonly int _size;
		private readonly int _rectangleSize;
		public ShipFinder(int size)
		{
			_size = size;
			_rectangleSize = size + 2;
		}

		public Point HitShipByBoundaries(Cell[,] field)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Ищем куда выстрелить по площади - 9x9, пропуская границы
		/// </summary>
		public Point FindPointByArea(Cell[,] field)
		{
			var point = FindCellByVertical(field);
			if (point.X == -1 && point.Y == -1)
				point = FindCellByHorizontal(field);

			return point;
		}

		private Point FindCellByVertical(Cell[,] field)
		{
			var verticals = VerticalRectangleLogic.GetFreeVerticals(field, _size).ToArray();
			var filteredVerticals = AlgoUtils.GroupNumbers(verticals).ToArray();

			foreach (var verticalGroup in filteredVerticals)
			{
				var point = VerticalRectangleLogic.GetPointByVerticalGroup(field, verticalGroup, _rectangleSize);
				if (point.X != -1 && point.Y != -1)
					return point;
			}

			return new(-1, -1);
		}

		private Point FindCellByHorizontal(Cell[,] field)
		{
			throw new NotImplementedException();
		}
	}
}
