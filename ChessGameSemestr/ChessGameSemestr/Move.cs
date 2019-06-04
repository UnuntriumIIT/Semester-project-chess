using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChessGameSemestr
{
    public class Move
    {
        #region Variables
        public Figure Figure;
        public Point From;
        public Point To;
        private List<Figure> figs;
        private Figure target;
        private readonly Form1 form;
        #endregion

        public Move(Figure Figure, Point From, Point To, List<Figure> Figures, Form1 form)
        {
            this.Figure = Figure;
            this.From = From;
            this.To = To;
            figs = Figures;
            target = figs.FindAndGetByPoint(To);
            this.form = form;
        }

        public override string ToString()
        {
            var f = GetCoord(From);
            var t = GetCoord(To);
            var c = "Black ";
            if (Figure.isWhite)
                c = "White ";
            return c + Figure.Type.ToLower() + " from: " + f + " To: " + t;
        }

        public bool isRight()
        {
            string type = Figure.Type;
            if (type != "Knight" && !IsOverJump())
            {
                switch (type)
                {
                    case "Pawn":
                        return CheckMoveForPawn();
                    case "Rook":
                        return CheckMoveForRook();
                    case "Bishop":
                        return CheckMoveForBishop();
                    case "King":
                        return CheckMoveForKing();
                    case "Queen":
                        return CheckMoveForBishop() || CheckMoveForRook();
                }
            }
            else if (type == "Knight")
                return CheckMoveForKnight();
            return false;
        }

        #region Logic

        private string GetCoord(Point p)
        {
            var letters = new[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            var numbers = new[] { "8", "7", "6", "5", "4", "3", "2", "1" };

            return letters[(p.X - 20) / 80] + numbers[p.Y / 80];
        }

        private bool IsOverJump()
        {
            string type = Figure.Type;
            switch (type)
            {
                case "Rook":
                    return CheckOverJumpForRook();
                case "Bishop":
                    return CheckOverJumpForBishop();
                case "Queen":
                    return CheckOverJumpForBishop() || CheckOverJumpForRook();
            }
            return false;
        }

        private bool CheckOverJumpForBishop()
        {
            if (From.X > To.X)
            {
                if (From.Y > To.Y)
                {
                    var x = From.X - 80;
                    var y = From.Y - 80;
                    while (x > To.X && y > To.Y)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, y));
                        x -= 80; y -= 80;
                        if (f != null)
                            return true;
                    }
                }
                else
                {
                    var x = From.X - 80;
                    var y = From.Y + 80;
                    while (x > To.X && y < To.Y)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, y));
                        x -= 80; y += 80;
                        if (f != null)
                            return true;
                    }
                }
            }
            else
            {
                if (From.Y > To.Y)
                {
                    var x = From.X + 80;
                    var y = From.Y - 80;
                    while (x < To.X && y > To.Y)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, y));
                        x += 80; y -= 80;
                        if (f != null)
                            return true;
                    }
                }
                else
                {
                    var x = From.X + 80;
                    var y = From.Y + 80;
                    while (x < To.X && y < To.Y)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, y));
                        x += 80; y += 80;
                        if (f != null)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool CheckOverJumpForRook()
        {
            if (From.X - To.X == 0)
                if (From.Y > To.Y)
                    for (var y = From.Y - 80; y > To.Y; y -= 80)
                    {
                        var f = figs.FindAndGetByPoint(new Point(From.X, y));
                        if (f != null)
                            return true;
                    }
                else
                    for (var y = From.Y + 80; y < To.Y; y += 80)
                    {
                        var f = figs.FindAndGetByPoint(new Point(From.X, y));
                        if (f != null)
                            return true;
                    }
            if (From.Y - To.Y == 0)
                if (From.X > To.X)
                    for (var x = From.X - 80; x > To.X; x -= 80)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, From.Y));
                        if (f != null)
                            return true;
                    }
                else
                    for (var x = From.X + 80; x < To.X; x += 80)
                    {
                        var f = figs.FindAndGetByPoint(new Point(x, From.Y));
                        if (f != null)
                            return true;
                    }
            return false;
        }

        private bool CheckMoveForKnight()
        {
            if (target == null)
            {
                if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 160)
                    return true;
                if (Math.Abs(From.Y - To.Y) == 80 && Math.Abs(From.X - To.X) == 160)
                    return true;
            }
            else
            {
                if ((!target.isWhite && Figure.isWhite)
                    || (target.isWhite && !Figure.isWhite))
                {
                    if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 160)
                        return true;
                    if (Math.Abs(From.Y - To.Y) == 80 && Math.Abs(From.X - To.X) == 160)
                        return true;
                }
            }
            return false;
        }

        private bool CheckMoveForKing()
        {
            if (target == null)
            {
                if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 80)
                    return true;
                if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 0)
                    return true;
                if (Math.Abs(From.X - To.X) == 0 && Math.Abs(From.Y - To.Y) == 80)
                    return true;
            }
            else
            {
                if ((!target.isWhite && Figure.isWhite)
                    || (target.isWhite && !Figure.isWhite))
                {
                    if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 80)
                        return true;
                    if (Math.Abs(From.X - To.X) == 80 && Math.Abs(From.Y - To.Y) == 0)
                        return true;
                    if (Math.Abs(From.X - To.X) == 0 && Math.Abs(From.Y - To.Y) == 80)
                        return true;
                }
            }
            return false;
        }

        private bool CheckMoveForBishop()
        {
            if (target == null)
            {
                if (From.X - To.X != 0 && From.Y - To.Y != 0)
                    if (Math.Abs(From.X - To.X) == Math.Abs(From.Y - To.Y))
                        return true;
            }
            else
            {
                if ((!target.isWhite && Figure.isWhite)
                    || (target.isWhite && !Figure.isWhite))
                    if (From.X - To.X != 0 && From.Y - To.Y != 0)
                        if (Math.Abs(From.X - To.X) == Math.Abs(From.Y - To.Y))
                            return true;
            }
            return false;
        }

        private bool CheckMoveForRook()
        {
            if (target == null)
            {
                if ((Math.Abs(From.X - To.X) <= 560 && From.Y - To.Y == 0) 
                    || Math.Abs(From.Y - To.Y) <= 560 && From.X - To.X == 0)
                    return true;
            }
            else
            {
                if ((!target.isWhite && Figure.isWhite) 
                    || (target.isWhite && !Figure.isWhite))
                    if ((Math.Abs(From.X - To.X) <= 560 && From.Y - To.Y == 0) 
                        || Math.Abs(From.Y - To.Y) <= 560 && From.X - To.X == 0)
                        return true;
            }
            return false;
        }

        private bool CheckMoveForPawn()
        {
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

        #endregion
    }
}
