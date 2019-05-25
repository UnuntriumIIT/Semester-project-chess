using System.Drawing;

namespace ChessGameSemestr
{
    public class Move
    {
        public Point From;
        public Point To;
        public bool isCorrect;

        public Move(Point From, Point To)
        {
            this.From = From;
            this.To = To;
        }
    }
}
