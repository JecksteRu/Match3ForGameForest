using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Match3Game.Node
{
    public sealed class BombChip : Chip, IBonusChip
    {

        List<Cell> cellsToBlast = new List<Cell>(9);
        
        public bool IsActivated { get; private set; } = false;

        private const float DetonateTimer = 250f;

        private float TimeBeforeDetonate = DetonateTimer;
        public BombChip(ChipColor chipColor) : base(chipColor)
        {
            PointsForDestruction = 45;
        }


        public override void LoadContent()
        {
            base.LoadContent();
            texture = Global.Game.Content.Load<Texture2D>("bomb");
        }

        public void UseBonus()
        {
            if (IsActivated) { return; }
            IsActivated = true;
            GridNode grid = GetParent() as GridNode;
            for (int i = -1; i <= 1; i++)
            {
                if (0 > currentCell.PositionInGrid.X + i || currentCell.PositionInGrid.X + i > GridNode.GridColumnNumber - 1) { continue; }
                for (int j = -1; j <= 1; j++)
                {
                    if (0 > currentCell.PositionInGrid.Y + j || currentCell.PositionInGrid.Y + j > GridNode.GridRowNumber - 1) { continue; }
                    Cell cell = grid.Cells[i + currentCell.PositionInGrid.X, currentCell.PositionInGrid.Y + j];
                    cellsToBlast.Add(cell);
                    cell.OnBang();

                }
            }
        }

        private void Detonate()
        {
            foreach (Cell cell in cellsToBlast)
            {
                cell.DestroyCurrentChip();
                cell.ClearColor();
            }
            Dispose();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsActivated)
            {
                if (TimeBeforeDetonate <= 0)
                {
                    Detonate();
                }
                TimeBeforeDetonate -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

    }


}




