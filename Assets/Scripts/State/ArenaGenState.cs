using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ArenaGenerators;
using JAFprocedural;

namespace StatePattern {

    public interface ArenaGenState
    {
        void Generate(Space2D s);
        void Display(ArenaData a, FactoryChild f, Transform t);

        
    }

    public class WesternState : ArenaGenState
    {
        public void Generate(Space2D westernArena)
        {
            ArenaGenerators.Arena_Western.MakeWesternArena(westernArena);
        }

        public void Display(ArenaData westernArena, FactoryChild factory, Transform boardOrigin)
        {
            for(int y = 0; y < westernArena.terrainValues.height; y++)
            {
                for(int x = 0; x < westernArena.terrainValues.width; x++)
                {
                    int thisBitch = westernArena.terrainValues.GetCell(new Coord(x, y));
                    GameObject currentTile;

                    currentTile = factory.tiles[thisBitch + 1].Create(factory.tileGOs[thisBitch], new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                    currentTile.transform.SetParent(boardOrigin);

                    if(thisBitch <= 0)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.BOUNDS));
                    }else if(thisBitch == 1)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.GROUND, 0, true, false, 0f));
                    }else if(thisBitch < 8)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.RAIL, thisBitch - 2, true, true, 0f));
                    }else if(thisBitch == 8)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.MINE));
                    }else if(thisBitch == 9)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicDestructableTile(ArenaTileType.TREE));
                    }else if(thisBitch == 10)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicDestructableTile(ArenaTileType.CACTUS));
                    }else if(thisBitch == 11)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicDestructableTile(ArenaTileType.BARREL));
                    }else if(thisBitch < 15)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.CLIFF_BACK, thisBitch - 12, true, false, 0.5f));
                    }else if(thisBitch < 21)
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile(ArenaTileType.CLIFF_FRONT, thisBitch - 15, true, false, 1f));
                    }
                    else
                    {
                        currentTile.GetComponent<ArenaTileProperties>().properties = (new BasicIndestructableTile());
                    }
                }
            }
        }
    }

    public class DefaultState : ArenaGenState {
        public void Generate(Space2D defaultArena)
        {
            Arena_Basic.ArenaPrototype(defaultArena);
        }

        public void Display(ArenaData defaultArena, FactoryChild factory, Transform boardOrigin)
        {
            for (int y = 0; y < defaultArena.terrainValues.height; y++)
            {
                for (int x = 0; x < defaultArena.terrainValues.width; x++)
                {
                    GameObject currentTile;

                    switch (defaultArena.terrainValues.GetCell(new Coord(x, y)))
                    {
                        case 0:
                            currentTile = factory.GetTile("boundaries").Create(factory.boundaries, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 1:
                            currentTile = factory.GetTile("ground").Create(factory.ground, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 2:
                            currentTile = factory.GetTile("water").Create(factory.water, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 3:
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 4:
                            currentTile = factory.GetTile("tree").Create(factory.tree, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        default:
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                    }
                }
            }
        }
    }



    public class ArenaGenerator : ArenaGenState {
        public ArenaGenState agState { get; set; }

        public ArenaGenerator()
        {
            agState = new WesternState();
        }

        public void Generate(Space2D s)
        {
            agState.Generate(s);
        }

        public void Display(ArenaData a, FactoryChild f, Transform t)
        {
            agState.Display(a, f, t);
        }
    }

}



