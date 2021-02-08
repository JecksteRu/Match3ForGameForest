using System.Collections.Generic;



namespace Match3Game.Node
{
    public class Dispencer : Node
    {

        private int Colomn;

        private Stack<Chip> ChipsOrder;
        public Dispencer(int colomn) : base("Dispencer")
        {
            Colomn = colomn;
            ChipsOrder = new Stack<Chip>();
        }
        public override void Initialize()
        {
            base.Initialize();
            SetPosition((Colomn) * GridNode.cellSize, -200);
        }

        public void AddToOrder(Chip chip)
        {
            ChipsOrder.Push(chip);
            chip.spriteOpacity = 0;
        }

        public void ClearOrder()
        {
            ChipsOrder.Clear();
        }

        public void DispenceChips()
        {
            int count = ChipsOrder.Count;
            for (int i = 0; i < count; i++)
            {
                Chip chip = ChipsOrder.Pop();
                chip.SetPosition(GetPosition().X, GetPosition().Y - i * GridNode.cellSize);
                chip.spriteOpacity = 1;
            }

        }

    }
}

