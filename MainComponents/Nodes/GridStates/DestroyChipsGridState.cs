namespace Match3Game.Node
{
    public class DestroyChipsGridState : IGridState
    {
        public void Start(GridNode gridNode)
        {
             gridNode.DestroyMatchedChips();
        }

        public void Update(GridNode gridNode)
        {
            if (gridNode.Destroyers.Count != 0 || gridNode.HasDestructingChips()) { return; }
            gridNode.CurrentGridState = new SpawnAdnDropGridState();
            
        }
    }
}



