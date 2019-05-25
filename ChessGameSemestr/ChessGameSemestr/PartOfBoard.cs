using System.Drawing;

namespace ChessGameSemestr
{
    public class PartOfBoard
    {
        public Color Color;
        public Point Point;

        public PartOfBoard(Point Point, Color Color)
        {
            this.Color = Color;
            this.Point = Point;
        }
    }
}
