namespace Match3Game.Node
{
    public class MoveGridState : IGridState
    {
        public void Start(GridNode gridNode)
        {
            foreach (Chip chip in gridNode.Chips)
            {
                chip.CurrentState = new MoveChipState(chip);
            }
        }

        public void Update(GridNode gridNode)
        {

            if (!gridNode.ChipsAreMoving())
            {
                gridNode.CurrentGridState = new ChipMatchState();
            }
        }
    }
}



