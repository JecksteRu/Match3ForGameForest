using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Match3Game.Node
{
    public class Cell : Node
    {

        private Chip currentChip;
        public readonly Point PositionInGrid;
        private Texture2D texture;
        private Color Color = Color.White;

        public Cell(Point position) : base("Cell")
        {
            PositionInGrid = position;

        }

        public Chip CurrentChip
        {
            get => currentChip; 
            set
            {
                currentChip = value;
                if (currentChip is not null)
                    currentChip.CurrentCell = this;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Global.SpriteBatch.Draw(texture, Rectangle, Color);
        }

        public void OnBang()
        {
            Color = Color.Yellow;
        }

        public void OnSelect()
        {
            Color = Color.YellowGreen;
        }

        public void ClearColor()
        {
            Color = Color.White;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            texture = Global.Game.Content.Load<Texture2D>("Cell");
            
        }

        public void ClearCell()
        {
            currentChip?.Dispose();
            currentChip = null;
        }

        public void DestroyCurrentChip()
        {
            if (currentChip is not null && currentChip.CurrentState is not DestroyChipState)
                currentChip.CurrentState = new DestroyChipState(currentChip);
            currentChip = null;
        }

        
    }
}

