using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace Match3Game.Node
{
    public class StartGridState : IGridState
    {
        public void Start(GridNode gridNode)
        {
            gridNode.CurrentGridState = null;
            gridNode.FillEmptySpaceInGrid();
            while (gridNode.HasMatchesOnStart())
            {
                gridNode.Chips.Clear();
                foreach (Dispencer dispencer in gridNode.Dispencers)
                {
                    dispencer.ClearOrder();
                }
                foreach (Cell cell in gridNode.Cells)
                {
                    cell.ClearCell();
                }
                gridNode.FillEmptySpaceInGrid();
            }
            gridNode.DispenceNewChips();
            gridNode.CurrentGridState = new MoveGridState();
        }

        public void Update(GridNode gridNode)
        {
            

        }
    }
}



