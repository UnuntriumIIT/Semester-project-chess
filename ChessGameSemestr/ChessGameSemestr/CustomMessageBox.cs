using System;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public partial class CustomMessageBox : Form
    {
        public string FigureChoiceType;
        private Form1 mainForm;
        public CustomMessageBox(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        public string ReturnData()
        {
            return FigureChoiceType;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FigureChoiceType = "Queen";
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            FigureChoiceType = "Knight";
            this.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            FigureChoiceType = "Rook";
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FigureChoiceType = "Bishop";
            this.Close();
        }
    }
}
