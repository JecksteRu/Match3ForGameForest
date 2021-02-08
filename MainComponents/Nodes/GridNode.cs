using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace Match3Game.Node
{
    public sealed class GridNode : Node
    {

        public event Action<int> OnScoreAdded; 

        public const int GridColumnNumber = 8;
        public const int GridRowNumber = 8;




        public const int cellSize = 75;
        public const int spawnChipYPosition = -100;


        private IGridState currentGridState;






        public Cell[,] Cells;
        public List<Chip> Chips;
        public Dispencer[] Dispencers;
        public List<Destroyer> Destroyers = new List<Destroyer>();
        public List<Chip> LastMovedChip;

        private List<Cell> cellsToClear = new List<Cell>();
        private List<IBonusChip> bonusChipsAreUsing = new List<IBonusChip>();

        private List<SpawnChipData> bombSpawnData = new List<SpawnChipData>();
        private List<SpawnChipData> hLineSpawnData = new List<SpawnChipData>();
        private List<SpawnChipData> vLineSpawnData = new List<SpawnChipData>();


        private Random random = new Random();

        private Chip lastSelectedCips;
        public List<Chip> lastMovedChips = new List<Chip>(2);

        public IGridState CurrentGridState
        {
            get => currentGridState;
            set
            {
                currentGridState = value;
                currentGridState?.Start(this);
            }
        }

        public GridNode() : base("Grid")
        {

            Cells = new Cell[GridColumnNumber, GridRowNumber];
            Chips = new List<Chip>();
            LastMovedChip = new List<Chip>();
            Dispencers = new Dispencer[GridColumnNumber];
            SetGlobalPosition(100, 100);


        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();

            CreateCellGrid();
            CreateDispencers();
            CurrentGridState = new StartGridState();

        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CurrentGridState?.Update(this);

        }


        public void FillEmptySpaceInGrid()
        {

            for (int x = 0; x < GridColumnNumber; x++)
            {
                for (int y = 0; y < GridRowNumber; y++)
                {
                    
                    if (Cells[x, y].CurrentChip is null) { CreateRegularChip(Cells[x, y]); }

                }
            }

        }

        public void DispenceNewChips()
        {
            foreach (Dispencer dispencer in Dispencers)
            {
                dispencer.DispenceChips();
            }
        }

        #region ChipCreation
        public void CreateRegularChip(Cell cell)
        {

            ChipColor chipColor = (ChipColor)random.Next(Enum.GetNames(typeof(ChipColor)).Length);
            Chip chip = SetChipSettings(new Chip(chipColor), cell);
            chip.spriteOpacity = 0;
            Dispencers[cell.PositionInGrid.X].AddToOrder(chip);

        }

        public void CreateBombChip(ChipColor chipColor, Cell cell)
        {
            SetChipSettings(new BombChip(chipColor), cell);
        }

        public void CreateHLineChip(ChipColor chipColor, Cell cell)
        {
            SetChipSettings(new HorizontalLineChip(chipColor), cell);
        }

        public void CreateVLineChip(ChipColor chipColor, Cell cell)
        {
            SetChipSettings(new VerticalLineChip(chipColor), cell);
        }

        private Chip SetChipSettings(Chip chip, Cell cell)
        {
            AddChild(chip);
            chip.SetSize(cellSize, cellSize);
            cell.CurrentChip = chip;
            Chips.Add(chip);
            chip.SetPosition(cell.GetPosition());
            chip.ChipSelected += OnChipSelected;
            chip.ChipScoreAdd += (int amount) => OnScoreAdded?.Invoke(amount);
            chip.ChipDisposed += OnChipDestructed;
            return chip;
        }

        #endregion


        public Cell GetCellInPoint(Point position)
        {
            for (int x = 0; x < GridColumnNumber; x++)
            {
                for (int y = 0; y < GridColumnNumber; y++)
                {
                    Cell cell = Cells[x, y];
                    if (cell.Rectangle.Contains(position)){ return cell; }
                }
            }
            return null;
        }

        private void OnChipDestructed(Chip chip)
        {
            Chips.Remove(chip);
        }


        private void CreateDispencers()
        {
            for (int x = 0; x < GridColumnNumber; x++)
            {
                Dispencers[x] = new Dispencer(x);
                AddChild(Dispencers[x]);

            }
        }

        public bool ChipsAreMoving()
        {
            foreach (Chip chipNode in Chips)
            {
                if (chipNode.CurrentState is MoveChipState)
                {
                    return true;
                }
            }
            return false;
        }



        private void CreateCellGrid()
        {
            for (int x = 0; x < GridColumnNumber; x++)
            {
                for (int y = 0; y < GridColumnNumber; y++)
                {

                    Cell cell = new Cell(new Point(x, y));
                    AddChild(cell);
                    Cells[x, y] = cell;
                    cell.SetSize(cellSize, cellSize);
                    cell.SetPosition(new Point(cellSize * x, cellSize * y));

                }
            }
        }


        private void OnChipSelected(Chip chip)
        {
            if (lastSelectedCips is null)
            {

                lastSelectedCips = chip;
                lastSelectedCips.SelectCell();
                return;
            }
            if (ChipsAreNotNeibors(chip, lastSelectedCips))
            {
                lastSelectedCips.DeselectCell();
                lastSelectedCips = null;
                return;
            }
            chip.SelectCell();
            SwapChips(chip, lastSelectedCips);
            lastSelectedCips.DeselectCell();
            chip.DeselectCell();
            lastSelectedCips = null;
            CurrentGridState = new MoveGridState();

        }

        public bool HasMatchesOnStart()
        {
            //Horizontal Check
            for (int x = 0; x < GridColumnNumber - 2; x++)
            {
                for (int y = 0; y < GridRowNumber; y++)
                {
                    ChipColor currentColor = Cells[x, y].CurrentChip.ChipColor;
                    if (currentColor == Cells[x + 1, y].CurrentChip.ChipColor && currentColor == Cells[x + 2, y].CurrentChip.ChipColor)
                    {
                        if (Cells[x + 1, y].CurrentChip is not IBonusChip && Cells[x + 2, y].CurrentChip is not IBonusChip)
                        {
                            return true;
                        }

                    }
                }
            }
            //Vertical check
            for (int x = 0; x < GridColumnNumber; x++)
            {
                for (int y = 0; y < GridRowNumber - 2; y++)
                {
                    ChipColor currentColor = Cells[x, y].CurrentChip.ChipColor;
                    if (currentColor == Cells[x, y + 1].CurrentChip.ChipColor && currentColor == Cells[x, y + 2].CurrentChip.ChipColor)
                    {
                        if (Cells[x, y + 1].CurrentChip is not IBonusChip && Cells[x, y + 2].CurrentChip is not IBonusChip)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        private bool ChipsAreNotNeibors(Chip chip1, Chip chip2)
        {
            Cell cell1 = chip1.CurrentCell;
            Cell cell2 = chip2.CurrentCell;
            if (chip1 == chip2) { return true; }
            if (Math.Abs(cell1.PositionInGrid.Y - cell2.PositionInGrid.Y) == 1 && (cell1.PositionInGrid.X == cell2.PositionInGrid.X)) { return false; }
            if (Math.Abs(cell1.PositionInGrid.X - cell2.PositionInGrid.X) == 1 && (cell1.PositionInGrid.Y == cell2.PositionInGrid.Y)) { return false; }
            return true;
        }

        public void SwapChips(Chip chip1, Chip chip2)
        {
            lastMovedChips.Clear();
            Cell cell1 = chip1.CurrentCell;
            Cell cell2 = chip2.CurrentCell;
            cell1.CurrentChip = chip2;
            cell2.CurrentChip = chip1;
            lastMovedChips.Add(chip1);
            lastMovedChips.Add(chip2);
        }

        public void SwapBack()
        {
            SwapChips(lastMovedChips[0], lastMovedChips[1]);
            lastMovedChips.Clear();
        }
        

        internal bool CheckMatch()
        {
            bool hasMatch = false;

            List<HorizontalLine> horizontalLines = new List<HorizontalLine>();
            List<VerticalLine> verticalLines = new List<VerticalLine>();

            List<HorizontalLine> crossHorizontalLines = new List<HorizontalLine>();
            List<VerticalLine> crossVerticalLines = new List<VerticalLine>();


            


            for (int x = 0; x < GridColumnNumber; x++)
            {
                horizontalLines.AddRange(GetHorizontalLines(x));
            }
            for (int y = 0; y < GridRowNumber; y++)
            {
                verticalLines.AddRange(GetVerticalLines(y));
            }

            if (horizontalLines.Count != 0 || verticalLines.Count != 0)
            {
                hasMatch = true;
            }

            foreach (HorizontalLine horizontalLine in horizontalLines)
            {
                foreach (VerticalLine verticalLine in verticalLines)
                {
                    if (CheckCross(horizontalLine, verticalLine))
                    {
                        Cell cell = Cells[verticalLine.Cells[0].PositionInGrid.X, horizontalLine.Cells[0].PositionInGrid.Y];
                        ChipColor chipColor = cell.CurrentChip.ChipColor;
                        cellsToClear.AddRange(horizontalLine.Cells);
                        cellsToClear.AddRange(verticalLine.Cells);
                        bombSpawnData.Add(new SpawnChipData(cell, chipColor));
                        crossHorizontalLines.Add(horizontalLine);
                        crossVerticalLines.Add(verticalLine);
                    }
                }

            }

            horizontalLines = horizontalLines.Except(crossHorizontalLines).ToList();
            verticalLines = verticalLines.Except(crossVerticalLines).ToList();

            foreach (HorizontalLine line in horizontalLines)
            {
                cellsToClear.AddRange(line.Cells);
                if (line.Cells.Count >= 4)
                {
                    Cell cellToSpawnBonus = line.Cells[0];
                    if (lastMovedChips.Count == 2)
                    {
                        cellToSpawnBonus = line.Cells.Find(cell => cell == lastMovedChips[0].CurrentCell || cell == lastMovedChips[1].CurrentCell);
                    }
                    if (line.Cells.Count >= 5)
                        bombSpawnData.Add(new SpawnChipData(cellToSpawnBonus, cellToSpawnBonus.CurrentChip.ChipColor));
                    else
                        hLineSpawnData.Add(new SpawnChipData(cellToSpawnBonus, cellToSpawnBonus.CurrentChip.ChipColor));

                }
            }

            foreach (VerticalLine line in verticalLines)
            {
                cellsToClear.AddRange(line.Cells);
                if (line.Cells.Count >= 4)
                {
                    Cell cellToSpawnBonus = line.Cells[0];
                    if (lastMovedChips.Count == 2)
                    {
                        cellToSpawnBonus = line.Cells.Find(cell => cell == lastMovedChips[0].CurrentCell || cell == lastMovedChips[1].CurrentCell);
                    }
                    if (line.Cells.Count >= 5)
                        bombSpawnData.Add(new SpawnChipData(cellToSpawnBonus, cellToSpawnBonus.CurrentChip.ChipColor));
                    else
                        vLineSpawnData.Add(new SpawnChipData(cellToSpawnBonus, cellToSpawnBonus.CurrentChip.ChipColor));
                }
            }



            

            return hasMatch;

        }

        internal void SpawnBonusChips()
        {
            foreach (SpawnChipData data in bombSpawnData)
            {
                CreateBombChip(data.ChipColor, data.Cell);
            }

            foreach (SpawnChipData data in hLineSpawnData)
            {
                CreateHLineChip(data.ChipColor, data.Cell);
            }

            foreach (SpawnChipData data in vLineSpawnData)
            {
                CreateVLineChip(data.ChipColor, data.Cell);
            }
            bombSpawnData.Clear();
            hLineSpawnData.Clear();
            vLineSpawnData.Clear();
        }


        internal void DropCells()
        {
            for (int x = 0; x < GridColumnNumber; x++)
            {
                for (int y = GridRowNumber - 1; y > 0; y--)
                {
                    if (Cells[x, y].CurrentChip is null)
                    {
                        int k = y - 1;
                        while (k >= 0 && Cells[x, k].CurrentChip is null)
                        {
                            k--;
                        }
                        if (k < 0) break;
                        Chip chip = Cells[x, k].CurrentChip;
                        Cells[x, k].CurrentChip = null;
                        Cells[x, y].CurrentChip = chip;
                    }
                }
            }
        }

        private bool CheckCross(HorizontalLine horizontalLine, VerticalLine verticalLine)
        {
            if (Enumerable.Range(horizontalLine.Cells[0].PositionInGrid.X, horizontalLine.Cells[0].PositionInGrid.X + horizontalLine.Cells.Count).Contains(verticalLine.Cells[0].PositionInGrid.X) )
            {
                if (Enumerable.Range(verticalLine.Cells[0].PositionInGrid.Y, verticalLine.Cells[0].PositionInGrid.Y + verticalLine.Cells.Count).Contains(horizontalLine.Cells[0].PositionInGrid.Y))
                {
                    return true;
                }
            }
            return false;
        }

        public void DestroyMatchedChips()
        {

            IEnumerable<Cell> cellsNeededToClear = cellsToClear.Distinct();
            foreach (Cell cell in cellsNeededToClear)
            {
                cell.DestroyCurrentChip();

            }
            

            
            cellsToClear.Clear();
        }

        public  bool HasDestructingChips()
        {
            List<Chip> chipList = Chips.ToList();
            foreach (Chip chip in chipList)
            {
                if (chip.CurrentState is DestroyChipState)
                {
                    return true;
                }
            }
            return false;
        }


        private IEnumerable<HorizontalLine> GetHorizontalLines(int y)
        {
            var color = Cells[0, y].CurrentChip.ChipColor;
            HorizontalLine line = new HorizontalLine(Cells[0, y]);
            for (int x = 1; x < GridColumnNumber; x++)
            {
                if (color == Cells[x, y].CurrentChip.ChipColor)
                {
                    line.Cells.Add(Cells[x, y]);
                }
                else
                {
                    if (line.Cells.Count >= 3)
                        yield return line;
                    line = new HorizontalLine(Cells[x, y]);
                    color = Cells[x, y].CurrentChip.ChipColor;
                }
            }
            if (line.Cells.Count >= 3)
                yield return line;
        }

        private IEnumerable<VerticalLine> GetVerticalLines(int x)
        {
            ChipColor color = Cells[x, 0].CurrentChip.ChipColor;
            VerticalLine line = new VerticalLine(Cells[x, 0]);
            for (int y = 1; y < GridRowNumber; y++)
            {
                if (color == Cells[x, y].CurrentChip.ChipColor)
                {
                    line.Cells.Add(Cells[x, y]);
                }
                else
                {
                    if (line.Cells.Count >= 3)
                        yield return line;
                    line = new VerticalLine(Cells[x, y]);
                    color = Cells[x, y].CurrentChip.ChipColor;
                }
            }
            if (line.Cells.Count >= 3)
                yield return line;
        }

    }
}

