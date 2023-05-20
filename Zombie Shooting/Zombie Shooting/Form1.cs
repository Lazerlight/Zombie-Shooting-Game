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
            RestartGame();
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
                player.Image = Properties.Resources.dead;
                GameTimer.Stop(); 
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

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                
                if(x is PictureBox && (string)x.Tag == "zombie")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        playerHealth -= 5;
                        MakeZombie();
                    }
                    if (x.Left > player.Left)
                    {
                        x.Left -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zleft;
                    }
                    if (x.Left < player.Left)
                    {
                        x.Left += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zright;
                    }
                    if (x.Top > player.Top)
                    {
                        x.Top -= zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zup;
                    }
                    if (x.Top < player.Top)
                    {
                        x.Top += zombieSpeed;
                        ((PictureBox)x).Image = Properties.Resources.zdown;
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            zombieKills++;
                            this.Controls.Remove(j);
                            ((PictureBox)j).Dispose();
                            this.Controls.Remove(x);
                            ((PictureBox)x).Dispose();
                            zombiesList.Remove(((PictureBox)x));
                            MakeZombie();


                        }
                    }
                }

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
            if(e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                ShootBullet(facing);
                if(ammo < 1)
                {
                    DropAmmo();
                }
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
            zombie.SizeMode = PictureBoxSizeMode.AutoSize;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront(); 
        }

        private void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.SizeMode = PictureBoxSizeMode.AutoSize;
            ammo.Left = randomNum.Next(10, this.ClientSize.Width - ammo.Width);
            ammo.Top = randomNum.Next(60, this.ClientSize.Height - ammo.Height);
            ammo.Tag = "ammo";
            this.Controls.Add(ammo);

            ammo.BringToFront();
            player.BringToFront();
        }
        private void RestartGame()
        {
            player.Image = Properties.Resources.up;

            foreach (PictureBox i in zombiesList)
            {
                this.Controls.Remove(i);
            }
            for(int i = 0; i < 3; i++)
            {
                MakeZombie();
            }

            up = false;
            down = false;
            right = false;
            left = false;

            playerHealth = 100;
            zombieKills = 0;
            ammo = 10;

            GameTimer.Start();
        }

    }
}
