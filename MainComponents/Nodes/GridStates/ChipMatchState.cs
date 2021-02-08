namespace Match3Game.Node
{
    public class ChipMatchState : IGridState
    {
        public void Start(GridNode gridNode)
        {

            if (gridNode.CheckMatch())
            {
                gridNode.lastMovedChips.Clear();
                gridNode.CurrentGridState = new DestroyChipsGridState();
            }
            else if (gridNode.lastMovedChips.Count == 2)
            {
                gridNode.SwapBack();
                gridNode.CurrentGridState = new MoveGridState();
            }
            else
                gridNode.CurrentGridState = new InputGridState();


        }

        public void Update(GridNode gridNode)
        {



            //gridNode.CurrentGridState = new FallGridState();

        }
    }
}



