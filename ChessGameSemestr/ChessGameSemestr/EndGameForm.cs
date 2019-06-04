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
    public partial class EndGameForm : Form
    {
        public EndGameForm(bool isWhiteMovedLast)
        {
            InitializeComponent();
            if (isWhiteMovedLast)
                label3.Text = "БЕЛЫЕ!";
            else
                label3.Text = "ЧЁРНЫЕ!";
            button1.Click += new EventHandler(NewGame);
            button2.Click += new EventHandler(Exit);
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewGame(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
