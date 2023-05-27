using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RacingGame
{
    public partial class Form1 : Form
    {
        private Point pos;
        private bool dragging;
        private int countCoins = 0;
        public Form1()
        {
            InitializeComponent();
            bg1.MouseDown += MouseClickDown;
            bg1.MouseUp += MouseClickUp;
            bg1.MouseMove += MouseClickMove;
            bg2.MouseDown += MouseClickDown;
            bg2.MouseUp += MouseClickUp;
            bg2.MouseMove += MouseClickMove;
            labelLose.Visible = false;
            btnRestart.Visible = false;
            KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void MouseClickDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            pos.X = e.X;
            pos.Y = e.Y;
        }
        private void MouseClickUp(object sender, MouseEventArgs e)
        {
            dragging = false;

        }
        private void MouseClickMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point curPoint = PointToScreen(new Point(e.X, e.Y));
                this.Location = new Point(curPoint.X - pos.X, curPoint.Y - pos.Y + bg1.Top);

            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int speed = 10;
            bg1.Top += speed;
            bg2.Top += speed;
            int enemySpeed = 8;
            enemy1.Top += enemySpeed;
            enemy2.Top += enemySpeed;
            coin.Top += speed;
            if (bg1.Top >= 650)
            {
                bg1.Top = 0;
                bg2.Top = -650;

            }

            if (player.Bounds.IntersectsWith(coin.Bounds))
            {
                countCoins++;
                labelCoins.Text = $@"Coins: {countCoins.ToString()}";
                coin.Top = -50;
                Random rand = new Random();
                coin.Left = rand.Next(150, 650);
            }
           

            if (enemy1.Top >= 650 && enemy2.Top>=650 && coin.Top >=650)
            {
                enemy1.Top = -130;
                enemy2.Top = -400;
                coin.Top = -50;
                Random rand = new Random();
                enemy1.Left = rand.Next(150, 300);
                enemy2.Left = rand.Next(300, 560);
                coin.Left = rand.Next(150, 560);
            }

            if (player.Bounds.IntersectsWith(enemy1.Bounds) || player.Bounds.IntersectsWith(enemy2.Bounds))
            {
                timer.Enabled = false;
                labelLose.Visible = true;
                btnRestart.Visible = true;
            }
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int speed = 10;
            if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A)&& player.Left > 150)
            {
                player.Left -= speed;

            }
            else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D)&&player.Right<700)
            {
                player.Left += speed;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            enemy1.Top = -130;
            enemy2.Top = -400;
            labelLose.Visible = false;
            btnRestart.Visible = false;
            timer.Enabled = true;
            countCoins = 0;
            labelCoins.Text = $@"Coins: {countCoins.ToString()}";
            coin.Top = -550;
        }
    }
}
