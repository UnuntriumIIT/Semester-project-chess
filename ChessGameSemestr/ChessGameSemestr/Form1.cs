using System;
using System.Windows.Forms;

namespace ChessGameSemestr
{
    public partial class Form1 : Form
    {
        ChessBoard Board;
        public Form1()
        {
            InitializeComponent();

            Board = new ChessBoard(this);

            Board.Draw();

            close.Click += new EventHandler(close_Click);
            button1.Click += new EventHandler(start_Click);
        }

        private void close_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Защита", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Close();
        }

        private void start_Click(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(888, 658);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(100, 30);
            this.close.TabIndex = 0;
            this.close.Text = "Выйти из игры";
            this.close.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(788, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Начать игру!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Chess Game!";
            this.ResumeLayout(false);

        }
    }
}
