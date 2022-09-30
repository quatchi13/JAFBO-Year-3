using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Generation_Tools
{
    public static class BasicBuilderFunctions
    {
        public static void HorizontalPath(Space2D space, Cell pathType, Coord start, int stride)
        {
            int step = -1 + ((stride < 0) ? 0 : 2);

            for (int i = start.x; i != (start.x + stride); i += step)
            {
                space.SetCell(new Coord(i, start.y), pathType);
            }
        }
        public static void VerticalPath(Space2D space, Cell pathType, Coord start, int stride)
        {
            int step = -1 + ((stride < 0) ? 0 : 2);

            for (int i = start.y; i != (start.y + stride); i += step)
            {
                space.SetCell(new Coord(start.x, i), pathType);
            }
        }
        public static int CalculateStride(Coord end, Coord start, bool isXStride)
        {
            int stride = (isXStride) ? (end.x - start.x) : (end.y - start.y);
            stride += (-1 + ((stride < 0) ? 0 : 2));

            return stride;
        }
        public static void Connect3(Space2D space, Coord a, Coord b, Coord c, Cell pathValue)
        {
            int stride1 = CalculateStride(b, a, true);
            int stride2 = CalculateStride(b, a, false);
            int stride3 = CalculateStride(b, c, true);
            int stride4 = CalculateStride(b, c, false);

            if(RNG.GenRand(0, 2) == 1)
            {
                HorizontalPath(space, pathValue, a, stride1);
                VerticalPath(space, pathValue, new Coord(b.x, a.y), stride2);
            }
            else
            {
                VerticalPath(space, pathValue, a, stride2);
                HorizontalPath(space, pathValue, new Coord(a.x, b.y), stride1);
            }

            if(RNG.GenRand(0, 2) == 1)
            {
                HorizontalPath(space, pathValue, c, stride3);
                VerticalPath(space, pathValue, new Coord(b.x, c.y), stride4);
            }
            else
            {
                VerticalPath(space, pathValue, c, stride4);
                HorizontalPath(space, pathValue, new Coord(c.x, b.y), stride3);
            }
        }
        public static void Branch(Space2D space, Coord start, Coord direction, Cell branchVal)
        {
            Coord next;
            int stride;

            if (!IsEdge(space, start))
            {
                next = FindNext(space, branchVal, start, direction);
                if (next.x > -1)
                {
                    stride = CalculateStride(next, start, ((direction.x == 0) ? false : true));
                    if (direction.x != 0)
                    {
                        HorizontalPath(space, branchVal, start, stride);
                    }
                    else
                    {
                        VerticalPath(space, branchVal, start, stride);
                    }
                }
            }
        }
        public static void Cluster(Space2D space, Cell clusterType)
        {
            int min, range;

            if(space.area() < 15)
            {
                min = 3;
                range = 2;
            }
            else
            {
                min = (int)space.area() / 3;
                range = (int)space.area() / 5;
            }

            for(int i = 0; i < RNG.GenRand(min, range); i++)
            {
                bool placed = false;
                while (!placed)
                {
                    Coord cur = RNG.GenRandCoord(space, true);
                    if (space.GetCell(cur) != clusterType.value)
                    {
                        space.SetCell(cur, clusterType);
                        placed = true;
                    }
                }
            }

        }
        public static Coord FindNext(Space2D space, Cell toFind, Coord pos, Coord direction)
        {
            bool found = false;
            Coord next = new Coord();

            while (!found)
            {
                pos.x += direction.x;
                pos.y += direction.y;

                if (space.GetCell(pos) == toFind.value)
                {
                    next = pos;
                    found = true;
                }
                else if (IsEdge(space, pos))
                {
                    next = new Coord(-1, -1);
                    found = true;
                }
            }

            return next;
        }
        public static bool IsEdge(Space2D space, Coord location)
        {
            if(location.x > 0 && location.y > 0)
            {
                if (location.x < space.width - 1 && location.y < space.height - 1)
                {
                    return false;
                }
            }
            return true;
        }
        public static float PercentageOf(Space2D space, Cell tileType)
        {
            int tCount = 0;
            for(int i = 0; i < space.height; i++)
            {
                for(int j = 0; j < space.width; j++)
                {
                    if (space.GetCell(new Coord(j, i)) == tileType.value)
                    {
                        tCount++;
                    }
                }
            }

            return ((float)tCount / space.area());
        }
        public static void Flood(Space2D space, Cell toReplace, Cell newValue, int xStart = 0, int yStart = 0, int xEnd = 0, int yEnd = 0)
        {
            xEnd = (xEnd == 0) ? space.width : xEnd;
            yEnd = (yEnd == 0) ? space.height : yEnd;
            for (int i = yStart; i < yEnd; i++)
            {
                for (int j = xStart; j < xEnd; j++)
                {
                    if (space.GetCell(new Coord(j, i)) == toReplace.value)
                    {
                        space.SetCell(new Coord(j, i), newValue);
                    }
                }
            }
        }
        public static void CopySpaceAToB(Space2D a, Space2D b, List<Cell> typesToCopy)
        {
            bool copyAll = false;
            bool single = false;
            Cell singleCell = new Cell(0);

            if(typesToCopy.Count == 0)
            {
                copyAll = true;
            }
            else if(typesToCopy.Count == 1)
            {
                single = true;
                singleCell = typesToCopy[0];
            }

            for (int i = 0; i < a.height; i++)
            {
                for (int j = 0; j < a.width; j++)
                {
                    if (copyAll || (single && (a.GetCell(new Coord(j, i)) == singleCell.value)))
                    {
                        b.SetCell(new Coord(a.worldOrigin.x + j, a.worldOrigin.y + i), new Cell(a.GetCell(new Coord(j, i))));
                    }
                    else
                    {
                        for (int k = 0; k < typesToCopy.Count; k++)
                        {
                            if (a.GetCell(new Coord (j, i)) == typesToCopy[k].value)
                            {
                                b.SetCell(new Coord (a.worldOrigin.x + j, a.worldOrigin.y + i), new Cell(a.GetCell(new Coord(j, i))));
                                k = typesToCopy.Count - 1;
                            }
                        }
                    }
                }
            }
        }
        public static Coord CheckAdjacentCells(Space2D space, Coord start, bool mustMatchCType = false, Cell cType = null)
        {
            Coord surrounding = new Coord();
            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if ((start.y - 1) > -1)
            {

                if ((mustMatchCType && new Cell(space.GetCell(new Coord(start.x, start.y - 1))) == cType) || (space.GetCell(new Coord(start.x, start.y - 1)) > 0 && !mustMatchCType))
                {
                    up = true;
                }
            }

            if ((start.y + 1) < space.height)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x, start.y + 1)) == cType.value) || (space.GetCell(new Coord(start.x, start.y + 1)) > 0 && !mustMatchCType))
                {
                    down = true;
                }
            }

            if ((start.x - 1) > -1)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x - 1, start.y)) == cType.value) || (space.GetCell(new Coord(start.x - 1, start.y)) > 0 && !mustMatchCType))
                {
                    left = true;
                }
            }

            if ((start.x + 1) < space.width)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x + 1, start.y)) == cType.value) || (space.GetCell(new Coord(start.x + 1, start.y)) > 0 && !mustMatchCType))
                {
                    right = true;
                }
            }

            if (!up && !down)
            {
                surrounding.y = 2;
            }
            else
            {
                if (down) surrounding.y = 1; else surrounding.y = 0;
                if (up) surrounding.y -= 1;
            }

            if (!left && !right)
            {
                surrounding.x = 2;
            }
            else
            {
                if (right) surrounding.y = 1; else surrounding.y = 0;
                if (left) surrounding.y -= 1;
            }

            return surrounding;
        }
        public static Coord CheckDiagonalCells(Space2D space, Coord start, bool mustMatchCType = false, Cell cType = null)
        {
            Coord surrounding = new Coord();
            bool lUp = false;
            bool lDown = false;
            bool rUp = false;
            bool rDown = false;

            if ((start.y - 1) > -1 && (start.x - 1) > -1)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x - 1, start.y - 1)) == cType.value) || (space.GetCell(new Coord(start.x - 1, start.y - 1)) > 0 && !mustMatchCType))
                {
                    lUp = true;
                }
            }

            if ((start.y + 1) < space.height && (start.x - 1) > -1)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x - 1, start.y + 1)) == cType.value || (space.GetCell(new Coord(start.x - 1, start.y + 1)) > 0 && !mustMatchCType)))
                {
                    lDown = true;
                }
            }

            if ((start.x + 1) < space.width && (start.y - 1) > -1)
            {

                if ((mustMatchCType && space.GetCell(new Coord(start.x + 1, start.y - 1)) == cType.value || (space.GetCell(new Coord(start.x - 1, start.y)) > 0 && !mustMatchCType)))
                {
                    rUp = true;
                }
            }

            if ((start.x + 1) < space.width && (start.y + 1) < space.height)
            {

                if ((mustMatchCType && space.GetCell(new Coord (start.x + 1, start.y + 1)) == cType.value || (space.GetCell(new Coord(start.x + 1, start.y)) > 0 && !mustMatchCType)))
                {
                    rDown = true;
                }
            }


            if (!lUp && !lDown)
            {
                surrounding.y = 2;
            }
            else
            {
                if (lDown) surrounding.y = 1; else surrounding.y = 0;
                if (lUp) surrounding.y -= 1;
            }

            if (!rUp && !rDown)
            {
                surrounding.x = 2;
            }
            else
            {
                if (rDown) surrounding.y = 1; else surrounding.y = 0;
                if (rUp) surrounding.y -= 1;
            }

            return surrounding;
        }
    }
}
