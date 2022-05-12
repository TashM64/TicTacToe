using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Generic;

namespace TicTacToeForm
{
    public partial class TicTacToeForm : Form
    {
        public TicTacToeForm()
        {
            InitializeComponent();

            GenerateButtons();
        }

        Button[,] buttons = new Button[3, 3];

        private void GenerateButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i,j] = new Button();
                    buttons[i, j].Size = new Size(150, 150);
                    buttons[i, j].Location = new Point(i * 150, j * 150);
                    buttons[i, j].FlatStyle = FlatStyle.Flat;
                    buttons[i, j].Font = new System.Drawing.Font(DefaultFont.FontFamily, 80, FontStyle.Bold);

                    // Define button click event
                    buttons[i, j].Click += new EventHandler(button_Click);

                    // Add button in to the panel
                    panel1.Controls.Add(buttons[i, j]);
                }			 
			}
        }
        
        void button_Click(object sender, EventArgs e)
        {
            // Load clicked button into local variable
            Button button = sender as Button;

            // Do nothing if block is already checked
            if (button.Text != "")
            {
                return;
            }

            // Mark block with current players symbol
            button.Text = PlayerButton.Text;

            TogglePlayer();
        }

        private void TogglePlayer()
        {
            CheckIfGameEnds();

            if (PlayerButton.Text == "X")
            {
                PlayerButton.Text = "O";
            }
            else
            {
                PlayerButton.Text = "X";
            } 
        }
        
        private void CheckIfGameEnds()
        {
            List<Button> winnerButtons = new List<Button>();
            
            #region // vertically
            for (int i = 0; i < 3; i++)
            {
                winnerButtons = new List<Button>();
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[i, j].Text != PlayerButton.Text)
                    {
                        break;
                    }

                    winnerButtons.Add(buttons[i, j]);
                    if (j == 2)
                    {
                        ShowWinner(winnerButtons);
                        return;
                    }
                }
            }
            #endregion            
            #region // horizontally
            for (int i = 0; i < 3; i++)
            {
                winnerButtons = new List<Button>();
                for (int j = 0; j < 3; j++)
                {
                    if (buttons[j, i].Text != PlayerButton.Text)
                    {
                        break;
                    }

                    winnerButtons.Add(buttons[j, i]);
                    if (j == 2)
                    {
                        ShowWinner(winnerButtons);
                        return;
                    }
                }
            }
            #endregion            
            #region// diagonally 1 (top-left to bottom-right)
            winnerButtons = new List<Button>();
            for (int i = 0, j = 0; i < 3; i++, j++)
            {
                if (buttons[i, j].Text != PlayerButton.Text)
                {
                    break;
                }

                winnerButtons.Add(buttons[i, j]);
                if (j == 2)
                {
                    ShowWinner(winnerButtons);
                    return;
                }
            }
            #endregion
            #region// diagonally 2 (bottom-left to top-right)
            winnerButtons = new List<Button>();
            for (int i = 2, j = 0; j < 3; i--, j++)
            {
                if (buttons[i, j].Text != PlayerButton.Text)
                {
                    break;
                }

                winnerButtons.Add(buttons[i, j]);
                if (j == 2)
                {
                    ShowWinner(winnerButtons);
                    return;   
                }
            }
            #endregion

            // check if all the blocks are marked
            foreach (var button in buttons)
            {
                if (button.Text == "")
                    return;
            }

            MessageBox.Show("Game Draw");
            Application.Restart();
        }

        private void ShowWinner(List<Button> winnerButtons)
        {
            // color all the winner blocks
            foreach (var button in winnerButtons)
            {
                button.BackColor = Color.Red;
            }

            MessageBox.Show("Player " + PlayerButton.Text + " wins");
            Application.Restart();
        }
    }
}
