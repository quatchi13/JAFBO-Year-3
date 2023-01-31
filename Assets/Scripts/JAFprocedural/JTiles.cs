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
        GROUND = 1,
        RAIL = 2,
        MINE = 8,
        TREE = 9,//western tree = 4
        CACTUS = 10,
        BARREL = 11, //straight:7 bent: 7
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
        public ArenaTileType tileIDNum;
        public int idVariation;

        public bool is_walkable;
        public bool is_minecartMoveable;
        public bool is_destructable;

        public float elevation;
    }

    public class BasicIndestructableTile: ArenaTile
    {
        public BasicIndestructableTile(ArenaTileType tm = 0, int tvn = 0, bool wk = false, bool mm = false, float h = 0)
        {
            tileIDNum = tm;
            idVariation = tvn;

            is_walkable = wk;
            is_minecartMoveable = mm;
            is_destructable = false;

            elevation = h;
        }
    }

    public class BasicDestructableTile : ArenaTile
    {
        public bool has_drops;
        public List<EnvironmentDrops> drop_list;

        public BasicDestructableTile(ArenaTileType tm = 0, int tvn = 0, bool wk = false, bool mm = false, float h = 0, bool drp = false, List<EnvironmentDrops>edrp = null)
        {
            tileIDNum = tm;
            idVariation = tvn;

            is_walkable = wk;
            is_minecartMoveable = mm;
            is_destructable = true;

            has_drops = drp;
            drop_list = edrp;

            elevation = h;
        }

    }
}
