using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAFprocedural
{
    public enum ArenaTileType
    {
        //general
        BOUNDS = 0,
        GROUND = 1,//western ground = 2
        WATER = 3,
        TREE = 4,//western tree = 5
        ROCK = 6,
        RAIL = 7, //straight: 7 bent: 8
        MINE = 9,
        CACTUS = 10,
        BARREL = 11,
        CLIFF_BACK = 12, //lcorn: 12 mid: 13 rcorn: 14
        CLIFF_FRONT = 15 //15-20, left-right normal orientation
    }



    public struct EnvironmentDrops
    {
        public int quantity;
        public EnvDropTypes drop;

        public EnvironmentDrops(int q, EnvDropTypes d)
        {
            quantity = q;
            drop = d;
        }
    }
    public enum EnvDropTypes
    {
        THING_A,
        THING_B
    }


    public class ArenaTile
    {
        public ArenaTileType tileName;
        public bool is_walkable;
        public bool is_minecartMoveable;
        public bool is_destructable;
        public List<EnvironmentDrops> dropOnBreak;

        public int TileVariationNum;
        public List<float> heightToWalk;
        public float elevation;

        public ArenaTile(ArenaTileType att, bool iw, bool imm, bool id, int doc = 0)
        {
            tileName = att;
            is_walkable = iw;
            is_minecartMoveable = imm;
            is_destructable = id;
            dropOnBreak = new List<EnvironmentDrops> { };
        }
        public ArenaTile(ArenaTileType att, bool iw, bool imm, bool id, List<EnvironmentDrops> edl)
        {
            tileName = att;
            is_walkable = iw;
            is_minecartMoveable = imm;
            is_destructable = id;

            dropOnBreak = new List<EnvironmentDrops> { };
            for (int i = 0; i < edl.Count; i++)
            {
                dropOnBreak.Add(edl[i]);
            }
        }
    }
}
