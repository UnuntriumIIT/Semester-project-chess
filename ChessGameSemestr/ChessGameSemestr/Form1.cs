using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawBoard();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(700, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Chess Game!";
            this.ResumeLayout(false);

        }

        private void DrawBoard()
        {
            var previousColor = Color.White;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    var p = new Panel();
                    p.Size = new Size(80, 80);
                    p.Location = new Point((j-1) * 80, (i-1) * 80);
                    if (previousColor == Color.White)
                    {
                        p.BackColor = previousColor = Color.Black;
                    }
                    else
                    {
                        p.BackColor = previousColor = Color.White;
                    }
                    this.Controls.Add(p);
                }
                if (previousColor == Color.White)
                    previousColor = Color.Black;
                else
                    previousColor = Color.White;
            }
            var letters = new String[] { "A", "B", "C", "D", "E", "F", "G", "H" };
            var numbers = new String[] { "1", "2", "3", "4", "5", "6", "7", "8" };
            
            int x = 0;
            foreach(var e in letters)
            {
                var p = new TextBox();
                p.Size = new Size(20, 20);
                p.Text = e;
                p.ReadOnly = true;
                p.TextAlign = HorizontalAlignment.Center;
                p.Location = new Point(x+30, 640);
                x += 80;
                this.Controls.Add(p);
            }
            int y = 560;
            foreach (var e in numbers)
            {
                var p = new TextBox();
                p.Size = new Size(20, 20);
                p.Text = e;
                p.ReadOnly = true;
                p.TextAlign = HorizontalAlignment.Center;
                p.Location = new Point(640, y+30);
                y -= 80;
                this.Controls.Add(p);
            }
        }
    }
}
