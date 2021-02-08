using Microsoft.Xna.Framework;


namespace Match3Game.Node
{
    public sealed class MoveChipState : ChipState
    {

        private bool IsOnPosition = false;
        public MoveChipState(Chip chip) : base(chip)
        {

        }

        public override void Start()
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (IsOnPosition)
            {
                Chip.CurrentState = new WaitChipState(Chip);
                return;
            }
            Vector2 moveDirection = (Chip.CurrentCell.GetPosition() - Chip.GetPosition()).ToVector2();
            moveDirection.Normalize();
            float moveLength = (Chip.CurrentCell.GetPosition() - Chip.GetPosition()).ToVector2().Length();
            if (moveLength <= Chip.moveSpeed * gameTime.ElapsedGameTime.TotalSeconds)
            {
                Chip.SetPosition(Chip.CurrentCell.GetPosition());
                IsOnPosition = true;
            }
            else
            {
                Vector2 newMovementOfset = moveDirection * Chip.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                int x = (int)newMovementOfset.X;
                int y = (int)newMovementOfset.Y;
                Chip.SetPosition(new Point(x, y) + Chip.GetPosition());
            }
        }
    }

}




