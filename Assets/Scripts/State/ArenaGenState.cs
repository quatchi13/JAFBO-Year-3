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
                        case (< 8):
                            currentTile = factory.GetTile("water").Create(factory.water, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (8):
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (< 11):
                            currentTile = factory.GetTile("tree").Create(factory.tree, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (11):
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (58):
                            //you will need to repeat this for every individual tile on the cliff, may you rip in peace
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (< 63):
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
                            currentTile.transform.SetParent(boardOrigin);
                            break;
                        case (63):
                            currentTile = factory.GetTile("rock").Create(factory.rock, new Vector3(boardOrigin.position.x + x, 0, boardOrigin.position.z - y), Quaternion.identity);
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



