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

            close.Click += new EventHandler(Close_Click);
            button1.Click += new EventHandler(Start_Click);
            button3.Click += new EventHandler(Pause_Click);
            button4.Click += new EventHandler(Surrender_Click);
        }

        private void Surrender_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите cдаться?", "Защита", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                Application.Restart();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Защита", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                Close();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            timer1.Tick += new EventHandler(Timer1_Tick);
            timer1.Start();
            Board.isGameStarted = true;
            if (Board.isWhiteMovingNow)
                label2.Text = "Белые";
            else
                label2.Text = "Чёрные";
        }

        DateTime date1 = new DateTime(0);

        private void Timer1_Tick(object sender, EventArgs e)
        {
            date1 = date1.AddSeconds(1);
            label3.Text = date1.ToString("HH:mm:ss");
        }

        private bool isPauseClicked = false;
        private void Pause_Click(object sender, EventArgs e)
        {
            if (isPauseClicked)
            {
                button3.Text = "Пауза";
                timer1.Start();
                button4.Enabled = true;
                if (Board.isWhiteMovingNow)
                    label2.Text = "Белые";
                else
                    label2.Text = "Чёрные";
                Board.isGameStarted = true;
            }
            else
            {
                button3.Text = "Продолжить";
                timer1.Stop();
                button4.Enabled = false;
                label2.Text = "Никто";
                Board.isGameStarted = false;
            }
            isPauseClicked = !isPauseClicked;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.close = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(785, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Сейчас ходят:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(874, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "никто";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(785, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "00:00:00";
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(877, 137);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Пауза";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(788, 658);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 30);
            this.button4.TabIndex = 7;
            this.button4.Text = "Сдаться";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(788, 166);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(200, 486);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "The Chess Game!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
