using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public class ChessBoard
    {
        #region Variables
        private List<Figure> Figures = new List<Figure>();
        private List<PartOfBoard> Board = new List<PartOfBoard>();
        private readonly Form1 form;
        private bool isClickedBefore = false;
        private Point prevClick;
        private Point nextClick;
        private Color currColor;
        private Figure currFigure = null;
        public bool isWhiteMovingNow { get; set; }
        public bool isGameStarted { get; set; }
        public bool isShahForWhite { get; set; }
        public bool isShahForBlack { get; set; }
        #endregion

        public ChessBoard(Form1 form)
        {
            this.form = form;
            form.MouseClick += new MouseEventHandler(Form_MouseClick);
            FillFigures();
            FillTheBoard();
            isWhiteMovingNow = true;
            isGameStarted = false;
        }

        public void Draw()
        {
            form.Paint += new PaintEventHandler(DrawField)
                + new PaintEventHandler(DrawSigns)
                + new PaintEventHandler(DrawFigures);
        }

        #region Drawing_Methods

        private void DrawField(object sender, PaintEventArgs e)
        {
            var rects = new Rectangle[64];
            var g = e.Graphics;
            var squareColor = Color.Black;
            var u = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    rects[u] = new Rectangle(j * 80 + 20, i * 80, 80, 80);
                    Brush br = new SolidBrush(squareColor);
                    g.DrawRectangle(new Pen(squareColor), rects[u]);
                    g.FillRectangle(br, rects[u]);
                    if (squareColor == Color.Black)
                        squareColor = Color.White;
                    else
                        squareColor = Color.Black;
                    u++;
                }
                if (squareColor == Color.Black)
                    squareColor = Color.White;
                else
                    squareColor = Color.Black;
            }
        }

        private void DrawFigures(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < Figures.Count; i++)
                g.DrawImage(Figures[i].Icon, Figures[i].Position.X, Figures[i].Position.Y, 70, 70);
        }

        private void DrawSigns(object sender, PaintEventArgs e)
        {
            var signs = new[] { "A", "B", "C", "D", "E", "F", "G", "H",
                                "1", "2", "3", "4", "5", "6", "7", "8" };
            var g = e.Graphics;
            var brushForString = new SolidBrush(Color.Black);
            var x = 0;
            var y = 560;
            Point p;

            for (int i = 0; i < signs.Length; i++)
            {
                if (i < 8)
                {
                    p = new Point(x + 50, 645);
                    x += 80;
                }
                else
                {
                    p = new Point(5, y + 30);
                    y -= 80;
                }
                g.DrawString(signs[i], form.Font, brushForString, p);
            }
        }

        private void DrawMove(Graphics g)
        {
            var br = new SolidBrush(currColor);
            var p = new Pen(br);
            var r = new Rectangle(prevClick, new Size(80, 80));
            g.DrawRectangle(p, r);
            g.FillRectangle(br, r);
            p.Dispose();
            var target = Figures.FindAndGetByPoint(nextClick);
            g.DrawImage(currFigure.Icon, nextClick.X, nextClick.Y, 70, 70);
            Figures.Remove(currFigure);
            if (currFigure.Type != "Pawn")
                Figures.Add(new Figure(nextClick, currFigure.Type, currFigure.isWhite, currFigure.Icon, false));
            else
                if ((nextClick.Y != 0 && currFigure.isWhite)
                    || (nextClick.Y != 560 && !currFigure.isWhite))
                Figures.Add(new Figure(nextClick, currFigure.Type, currFigure.isWhite, currFigure.Icon, false));
            if (target != null)
                Figures.Remove(target);
        }

        #endregion

        #region Logic
        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            using (Graphics g = form.CreateGraphics())
            {
                if (isGameStarted)
                {
                    if (e.Location.X >= 20 && e.Location.X <= 660 && e.Location.Y >= 0 && e.Location.Y <= 640)
                    {
                        var click = GetClickLocation(e.Location);
                        if (isClickedBefore)
                        {
                            if (currFigure != null)
                            {
                                if ((currFigure.isWhite && isWhiteMovingNow) || (!currFigure.isWhite && !isWhiteMovingNow))
                                {
                                    var move = new Move(currFigure, prevClick, click, Figures);
                                    if (move.isRight())
                                    {
                                        nextClick = click;
                                        DrawMove(g);
                                        if (currFigure.Type == "Pawn")
                                        {
                                            currFigure.isFirstMoveForPawn = false;
                                            if ((currFigure.isWhite && nextClick.Y == 0)
                                                || (!currFigure.isWhite && nextClick.Y == 560))
                                                Transformation(currFigure);
                                        }
                                        ListViewItem lvi = new ListViewItem();
                                        lvi.Text = move.ToString();
                                        form.listView1.Items.Add(lvi);
                                        isWhiteMovingNow = !isWhiteMovingNow;
                                        if (isWhiteMovingNow)
                                            form.label2.Text = "Белые";
                                        else
                                            form.label2.Text = "Чёрные";
                                        isShahForBlack = CheckBlackKing();
                                        isShahForWhite = CheckWhiteKing();
                                    }
                                    isClickedBefore = false;
                                    currColor = Board.WhatColorNow(click);
                                }
                            }
                        }
                        else
                        {
                            var figure = Figures.FindAndGetByPoint(click);
                            if (figure != null)
                            {
                                if ((figure.isWhite && isWhiteMovingNow) || (!figure.isWhite && !isWhiteMovingNow))
                                {
                                    currFigure = Figures.FindAndGetByPoint(click);
                                    if (currFigure != null)
                                    {
                                        currColor = Board.WhatColorNow(click);
                                        prevClick = click;
                                        isClickedBefore = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckWhiteKing()
        {
            var king = Figures.GetWhiteKing();
            foreach (var f in Figures)
            {
                if (!f.isWhite)
                {
                    var tempFigure = new Figure(king.Position, f.Type, f.isWhite, f.Icon, false);
                    var move = new Move(tempFigure, king.Position, f.Position, Figures);
                    if (move.isRight())
                        return true;
                }
            }
            return false;
        }

        private bool CheckBlackKing()
        {
            var king = Figures.GetBlackKing();
            foreach (var f in Figures)
            {
                if (f.isWhite)
                {
                    var tempFigure = new Figure(king.Position, f.Type, !f.isWhite, f.Icon, false);
                    var move = new Move(tempFigure, king.Position, f.Position, Figures);
                    if(move.isRight())
                        return true;
                }
            }
            return false;
        }

        private void Transformation(Figure Pawn)
        {
            var box = new CustomMessageBox(form);
            form.Visible = false;
            box.ShowDialog();
            if (box.DialogResult == DialogResult.Cancel)
            {
                var result = box.ReturnData();
                form.Visible = true;
                if (result != "")
                {
                    Figures.Remove(Pawn);
                    var image = GetImageByName(result, Pawn.isWhite);
                    if (image != null)
                    {
                        Figures.Add(new Figure(nextClick, result, Pawn.isWhite, image, false));
                        using (Graphics g = form.CreateGraphics())
                        {
                            g.DrawImage(image, nextClick.X, nextClick.Y, 70, 70);
                        }
                    }
                    else throw new System.Exception("Nope! My bad :) (Image in transformation)");
                }
            }
        }

        private Image GetImageByName(string Name, bool isWhite)
        {
            switch (Name)
            {
                case "Queen":
                    if (isWhite)
                        return Image.FromFile(@"../../resources/queenW.png");
                    else
                        return Image.FromFile(@"../../resources/queenB.png");
                case "Knight":
                    if (isWhite)
                        return Image.FromFile(@"../../resources/knightW.png");
                    else
                        return Image.FromFile(@"../../resources/knightB.png");
                case "Rook":
                    if (isWhite)
                        return Image.FromFile(@"../../resources/rookW.png");
                    else
                        return Image.FromFile(@"../../resources/rookB.png");
                case "Bishop":
                    if (isWhite)
                        return Image.FromFile(@"../../resources/bishopW.png");
                    else
                        return Image.FromFile(@"../../resources/bishopB.png");
                default:
                    return null;
            }
        }

        private Point GetClickLocation(Point ClickPoint)
        {
            var X = ClickPoint.X;
            var Y = ClickPoint.Y;
            var prevX = 20;
            for (int currX = 100; currX <= 660; currX += 80)
            {
                if (X >= prevX && X <= currX)
                {
                    X = prevX;
                    break;
                }
                else prevX = currX;
            }
            var prevY = 0;
            for (int currY = 80; currY <= 640; currY += 80)
            {
                if (Y >= prevY && Y <= currY)
                {
                    Y = prevY;
                    break;
                }
                else prevY = currY;
            }
            return new Point(X, Y);
        }

        #endregion

        #region Filling_Methods

        private void FillTheBoard()
        {
            var squareColor = Color.Black;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Point point = new Point(j * 80 + 20, i * 80);
                    Board.Add(new PartOfBoard(point, squareColor));
                    if (squareColor == Color.Black)
                        squareColor = Color.White;
                    else
                        squareColor = Color.Black;
                }
                if (squareColor == Color.Black)
                    squareColor = Color.White;
                else
                    squareColor = Color.Black;
            }
        }

        private void FillFigures()
        {
            for (int i = 0; i < 8; i++)
            {
                Figures.Add(new Figure(new Point(i * 80 + 20, 80), "Pawn", false, Image.FromFile(@"../../resources/pawnB.png"), true));
                Figures.Add(new Figure(new Point(i * 80 + 20, 480), "Pawn", true, Image.FromFile(@"../../resources/pawnW.png"),true));
            }
            Figures.Add(new Figure(new Point(20, 0), "Rook", false, Image.FromFile(@"../../resources/rookB.png"), false));
            Figures.Add(new Figure(new Point(580, 0), "Rook", false, Image.FromFile(@"../../resources/rookB.png"), false));
            Figures.Add(new Figure(new Point(100, 0), "Knight", false, Image.FromFile(@"../../resources/knightB.png"), false));
            Figures.Add(new Figure(new Point(500, 0), "Knight", false, Image.FromFile(@"../../resources/knightB.png"), false));
            Figures.Add(new Figure(new Point(180, 0), "Bishop", false, Image.FromFile(@"../../resources/bishopB.png"), false));
            Figures.Add(new Figure(new Point(420, 0), "Bishop", false, Image.FromFile(@"../../resources/bishopB.png"), false));
            Figures.Add(new Figure(new Point(260, 0), "Queen", false, Image.FromFile(@"../../resources/queenB.png"), false));
            Figures.Add(new Figure(new Point(340, 0), "King", false, Image.FromFile(@"../../resources/kingB.png"), false));
            Figures.Add(new Figure(new Point(20, 560), "Rook", true, Image.FromFile(@"../../resources/rookW.png"), false));
            Figures.Add(new Figure(new Point(580, 560), "Rook", true, Image.FromFile(@"../../resources/rookW.png"), false));
            Figures.Add(new Figure(new Point(100, 560), "Knight", true, Image.FromFile(@"../../resources/knightW.png"), false));
            Figures.Add(new Figure(new Point(500, 560), "Knight", true, Image.FromFile(@"../../resources/knightW.png"), false));
            Figures.Add(new Figure(new Point(180, 560), "Bishop", true, Image.FromFile(@"../../resources/bishopW.png"), false));
            Figures.Add(new Figure(new Point(420, 560), "Bishop", true, Image.FromFile(@"../../resources/bishopW.png"), false));
            Figures.Add(new Figure(new Point(260, 560), "Queen", true, Image.FromFile(@"../../resources/queenW.png"), false));
            Figures.Add(new Figure(new Point(340, 560), "King", true, Image.FromFile(@"../../resources/kingW.png"), false));
        }

        #endregion
    }
}
