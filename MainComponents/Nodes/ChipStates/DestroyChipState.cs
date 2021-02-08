using Microsoft.Xna.Framework;


namespace Match3Game.Node
{
    public sealed class DestroyChipState : ChipState
    {
        public DestroyChipState(Chip chip) : base(chip)
        {
        }

        public override void Start()
        {
            Chip.CallAddScore();
            if (Chip is IBonusChip)
            {
                (Chip as IBonusChip).UseBonus();
            }
            else
                Chip.Dispose();
        }

        public override void Update(GameTime gameTime)
        {

        }

    }

}




