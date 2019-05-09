using System.Drawing;

namespace ChessGameSemestr
{
    public class Figure
    {
        public readonly string Type;
        public Point Position;
        public readonly bool isWhite;
        public Image Icon;

        public Figure(Point Position, string Type, bool isWhite, Image Icon)
        {
            this.Type = Type;
            this.Position = Position;
            this.isWhite = isWhite;
            this.Icon = Icon;
        }
    }
}
