namespace Match3Game.Node
{
    public struct SpawnChipData
    {
        
        public Cell Cell;
        public ChipColor ChipColor;

        public SpawnChipData(Cell cell, ChipColor chipColor)
        {
            Cell = cell;
            ChipColor = chipColor;
        }
    }
}

