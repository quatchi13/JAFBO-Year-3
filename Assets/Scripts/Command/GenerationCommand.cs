using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Generation_Tools;
using MapSaving;

namespace CommandPattern
{
    public class GenerationCommand : ICommand {
        private Space2D newVal;
        private Space2D original;
        private ArenaData aData;

        public GenerationCommand(Space2D s, ArenaData arena)
        {
            aData = arena;
            newVal = s;
            original = new Space2D(30, 30);
            BasicBuilderFunctions.CopySpaceAToB(arena.terrainValues, original, new List<Cell>{});
        }

        public void Execute()
        {
            BasicBuilderFunctions.CopySpaceAToB(newVal, aData.terrainValues, new List<Cell>{});
        }

        public void ExecuteUndo()
        {
            BasicBuilderFunctions.CopySpaceAToB(original, aData.terrainValues, new List<Cell> {});
        }
    }

}
