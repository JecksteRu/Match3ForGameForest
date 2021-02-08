using Microsoft.Xna.Framework;


namespace Match3Game.Node
{
    public abstract class ChipState
    {
        protected Chip Chip;
        public ChipState(Chip chip)
        {
            Chip = chip;
        }

        public abstract void Start();
        public abstract void Update(GameTime gameTime);
    }

}




