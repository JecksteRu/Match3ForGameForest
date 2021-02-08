using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Match3Game.Node
{
    internal class HorizontalLine
    {
        //public Point PositionInGrid;
        //public int LengthOfMatch;
        public List<Cell> Cells = new List<Cell>();

        public HorizontalLine(Cell startCell)
        {
            Cells.Add(startCell);
        }
    }

    internal class VerticalLine
    {
        //public Point PositionInGrid;
        //public int LengthOfMatch;
        public List<Cell> Cells = new List<Cell>();

        public VerticalLine(Cell startCell)
        {
            Cells.Add(startCell);
        }
    }
}