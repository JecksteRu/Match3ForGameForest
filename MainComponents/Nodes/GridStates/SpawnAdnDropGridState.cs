namespace Match3Game.Node
{
    public class SpawnAdnDropGridState : IGridState
    {
        public void Start(GridNode gridNode)
        {
            gridNode.SpawnBonusChips();
            gridNode.DropCells();
            gridNode.FillEmptySpaceInGrid();
            gridNode.DispenceNewChips();
            gridNode.CurrentGridState = new MoveGridState();
        }

        public void Update(GridNode gridNode)
        {

        }
    }
}



