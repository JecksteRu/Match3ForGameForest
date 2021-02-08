using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Match3Game.Node
{
    public sealed class InputChipState : ChipState
    {
        public InputChipState(Chip chip) : base(chip)
        {
        }


        public override void Start() { }

        public override void Update(GameTime gameTime)
        {
            if (Chip is null) { return; }
            if (Chip.Rectangle.Contains(MouseInput.MouseState.Position) && MouseInput.MouseState.LeftButton == ButtonState.Pressed && MouseInput.LastMouseState.LeftButton == ButtonState.Released)
            {
                Chip.CallChipSelected();
            }
        }
    }

}




