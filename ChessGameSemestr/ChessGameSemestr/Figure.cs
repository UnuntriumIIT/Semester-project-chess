using System.Drawing;

namespace ChessGameSemestr
{
    public class Figure
    {
        public readonly string Type;
        public Point Position;

        public Figure(Point position, string type)
        {
            this.Type = type;
            this.Position = position;
        }
    }
}
