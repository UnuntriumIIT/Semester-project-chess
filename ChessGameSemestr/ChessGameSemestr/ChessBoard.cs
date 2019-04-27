using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public class ChessBoard
    {

        public Figure[,] Board = new Figure[8,8];
        private Form1 form;

        public ChessBoard(Form1 form)
        {
            this.form = form;
        }

        public void Draw()
        {
            form.Paint += new PaintEventHandler(this.DrawField);
            form.Paint += new PaintEventHandler(this.DrawSigns);
        }

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

        private void DrawSigns(object sender, PaintEventArgs e)
        {
            var signs = new String[] { "A", "B", "C", "D", "E", "F", "G", "H",
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
    }
}
