using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAFprocedural
{
    public class ArenaData
    {
        public Space2D terrainValues;

        public ArenaData()
        {
            terrainValues = new Space2D(30, 30);
        }
    }
}
