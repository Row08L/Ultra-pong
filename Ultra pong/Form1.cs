using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Media;

namespace Ultra_pong
{
    public partial class Form1 : Form
    {
        Rectangle player1U = new Rectangle();
        Rectangle player1D = new Rectangle();
        Rectangle player1L = new Rectangle();
        Rectangle player1R = new Rectangle();
        Rectangle player1 = new Rectangle();
        Rectangle player2 = new Rectangle();
        Rectangle player2U = new Rectangle();
        Rectangle player2D = new Rectangle();
        Rectangle player2L = new Rectangle();
        Rectangle player2R = new Rectangle();
        Rectangle ball = new Rectangle(295, 195, 50, 50);

        Rectangle player2Goal = new Rectangle();
        Rectangle player1Goal = new Rectangle();


        Random randGen = new Random();

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 7;
        double ballXSpeed = 0;
        double ballYSpeed = 0;

        double playerBallFriction = 0.2;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        bool endTrue = false;

        int speedUpChance;
        int speedUp = 0;

        Stopwatch ballDecel = new Stopwatch();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitePen = new Pen(Color.White, 3);

        SoundPlayer bounce = new SoundPlayer(Properties.Resources.Jump_SoundBible_com_1007297584);
        SoundPlayer win = new SoundPlayer(Properties.Resources.Mundial_Ronaldinho_Soccer_64_Intro__HD_);
        SoundPlayer score = new SoundPlayer(Properties.Resources._1_person_cheering_Jett_Rifkin_1851518140);

        public Form1()
        {
            InitializeComponent();
            ball = new Rectangle(this.Width / 2, this.Height / 2 - 20, 40, 40);
            ballDecel.Start();
            player1 = new Rectangle(this.Width / 2 -5, this.Height / 2 + 195, 90, 90);
            player1U = new Rectangle(this.Width / 2, this.Height / 2 + 195, 80, 20);
            player1D = new Rectangle(this.Width / 2, this.Height / 2 + 265, 80, 20);
            player1L = new Rectangle(this.Width / 2 - 5, this.Height / 2 + 200, 20, 80);
            player1R = new Rectangle(this.Width / 2 + 65, this.Height / 2 + 200, 20, 80);

            player2 = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 195, 90, 90);
            player2U = new Rectangle(this.Width / 2, this.Height / 2 - 195, 80, 20);
            player2D = new Rectangle(this.Width / 2, this.Height / 2 - 125, 80, 20);
            player2L = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 190, 20, 80);
            player2R = new Rectangle(this.Width / 2 + 65, this.Height / 2 - 190, 20, 80);

            player1Goal = new Rectangle(this.Width / 2 - 150, this.Height - 10, 300, 10);
            player2Goal = new Rectangle(this.Width / 2 - 150, 0 - 5, 300, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(redBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(whiteBrush, player1Goal);
            e.Graphics.FillRectangle(whiteBrush, player2Goal);
            e.Graphics.FillRectangle(whiteBrush, player2);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (ballDecel.Elapsed.TotalMilliseconds > 50)
            {
                ballXSpeed /= 1.01;
                ballYSpeed /= 1.01;
            }
            

            //move ball 
            ball.X += Convert.ToInt32(ballXSpeed);
            ball.Y += Convert.ToInt32(ballYSpeed);

            //move player 1 
            if (wDown == true && player1.Y > this.Height / 2)
            {
                player1.Y -= playerSpeed;
                player1U.Y -= playerSpeed;
                player1D.Y -= playerSpeed;
                player1L.Y -= playerSpeed;
                player1R.Y -= playerSpeed;
            }

            if (sDown == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
                player1U.Y += playerSpeed;
                player1D.Y += playerSpeed;
                player1L.Y += playerSpeed;
                player1R.Y += playerSpeed;
            }

            if (aDown == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
                player1U.X -= playerSpeed;
                player1D.X -= playerSpeed;
                player1L.X -= playerSpeed;
                player1R.X -= playerSpeed;
            }

            if (dDown == true && player1.X + player1.Width < this.Width)
            {
                player1.X += playerSpeed;
                player1U.X += playerSpeed;
                player1D.X += playerSpeed;
                player1L.X += playerSpeed;
                player1R.X += playerSpeed;
            }

            //move player 2 
            if (upArrowDown == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
                player2U.Y -= playerSpeed;
                player2D.Y -= playerSpeed;
                player2L.Y -= playerSpeed;
                player2R.Y -= playerSpeed;
            }

            if (downArrowDown == true && player2.Y < this.Height / 2 - player2.Height)
            {
                player2.Y += playerSpeed;
                player2U.Y += playerSpeed;
                player2D.Y += playerSpeed;
                player2L.Y += playerSpeed;
                player2R.Y += playerSpeed;
            }

            if (leftArrowDown == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
                player2U.X -= playerSpeed;
                player2D.X -= playerSpeed;
                player2L.X -= playerSpeed;
                player2R.X -= playerSpeed;
            }

            if (rightArrowDown == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
                player2U.X += playerSpeed;
                player2D.X += playerSpeed;
                player2L.X += playerSpeed;
                player2R.X += playerSpeed;
            }

            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
                ball.Y = 0;

                bounce.Play();
            }
            if (ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
                ball.Y = this.Height - ball.Height;

                bounce.Play();
            }

            if (ball.IntersectsWith(player2Goal))
            {
                score.Play();
                player1Score++;
                p1ScoreLabel.Text = "Player 1 Score: " + player1Score;

                ballXSpeed = 0;
                ballYSpeed = 0;

                ball = new Rectangle(this.Width / 2, this.Height / 2 - 20, 40, 40);

                player1 = new Rectangle(this.Width / 2 - 5, this.Height / 2 + 195, 90, 90);
                player1U = new Rectangle(this.Width / 2, this.Height / 2 + 195, 80, 20);
                player1D = new Rectangle(this.Width / 2, this.Height / 2 + 265, 80, 20);
                player1L = new Rectangle(this.Width / 2 - 5, this.Height / 2 + 200, 20, 80);
                player1R = new Rectangle(this.Width / 2 + 65, this.Height / 2 + 200, 20, 80);

                player2 = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 195, 90, 90);
                player2U = new Rectangle(this.Width / 2, this.Height / 2 - 195, 80, 20);
                player2D = new Rectangle(this.Width / 2, this.Height / 2 - 125, 80, 20);
                player2L = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 190, 20, 80);
                player2R = new Rectangle(this.Width / 2 + 65, this.Height / 2 - 190, 20, 80);
            }

            if (ball.IntersectsWith(player1Goal))
            {
                score.Play();
                player2Score++;
                p2ScoreLabel.Text = "Player 2 Score: " + player2Score;

                ball = new Rectangle(this.Width / 2, this.Height / 2 - 20, 40, 40);

                ballXSpeed = 0;
                ballYSpeed = 0;

                player1 = new Rectangle(this.Width / 2 - 5, this.Height / 2 + 195, 90, 90);
                player1U = new Rectangle(this.Width / 2, this.Height / 2 + 195, 80, 20);
                player1D = new Rectangle(this.Width / 2, this.Height / 2 + 265, 80, 20);
                player1L = new Rectangle(this.Width / 2 - 5, this.Height / 2 + 200, 20, 80);
                player1R = new Rectangle(this.Width / 2 + 65, this.Height / 2 + 200, 20, 80);

                player2 = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 195, 90, 90);
                player2U = new Rectangle(this.Width / 2, this.Height / 2 - 195, 80, 20);
                player2D = new Rectangle(this.Width / 2, this.Height / 2 - 125, 80, 20);
                player2L = new Rectangle(this.Width / 2 - 5, this.Height / 2 - 190, 20, 80);
                player2R = new Rectangle(this.Width / 2 + 65, this.Height / 2 - 190, 20, 80);
            }



            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 

            if (player1U.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballYSpeed -= playerSpeed + speedUp;
                //
                player1.Y += playerSpeed;
                player1U.Y += playerSpeed;
                player1D.Y += playerSpeed;
                player1L.Y += playerSpeed;
                player1R.Y += playerSpeed;
                //
                if (aDown == true)
                {
                    ballXSpeed -= playerSpeed + speedUp - 2;
                }
                if (dDown == true)
                {
                    ballXSpeed += playerSpeed + speedUp - 2;
                }
                ball.Y = player1.Y - ball.Height;
            }
            if (player1D.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballYSpeed += playerSpeed + speedUp;
                //
                player1.Y -= playerSpeed;
                player1U.Y -= playerSpeed;
                player1D.Y -= playerSpeed;
                player1L.Y -= playerSpeed;
                player1R.Y -= playerSpeed;
                //
                if (aDown == true)
                {
                    ballXSpeed -= playerSpeed + speedUp - 2;
                }
                if (dDown == true)
                {
                    ballXSpeed += playerSpeed + speedUp - 2;
                }
                ball.Y = player1.Y + player1.Height;
            }
            if (player1L.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballXSpeed -= playerSpeed + speedUp;
                //
                player1.X += playerSpeed;
                player1U.X += playerSpeed;
                player1D.X += playerSpeed;
                player1L.X += playerSpeed;
                player1R.X += playerSpeed;
                //
                if (wDown == true)
                {
                    ballYSpeed -= playerSpeed + speedUp - 2;
                }
                if (dDown == true)
                {
                    ballYSpeed += playerSpeed + speedUp - 2;
                }
                ball.X = player1.X - ball.Width;
            }
            if (player1R.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballXSpeed += playerSpeed + speedUp;
                //
                player1.X -= playerSpeed;
                player1U.X -= playerSpeed;
                player1D.X -= playerSpeed;
                player1L.X -= playerSpeed;
                player1R.X -= playerSpeed;
                //
                if (wDown == true)
                {
                    ballYSpeed -= playerSpeed + speedUp - 2;
                }
                if (dDown == true)
                {
                    ballYSpeed += playerSpeed + speedUp - 2;
                }
                ball.X = player1.X + player1.Width;
            }


            if (player2U.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballYSpeed -= playerSpeed + speedUp;
                //
                player2.Y += playerSpeed;
                player2U.Y += playerSpeed;
                player2D.Y += playerSpeed;
                player2L.Y += playerSpeed;
                player2R.Y += playerSpeed;
                //
                if (leftArrowDown == true)
                {
                    ballXSpeed -= playerSpeed + speedUp - 2;
                }
                if (rightArrowDown == true)
                {
                    ballXSpeed += playerSpeed + speedUp - 2;
                }
                ball.Y = player2.Y - ball.Height;
            }
            if (player2D.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballYSpeed += playerSpeed + speedUp;
                ballYSpeed *= 1;
                //
                player2.Y -= playerSpeed;
                player2U.Y -= playerSpeed;
                player2D.Y -= playerSpeed;
                player2L.Y -= playerSpeed;
                player2R.Y -= playerSpeed;
                //
                if (leftArrowDown == true)
                {
                    ballXSpeed -= playerSpeed + speedUp - 2;
                }
                if (rightArrowDown == true)
                {
                    ballXSpeed += playerSpeed + speedUp - 2;
                }
                ball.Y = player2.Y + player2.Height;
            }
            if (player2L.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballXSpeed -= playerSpeed + speedUp;
                //
                player2.X += playerSpeed;
                player2U.X += playerSpeed;
                player2D.X += playerSpeed;
                player2L.X += playerSpeed;
                player2R.X += playerSpeed;
                //
                if (upArrowDown == true)
                {
                    ballYSpeed -= playerSpeed + speedUp - 2;
                }
                if (downArrowDown == true)
                {
                    ballYSpeed += playerSpeed + speedUp - 2;
                }
                ball.X = player2.X - ball.Width;
            }
            if (player2R.IntersectsWith(ball))
            {
                bounce.Play();
                SpeedUpRandom();
                ballXSpeed += playerSpeed + speedUp;
                //
                player2.X -= playerSpeed;
                player2U.X -= playerSpeed;
                player2D.X -= playerSpeed;
                player2L.X -= playerSpeed;
                player2R.X -= playerSpeed;
                //
                if (upArrowDown == true)
                {
                    ballYSpeed -= playerSpeed + speedUp - 2;
                }
                if (downArrowDown == true)
                {
                    ballYSpeed += playerSpeed + speedUp - 2;
                }
                ball.X = player2.X + player2.Width;
            }


            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.X < 0)
            {
                bounce.Play();
                ballXSpeed *= -1;
                ball.X = 0;
            }
            if (ball.X > this.Width - ball.Width)
            {
                bounce.Play();
                ballXSpeed *= -1;
                ball.X = this.Width - ball.Width;
            }



            if (player1Score >= 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 1 Wins!";
                win.Play();
                endTrue = true;
                label1.Visible = true;
                closeButton.Visible = true;
                restButton.Visible = true;
            }
            if (player2Score >= 3)
            {
                gameTimer.Enabled = false;
                winLabel.Visible = true;
                winLabel.Text = "Player 2 Wins!";
                win.Play();
                endTrue = true;
                label1.Visible = true;
                closeButton.Visible = true;
                restButton.Visible = true;
            }

            Refresh();
        }

        void SpeedUpRandom()
        {
            speedUpChance = randGen.Next(1, 21);
            switch(speedUpChance)
            {
                case 20:
                    speedUp = 10;
                    break;
                default:
                    speedUp = 0;
                    break;
            }
        }

        private void restButton_Click(object sender, EventArgs e)
        {
            if (endTrue == true)
            {
                Application.Restart();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (endTrue == true)
            {
                Application.Exit();
            }
        }
    }
}
