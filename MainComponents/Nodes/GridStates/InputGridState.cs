namespace Match3Game.Node
{
    public class InputGridState : IGridState
    {
        public void Start(GridNode gridNode)
        {
            foreach (Chip chip in gridNode.Chips)
            {
                chip.CurrentState = new InputChipState(chip);
            }
        }

        public void Update(GridNode gridNode)
        {

        }
    }
}



