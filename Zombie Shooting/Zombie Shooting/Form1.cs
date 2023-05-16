using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zombie_Shooting
{
    public partial class Form1 : Form
    {
        bool up, down, right, left, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieKills = 0;
        int zombieSpeed = 3;
        Random randomNum = new Random();
        List<PictureBox> zombiesList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            if(playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
            }
            ammoLabel.Text = $"Ammo: {ammo}";
            killsLabel.Text = $"Kills: {zombieKills}";

            if(player.Left > 0 && left == true)
            {
                player.Left -= speed;
            }
            if(player.Left + player.Width < this.ClientSize.Width && right == true)
            {
                player.Left += speed;
            }
            if(player.Top > 45 && up == true)
            {
                player.Top -= speed;
            }
            if(player.Top + player.Height < this.ClientSize.Height && down == true)
            {
                player.Top += speed;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Down)
            {
                down = true;
                facing = "down";
                player.Image = Properties.Resources.down;
            }
            if(e.KeyCode == Keys.Up)
            {
                up = true;
                facing = "up";
                player.Image = Properties.Resources.up;
            }
            if(e.KeyCode == Keys.Right)
            {
                right = true;
                facing = "right";
                player.Image = Properties.Resources.right;
            }
            if(e.KeyCode == Keys.Left)
            {
                left = true;
                facing = "left";
                player.Image = Properties.Resources.left;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                down = false;
                
            }
            if (e.KeyCode == Keys.Up)
            {
                up = false;
               
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
                
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
                
            }
            if(e.KeyCode == Keys.Space)
            {
                ShootBullet(facing);
            }
        }
        private void ShootBullet(string direction)
        {
            Bullet bullet = new Bullet();
            bullet.direction = direction;
            bullet.bulletLeft = player.Left + player.Width / 2;
            bullet.bulletTop = player.Top + player.Height / 2;
            bullet.MakeBullet(this);
        }
        private void MakeZombie()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zdown;
            zombie.Left = randomNum.Next(0, 900);
            zombie.Top = randomNum.Next(0, 800);
        }
        private void RestartGame()
        {

        }

    }
}
