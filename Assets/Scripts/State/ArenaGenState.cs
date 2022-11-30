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
                    GameObject currentTile;

                    switch (westernArena.terrainValues.GetCell(new Coord(x, y)))
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
                            currentTile = factory.GetTile("hRail").Create(factory.hRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 3:
                            currentTile = factory.GetTile("vRail").Create(factory.vRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 4:
                           currentTile = factory.GetTile("swRail").Create(factory.swRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 5:
                           currentTile = factory.GetTile("neRail").Create(factory.neRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 6:
                           currentTile = factory.GetTile("nwRail").Create(factory.nwRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 7:
                            currentTile = factory.GetTile("seRail").Create(factory.seRail, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 8:
                            currentTile = factory.GetTile("goldmine").Create(factory.goldmine, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 9:
                            currentTile = factory.GetTile("tree").Create(factory.tree, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 10:
                            currentTile = factory.GetTile("cactus").Create(factory.cactus, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 11:
                            currentTile = factory.GetTile("barrel").Create(factory.barrel, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 58:
                            currentTile = factory.GetTile("b1Cliff").Create(factory.b1Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 59:
                            currentTile = factory.GetTile("b2Cliff").Create(factory.b2Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 60:
                            currentTile = factory.GetTile("b2Cliff").Create(factory.b2Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 61:
                            currentTile = factory.GetTile("b2Cliff").Create(factory.b2Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 62:
                            currentTile = factory.GetTile("b2Cliff").Create(factory.b2Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 63:
                            currentTile = factory.GetTile("b6Cliff").Create(factory.b6Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 64:
                            currentTile = factory.GetTile("f1Cliff").Create(factory.f1Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 65:
                            currentTile = factory.GetTile("f2Cliff").Create(factory.f2Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 66:
                            currentTile = factory.GetTile("f3Cliff").Create(factory.f3Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 67:
                            currentTile = factory.GetTile("f4Cliff").Create(factory.f4Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 68:
                            currentTile = factory.GetTile("f5Cliff").Create(factory.f5Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case 69:
                            currentTile = factory.GetTile("f6Cliff").Create(factory.f6Cliff, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        default:
                            break;
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
            agState = new DefaultState();
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



