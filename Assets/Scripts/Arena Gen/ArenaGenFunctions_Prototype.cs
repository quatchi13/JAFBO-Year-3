using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAFprocedural
{
    public static class Arena_Basic { 
        public static void ArenaPrototype(Space2D arena)
        {
            BasicBuilderFunctions.Flood(arena, new Cell(0), new Cell(1), 1, 1, 29, 29);
			MakeRiver(arena);
			MakeClusters(arena);
			MakeSpacedOut(arena, new Cell(1), new Cell(4), 3, 5);
        }

        public static void MakeRiver(Space2D arena)
        {
            List<Coord> riverPoints = new List<Coord> { };
            Coord curPoint = new Coord();

			for (int i = 0; i < 5; i++)
			{
				bool unique = false;

				while (!unique)
				{
					bool yes = true;
					curPoint = (RNG.GenRandCoord(arena));
					for (int j = 0; j < i; j++)
					{
						if (curPoint.x == riverPoints[j].x && curPoint.y == riverPoints[j].y)
						{
							yes = false;
						}
					}
					if (yes)
					{
						unique = yes;
						riverPoints.Add(curPoint);
					}

				}

			}

			BasicBuilderFunctions.Connect3(arena, riverPoints[0], riverPoints[1], riverPoints[2], new Cell(2));
			BasicBuilderFunctions.Connect3(arena, riverPoints[2], riverPoints[3], riverPoints[4], new Cell(2));
		}
		public static void MakeClusters(Space2D arena)
		{
			Cell clusterValue = new Cell(3);

			for (int i = 0; i < (RNG.GenRand(1, 3)); i++)
			{
				Space2D selection = new Space2D(RNG.GenRand(3, 8), RNG.GenRand(3, 8));
				BasicBuilderFunctions.Cluster(selection, clusterValue);
				selection.worldOrigin = RNG.GenRandCoord(new Space2D(arena.width - (selection.width + 1), arena.height - (selection.height + 1)), true);

				BasicBuilderFunctions.CopySpaceAToB(selection, arena, new List<Cell>{ clusterValue});
			}
		}
		public static void MakeSpacedOut(Space2D arena, Cell validSpace, Cell cType, int min, int range)
        {
			int keepGoing = 12;
			for (int i = 0; i < RNG.GenRand(min, range); i++)
			{
				bool valid = false;
				Coord surrounding;

				while (!valid && keepGoing > 0)
				{
					Coord pos = RNG.GenRandCoord(arena);

					if (arena.GetCell(pos) == validSpace.value)
					{
						surrounding = BasicBuilderFunctions.CheckAdjacentCells(arena, pos, true, validSpace);
						if (surrounding.x == 0 && surrounding.y == 0)
						{
							surrounding = BasicBuilderFunctions.CheckDiagonalCells(arena, pos, true, validSpace);
							if (surrounding.x == 0 && surrounding.y == 0)
							{
								arena.SetCell(pos, cType);
								valid = true;
							}
						}
					}

					if (!valid)
					{
						keepGoing--;
					}
				}

			}

		}
	
    }

}