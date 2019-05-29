using System.Drawing;

namespace ChessGameSemestr
{
    public class Figure
    {
        public readonly string Type;
        public Point Position;
        public readonly bool isWhite;
        public Image Icon;
        public bool isFirstMoveForPawn;

        public Figure(Point Position, string Type, bool isWhite, Image Icon, bool isFirstMoveForPawn)
        {
            this.Type = Type;
            this.Position = Position;
            this.isWhite = isWhite;
            this.Icon = Icon;
            this.isFirstMoveForPawn = isFirstMoveForPawn;
        }
    }
}
