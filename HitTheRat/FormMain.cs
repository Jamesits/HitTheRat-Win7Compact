using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace HitTheRat
{
	public partial class FormMain : Form
	{
        Button[] arrBoard;
        Random r = new Random();
        int currentScore = 0;
        int highestScore = 0;
        int lastSelectedTileIndex;
        int contHitCount = 0;
        bool clicked = true;
        bool refreshedHighScore = false;
        bool hasStarted = false;
        int errorCount = 0;

		public FormMain()
		{
			InitializeComponent();
            arrBoard = new Button[] { button1, button2, button3, button6, button5, button4, button9, button8, button7 };
		}

        // UI logic

        private void FormMain_Load(object sender, EventArgs e)
        {
            pause();
            reset();
            lblNotif.Text = "New Game";
        }

        private void btnquit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cleanBoard()
        {
            foreach (Button i in arrBoard)
            {
                i.Text = "";
                i.BackColor = Color.White;
            }
        }

        private void displayScore()
        {
            lblscore.Text = "Current: " + currentScore + "\nHigh: " + highestScore;
            // lblNotif.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tileClicked(0, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tileClicked(1, 0, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tileClicked(2, 0, 2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tileClicked(3, 1, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tileClicked(4, 1, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tileClicked(5, 1, 2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tileClicked(6, 2, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tileClicked(7, 2, 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tileClicked(8, 2, 2);
        }

        // Game logic

        private void tileClicked(int index, int x, int y)
        {
            if (hasStarted == false) return;
            // MessageBox.Show("X=" + x + "Y=" + y);
            clicked = true;
            lblNotif.Text = "";
            if (index == lastSelectedTileIndex)
            {
                currentScore += 100;
                contHitCount += 1;
                if (contHitCount > 2) lblNotif.Text = contHitCount + " hits!!!";
                if (highestScore < currentScore)
                {
                    if (highestScore > 0 && refreshedHighScore == false) lblNotif.Text = "New High Score!";
                    highestScore = currentScore;
                    refreshedHighScore = true;
                }
            }
            else
            {
                if (index == -1) lblNotif.Text = "Miss!";
                else lblNotif.Text = "Error!";
                contHitCount = 0;
                errorCount += 1;
                if (errorCount == 10)
                {
                    pause();
                    reset();
                    hasStarted = false;
                    cleanBoard();
                    return;
                }
            }
            displayScore(); 
            if (currentScore == 1000) gameTimer.Interval = 800;
            if (currentScore == 2000) gameTimer.Interval = 600;
            if (currentScore == 3000) gameTimer.Interval = 500;
            if (currentScore == 4000) gameTimer.Interval = 400;
            if (currentScore == 5000) gameTimer.Interval = 300;
            if (currentScore == 6000) gameTimer.Interval = 200;
            cleanBoard();
        }

        private void pause()
        {
            gameTimer.Enabled = false;
            lblNotif.Text = "Paused";
        }

        private void unpause()
        {
            gameTimer.Enabled = true;
            lblNotif.Text = "Start";
            hasStarted = true;
        }

        private void reset()
        {
            currentScore = 0;
            contHitCount = 0;
            clicked = true;
            refreshedHighScore = false;
            errorCount = 0;
            lastSelectedTileIndex = -2;
            gameTimer.Interval = 1000;
            cleanBoard();
            displayScore();
            hasStarted = false;
            lblNotif.Text = "Game End";
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (hasStarted == false) {
                cleanBoard();
                return;
            }
            if (clicked == false) tileClicked(-1, 0, 0);
            cleanBoard();
            lastSelectedTileIndex = r.Next(arrBoard.Length);
            arrBoard[lastSelectedTileIndex].Text = "Rat!";
            arrBoard[lastSelectedTileIndex].BackColor = Color.Red;
            clicked = false;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            pause();
            DialogResult ret = MessageBox.Show("Start new game?", "Hit The Rat!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (ret == DialogResult.Yes)
            {
                reset();
            }
            unpause();
        }
	}
}