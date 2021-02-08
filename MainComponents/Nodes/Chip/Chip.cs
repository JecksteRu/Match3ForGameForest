using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Match3Game.Node
{




    public class Chip : Node
    {

        protected int PointsForDestruction = 7;

        public event Action<Chip> ChipSelected;
        public event Action<Chip> ChipDisposed;
        public event Action<int> ChipScoreAdd;

        public bool IsOnPosition = true;
        internal const int moveSpeed = 500;


        public readonly ChipColor ChipColor;

        protected Point positionInGrid = new Point(-1, -1);
        protected Cell currentCell;


        public float spriteOpacity = 1;

        protected Color spriteColor;
        protected Texture2D texture;

        private ChipState currentState;

        public void CallAddScore()
        {
            ChipScoreAdd?.Invoke(PointsForDestruction);
        }

        public Cell CurrentCell
        {
            get => currentCell;

            set
            {
                if (currentCell == value) { return; }
                currentCell = value;
                IsOnPosition = false;
            }

        }

        public ChipState CurrentState
        {
            get => currentState;
            set
            {
                if (currentState is not null && CurrentState.GetType() == value.GetType()) { return; }
                currentState = value;
                currentState?.Start();
            }
        }

        public Chip(ChipColor chipColor) : base("Chip")
        {
            ChipColor = chipColor;
            spriteColor = ConvertChipColor(chipColor);

        }

        public override void Initialize()
        {
            base.Initialize();
            SetSize(GridNode.cellSize, GridNode.cellSize);
        }

        private Color ConvertChipColor(ChipColor chipColor)
        {
            switch (chipColor)
            {
                case ChipColor.Blue:
                    return Color.Blue;
                case ChipColor.Green:
                    return Color.Green;
                case ChipColor.Purple:
                    return Color.Purple;
                case ChipColor.Red:
                    return Color.Red;
                case ChipColor.Yellow:
                    return Color.Yellow;
                default:
                    return (Color.White);

            }
        }
        public override void LoadContent()
        {
            base.LoadContent();

            texture = Global.Game.Content.Load<Texture2D>("shape");

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Global.SpriteBatch.Draw(texture, Rectangle, new Color(spriteColor, spriteOpacity));
        }




        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CurrentState?.Update(gameTime);




        }

        public override void UnloadContent()
        {

            GridNode gridNode = GetParent() as GridNode;
            gridNode?.Chips.Remove(this);

            base.UnloadContent();

        }


        public void CallChipSelected()
        {
            ChipSelected?.Invoke(this);

        }

        internal void SelectCell()
        {
            currentCell.OnSelect();
        }

        internal void DeselectCell()
        {
            currentCell.ClearColor();
        }



        public override void Dispose()
        {
            ChipDisposed?.Invoke(this);
            base.Dispose();
        }


    }
}




