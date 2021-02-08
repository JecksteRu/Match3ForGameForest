using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Match3Game.Node
{
    public sealed class HorizontalLineChip : Chip, IBonusChip
    {

        public bool IsActivated { get; private set; } = false;

        public HorizontalLineChip(ChipColor chipColor) : base(chipColor)
        {
            PointsForDestruction = 25;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            texture = Global.Game.Content.Load<Texture2D>("HBonus");
        }

        public void UseBonus()
        {
            GridNode gridNode = GetParent() as GridNode;
            Destroyer destroyer1 = new Destroyer(CurrentCell, new Point(1, 0));
            Destroyer destroyer2 = new Destroyer(CurrentCell, new Point(-1, 0));


            gridNode.AddChild(destroyer1);
            gridNode.AddChild(destroyer2);
            gridNode.Destroyers.Add(destroyer1);
            gridNode.Destroyers.Add(destroyer2);
            Dispose();
        }
    }


}




