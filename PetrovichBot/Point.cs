using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrovichBot
{
	public readonly struct Point
	{
		public readonly int X;
		public readonly int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
