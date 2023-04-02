using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JAFprocedural;

namespace ArenaGenerators { 
    public static class Arena_Western{
    
        public static void MakeWesternArena(Space2D arena)
        {
            //fill with dirt/sand/whatever idk 
            BasicBuilderFunctions.Flood(arena, new Cell(0), new Cell(1), 1, 1, arena.width - 1, arena.height - 1);
            //self-explanatory
            MakeCliff(arena);
            //adds rails and possibly gold mines
            MakeRails(arena);
            //old trees
            Arena_Basic.MakeSpacedOut(arena, new Cell(1), new Cell(9), 1, 8);
            //cacti
            Arena_Basic.MakeSpacedOut(arena, new Cell(1), new Cell(10), 3, 10);
            //:stare:
            MakeBarrelClusters(arena, new Cell(1), new Cell(11));

        }

        public static void MakeCliff(Space2D arena)
        {
            Space2D cliffFormation = new Space2D(6, 2);
            BasicBuilderFunctions.Flood(cliffFormation, new Cell(), new Cell(1));
            List<Coord> placedCliffs = new List<Coord> { };
            int cCount = 0;
            Coord cliffStart = new Coord();

            cliffFormation.SetCell(new Coord(0, 0), new Cell(12));
            cliffFormation.SetCell(new Coord(5, 0), new Cell(14));
            for (int i = 1; i < 5; i++)
            {
                cliffFormation.SetCell(new Coord(i, 0), new Cell(13));
            }
            for(int i = 0; i < 6; i++)
            {
                cliffFormation.SetCell(new Coord(i, 1), new Cell(i + 15));
            }

            int randIt = RNG.GenRand(4, 9);
            UnityEngine.Debug.Log(randIt);
            for (int i = 0; i < randIt; i++)
            {
                //bool placed = false;
                //while (!placed)
                //{
                cliffStart.x = RNG.GenRand(2, arena.width - 9);
                cliffStart.y = RNG.GenRand(2, arena.height - 6);
                UnityEngine.Debug.Log(cliffStart.x + ", " + cliffStart.y);
                if (/*cCount == 0*/placedCliffs.Count == 0)
                {
                    cliffFormation.worldOrigin = cliffStart;
                    BasicBuilderFunctions.CopySpaceAToB(cliffFormation, arena, new List<Cell> { });
                    //placed = true;
                    placedCliffs.Add(new Coord(cliffStart.x, cliffStart.y));
                    cCount++;
                }
                else
                {
                    
                    for (int j = 0; j < placedCliffs.Count; j++)
                    {
                        int xDist = BasicBuilderFunctions.CalculateStride(cliffStart, placedCliffs[j], true);
                       
                        if (xDist < 0)
                        {
                            xDist *= -1;
                        }

                        if (xDist < 8)
                        {
                            int yDist = BasicBuilderFunctions.CalculateStride(cliffStart, placedCliffs[j], false);
                            if (yDist < 0)
                            {
                                yDist *= -1;
                            }

                            if (yDist < 4)
                            {
                                j = placedCliffs.Count;
                            }
                        }

                        if (j == (placedCliffs.Count - 1))
                        {
                            cliffFormation.worldOrigin = cliffStart;
                            BasicBuilderFunctions.CopySpaceAToB(cliffFormation, arena, new List<Cell> { });
                            //placed = true;
                            placedCliffs.Add(new Coord(cliffStart.x, cliffStart.y));
                        }
                    }
                }
                    
            }
        }

        public static void MakeRails(Space2D arena)
        {
            int goldMines = 0;
            int lineCount = RNG.GenRand(1, 3);
            Coord start;
            List<Coord> vert    = new List<Coord>{};
            List<Coord> lDown   = new List<Coord>{};
            List<Coord> vRight  = new List<Coord>{};
            List<Coord> vLeft   = new List<Coord>{};
            List<Coord> rDown   = new List<Coord>{};
            List <Coord> gMines = new List<Coord>{};
        
        
            for(int rails = 0; rails < lineCount; rails++)
            {
                //bool placed = false;
               // while (!placed)
                //{
                    start = new Coord(1, RNG.GenRand(1, 28));
                    if(arena.GetCell(start) != 2)
                    {
                        int y = start.y;
                        int offset = 0;
                        arena.SetCell(start, new Cell(2));

                        for(int i = 2; (i > 0 && i < 29); i++)
                        {
                            //if you are about to collide with a rail, bail and place a gold mine
                            if(arena.GetCell(new Coord(i, y + offset)) == 2)
                            {
                                arena.SetCell(new Coord(i - 1, y + offset), new Cell(8));
                                gMines.Add(new Coord(i - 1, y + offset));
                                //placed = true;
                                goldMines++;
                                i = -1;
                            }
                            //if you're about to hit a cliff, try to move around it
                            else if(arena.GetCell(new Coord(i, y + offset)) >= 12)
                            {
                                if(offset == 0)
                                {
                                    lDown.Add(new Coord(i - 1, y));
                                }
                                else
                                {
                                    if(vert.Count > 0)
                                    {
                                    //lDown.Add(vert.Last());
                                    //vert.RemoveAt(vert.Count - 1);
                                    vert.Add(new Coord(i - 1, y + offset));
                                    }
                                }

                                //move down one row if able (only thing that would be in the way is a rail, cliffs cannot spawn close enough and we don't care about anything else
                                if(arena.GetCell(new Coord(i - 1, y + (offset + 1))) != 2)
                                {
                                    i--;
                                    offset++;
                                    arena.SetCell(new Coord(i, y + offset), new Cell(2));
                                    vert.Add(new Coord(i, y + offset));
                                }
                                //otherwise, it's gold mine time!
                                else
                                {
                                    arena.SetCell(new Coord(i - 1, y + offset), new Cell(8));
                                    gMines.Add(new Coord(i - 1, y + offset));
                                    //placed = true;
                                    goldMines++;
                                    i = -1;
                                }
                            }
                            //otherwise, you can advance forward
                            else
                            {
                                if(offset != 0)
                                {
                                    if(vert.Count > 0)
                                    {
                                        //if we just moved upward and are now moving forward, change last tile to a bend
                                        if(vert.Last().x == (i - 1) && vert.Last().y == (y + offset))
                                        {
                                            vRight.Add(vert.Last());
                                            vert.RemoveAt(vert.Count - 1);
                                        }
                                    }
                                
                                    arena.SetCell(new Coord(i, y + offset), new Cell(2));

                                    int rounds = 0;
                                    int startingJ = offset - 1;
                                    for(int j = startingJ; j >= 0; j--)
                                    {
                                        if (arena.GetCell(new Coord(i, y + j)) < 12 && arena.GetCell(new Coord(i, y + j)) != 2)
                                        {
                                            if(j == startingJ)
                                            {
                                                vLeft.Add(new Coord(i, y + offset));
                                            }

                                            if(j == 0)
                                            {
                                                rDown.Add(new Coord(i, y));
                                            }
                                            else
                                            {
                                                vert.Add(new Coord(i, y + j));
                                            }

                                            arena.SetCell(new Coord(i, y + j), new Cell(2));
                                            offset--;
                                            rounds++;
                                        }
                                        else
                                        {
                                            if(rounds > 0)
                                            {
                                                offset = j;
                                            }
                                            j = 0;
                                        }
                                    }


                                }
                                else
                                {
                                    arena.SetCell(new Coord(i, y), new Cell(2));
                                }

                                //if (i == 28) placed = true;
                            }
                        }

                    }
                //}
            }

            BasicBuilderFunctions.AddPointsFromList(arena, vert, new Cell(3));
            BasicBuilderFunctions.AddPointsFromList(arena, lDown, new Cell(4));
            BasicBuilderFunctions.AddPointsFromList(arena, vRight, new Cell(5));
            BasicBuilderFunctions.AddPointsFromList(arena, vLeft, new Cell(6));
            BasicBuilderFunctions.AddPointsFromList(arena, rDown, new Cell(7));
            BasicBuilderFunctions.AddPointsFromList(arena, gMines, new Cell(8));

            //place additional gold mines, if any were generated 
            if(goldMines > 0)
            {
                for(int i = 0; i < (RNG.GenRand(1, 4 - (goldMines))); i++)
                {
                    bool mPlaced = false;
                    while (!mPlaced)
                    {
                        Coord rCor = RNG.GenRandCoord(arena);
                        if(arena.GetCell(rCor) > 8 || arena.GetCell(rCor) == 1)
                        {
                            arena.SetCell(rCor, new Cell(8));
                            mPlaced = true;
                        }
                    }
                }
            }
        }

        public static void MakeBarrelClusters(Space2D arena, Cell validSpace, Cell cellToPlace)
        {
            int keepGoing = 10;

            int a = RNG.GenRand(1, 4);
            int b = a + RNG.GenRand(1, 4);
            Space2D copyable = new Space2D(3, 3);
            BasicBuilderFunctions.Flood(copyable, new Cell(0), new Cell(1));

            for (int i = 0; i < b; i++)
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
                                BasicBuilderFunctions.Flood(copyable, new Cell(11), new Cell(1));
                                for (int j = 0; j < RNG.GenRand(1, 3); j++)
                                {
                                    Coord randCoord = RNG.GenRandCoord(copyable, true);
                                    if (copyable.GetCell(randCoord) == 1)
                                    {
                                        copyable.SetCell(randCoord, cellToPlace);
                                        if (j > 0) i++;
                                    }

                                }
                                copyable.worldOrigin = new Coord(pos.x - 1, pos.y - 1);
                                BasicBuilderFunctions.CopySpaceAToB(copyable, arena, new List<Cell>{ });
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



