using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public class ChessBoard
    {

        public List<Figure> Figures = new List<Figure>();
        private Form1 form;
        public ChessBoard(Form1 form)
        {
            this.form = form;
        }
        public void Draw()
        {
            form.Paint += new PaintEventHandler(DrawField);
            form.Paint += new PaintEventHandler(DrawSigns);
            form.Paint += new PaintEventHandler(DrawFigures);
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
            FillFigures();
            Graphics g = e.Graphics;
            for (int i = 0; i < Figures.Count; i++)
                g.DrawImage(Figures[i].Icon, Figures[i].Position.X, Figures[i].Position.Y, 70, 70);
        }

        private void DrawSigns(object sender, PaintEventArgs e)
        {
            var signs = new [] { "A", "B", "C", "D", "E", "F", "G", "H",
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

        private void FillFigures()
        {
            for(int i = 0; i < 8; i++)
            { 
                Figures.Add(new Figure(new Point(i * 80 + 20, 80), "Pawn", false, Image.FromFile(@"res/pawnB.png")));
                Figures.Add(new Figure(new Point(i * 80 + 20, 480), "Pawn", true, Image.FromFile(@"res/pawnW.png")));
            }
            Figures.Add(new Figure(new Point(20, 0), "Rook", false, Image.FromFile(@"res/rookB.png")));
            Figures.Add(new Figure(new Point(580, 0), "Rook", false, Image.FromFile(@"res/rookB.png")));
            Figures.Add(new Figure(new Point(100, 0), "Knight", false, Image.FromFile(@"res/knightB.png")));
            Figures.Add(new Figure(new Point(500, 0), "Knight", false, Image.FromFile(@"res/knightB.png")));
            Figures.Add(new Figure(new Point(180, 0), "Bishop", false, Image.FromFile(@"res/bishopB.png")));
            Figures.Add(new Figure(new Point(420, 0), "Bishop", false, Image.FromFile(@"res/bishopB.png")));
            Figures.Add(new Figure(new Point(260, 0), "Queen", false, Image.FromFile(@"res/queenB.png")));
            Figures.Add(new Figure(new Point(340, 0), "King", false, Image.FromFile(@"res/kingB.png")));
            Figures.Add(new Figure(new Point(20, 560), "Rook", true, new Bitmap(Image.FromFile(@"res/rookW.png"))));
            Figures.Add(new Figure(new Point(580, 560), "Rook", true, new Bitmap(Image.FromFile(@"res/rookW.png"))));
            Figures.Add(new Figure(new Point(100, 560), "Knight", true, new Bitmap(Image.FromFile(@"res/knightW.png"))));
            Figures.Add(new Figure(new Point(500, 560), "Knight", true, new Bitmap(Image.FromFile(@"res/knightW.png"))));
            Figures.Add(new Figure(new Point(180, 560), "Bishop", true, new Bitmap(Image.FromFile(@"res/bishopW.png"))));
            Figures.Add(new Figure(new Point(420, 560), "Bishop", true, new Bitmap(Image.FromFile(@"res/bishopW.png"))));
            Figures.Add(new Figure(new Point(260, 560), "Queen", true, new Bitmap(Image.FromFile(@"res/queenW.png"))));
            Figures.Add(new Figure(new Point(340, 560), "King", true, new Bitmap(Image.FromFile(@"res/kingW.png"))));
        }

        #endregion

    }
}
