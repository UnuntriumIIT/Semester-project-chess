using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChessGameSemestr
{
    public class Move
    {
        public Figure Figure;
        public Point From;
        public Point To;
        private List<Figure> figs;

        public Move(Figure Figure, Point From, Point To, List<Figure> Figures)
        {
            this.Figure = Figure;
            this.From = From;
            this.To = To;
            figs = Figures;
        }

        public bool isRight()
        {
            string type = Figure.Type;
            bool result = false;
            switch (type)
            {
                case "Pawn":
                    result = CheckMoveForPawn();
                    break;
            }
            return result;
        }

        private bool CheckMoveForPawn()
        {
            Figure target = figs.FindAndGetByPoint(To);
            if (target == null)
            {
                if (Figure.isWhite)
                {
                    if (Figure.isFirstMoveForPawn)
                    {
                        if (From.Y == 480)
                            if (From.X - To.X == 0)
                                if (From.Y - To.Y <= 160)
                                    return true;
                        return false;
                    }
                    else
                    {
                        if (From.X - To.X == 0)
                            if (From.Y - To.Y == 80)
                                return true;
                        return false;
                    }
                }
                else
                {
                    if (Figure.isFirstMoveForPawn)
                    {
                        if (From.Y == 80)
                            if (To.X - From.X == 0)
                                if (To.Y - From.Y <= 160)
                                    return true;
                        return false;
                    }
                    else
                    {
                        if (To.X - From.X == 0)
                            if (To.Y - From.Y == 80)
                                return true;
                        return false;
                    }
                }
            }
            else
            {
                if (Figure.isWhite && !target.isWhite)
                {
                    if (Math.Abs(From.X - To.X) == 80)
                        if (From.Y - To.Y == 80)
                            return true;
                    return false;
                }
                else if (!Figure.isWhite && target.isWhite)
                {
                    if (Math.Abs(From.X - To.X) == 80)
                        if (To.Y - From.Y == 80)
                            return true;
                    return false;
                }
                return false;
            }                    
        }
    }
}
