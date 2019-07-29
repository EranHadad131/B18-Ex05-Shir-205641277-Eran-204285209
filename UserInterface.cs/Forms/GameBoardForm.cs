using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05_Checkers
{
    public class GameBoardForm : Form
    {
        private readonly Gameplay.Soldier m_Solider;
        private Label m_LabelPlayer1 = new Label();
        private Label m_LabelPlayer2 = new Label();
        private UserInterface m_GameProperties;
        private Label m_LabelCurrentPawnPlayer1 = new Label();
        private Label m_LabelCurrentPawnPlayer2 = new Label();
        private Label m_LabelPlayer1Result = new Label();
        private Label m_LabelPlayer2Result = new Label();
        private CheckersPawnButton m_SourceButton;
        private CheckersPawnButton[,] m_Buttons;
        private GameBoard m_Board;
        private Gameplay m_Gameplay = new Gameplay();
        private bool m_Clicked = false;

        public GameBoardForm(UserInterface i_GameProperties)
        {
            this.Text = "Checkers";
            this.WindowState = FormWindowState.Maximized;
            m_GameProperties = i_GameProperties;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            m_Solider = new Gameplay.Soldier();
            initBoardForm();
        }

        private void initBoardForm()
        {
            int c, buttonTop = 50, buttonLeft = 500, currentButtonLeft = buttonLeft;

            Image FormBoardWallpaper = new Bitmap(Properties.Resources.FormBoardWallpaper);
            this.BackgroundImage = FormBoardWallpaper;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            m_LabelPlayer1.Text = m_GameProperties.Player1.Name + " {X}{K}:" + m_GameProperties.Player1.Result;
            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer1.Top = 10;
            m_LabelPlayer1.Left = 500;
            m_LabelPlayer1.ForeColor = Color.Red;
            m_LabelPlayer1.Font = new Font(m_LabelPlayer1.Font.Name, 12, FontStyle.Bold);
            m_LabelPlayer1.BackColor = Color.Transparent;

            m_LabelCurrentPawnPlayer1.Text = m_GameProperties.Player1.Name + " | Current Pawn in board = " + m_GameProperties.Player1.CurrentPawn;
            m_LabelCurrentPawnPlayer1.Font = new Font(m_LabelPlayer1.Font.Name, 12, FontStyle.Bold);
            m_LabelCurrentPawnPlayer1.ForeColor = Color.Red;
            m_LabelCurrentPawnPlayer1.AutoSize = false;
            m_LabelCurrentPawnPlayer1.Size = new Size(500, 20);
            m_LabelCurrentPawnPlayer1.BackColor = Color.Transparent;
            m_LabelCurrentPawnPlayer1.Top = 10;
            m_LabelCurrentPawnPlayer1.Left = 1500;

            m_LabelPlayer1Result.Text = m_GameProperties.Player1.Result.ToString();
            m_LabelPlayer1Result.AutoSize = true;
            m_LabelPlayer1Result.Top = 10;
            m_LabelPlayer1Result.Left = m_LabelPlayer1.Right - 50;
            m_LabelPlayer1Result.ForeColor = Color.White;
            m_LabelPlayer1Result.Font = new Font(m_LabelPlayer1Result.Font.Name, 12, FontStyle.Bold);
            m_LabelPlayer1Result.BackColor = Color.Transparent;

            m_LabelCurrentPawnPlayer2.Size = new Size(500, 20);
            m_LabelCurrentPawnPlayer2.Text = m_GameProperties.Player2.Name + " | Current Pawn in board = " + m_GameProperties.Player2.CurrentPawn;
            m_LabelCurrentPawnPlayer2.Font = new Font(m_LabelPlayer1.Font.Name, 12, FontStyle.Bold);
            m_LabelCurrentPawnPlayer2.AutoSize = false;
            m_LabelCurrentPawnPlayer2.ForeColor = Color.White;
            m_LabelCurrentPawnPlayer2.BackColor = Color.Transparent;
            m_LabelCurrentPawnPlayer2.Top = 40;
            m_LabelCurrentPawnPlayer2.Left = 1500;

            m_LabelPlayer2.Text = m_GameProperties.Player2.Name + " {O}{U}:" + m_GameProperties.Player2.Result;
            m_LabelPlayer2.AutoSize = true;
            m_LabelPlayer2.Top = 8;
            m_LabelPlayer2.Left = 900;
            m_LabelPlayer2.ForeColor = Color.White;
            m_LabelPlayer2.Font = new Font(m_LabelPlayer2.Font.Name, 12, FontStyle.Bold);
            m_LabelPlayer2.BackColor = Color.Transparent;

            m_LabelPlayer2Result.Text = m_GameProperties.Player2.Result.ToString();
            m_LabelPlayer2Result.AutoSize = true;
            m_LabelPlayer2Result.Top = 8;
            m_LabelPlayer2Result.Left = m_LabelPlayer2.Right + 5;
            m_LabelPlayer2Result.ForeColor = Color.White;
            m_LabelPlayer2Result.Font = new Font(m_LabelPlayer2Result.Font.Name, 12, FontStyle.Bold);
            m_LabelPlayer2Result.BackColor = Color.Transparent;

            m_Buttons = new CheckersPawnButton[m_GameProperties.BoardSize, m_GameProperties.BoardSize];
            m_Board = new GameBoard(m_GameProperties.BoardSize);

            for (int r = 0; r < m_GameProperties.BoardSize; r++)
            {
                for (c = 0; c < m_GameProperties.BoardSize; c++)
                {
                    m_Buttons[r, c] = new CheckersPawnButton(m_GameProperties.BoardSize);
                    if (m_GameProperties.BoardSize == 6)
                    {
                        m_Buttons[r, c].Size = new Size(145, 145);
                    }
                    else if (m_GameProperties.BoardSize == 8)
                    {
                        m_Buttons[r, c].Size = new Size(125, 125);
                    }
                    else
                    {
                        m_Buttons[r, c].Size = new Size(100, 100);
                    }

                    m_Buttons[r, c].Text = m_Board.GetBoard[r, c];
                    m_Buttons[r, c].Font = new Font("Tahoma", 8, FontStyle.Bold);
                    m_Buttons[r, c].SetButtonImage(m_Board.GetBoard[r, c]);
                    m_Buttons[r, c].Font = new Font(m_Buttons[r, c].Font.Name, 10, FontStyle.Bold);
                    m_Buttons[r, c].AutoSize = true;
                    m_Buttons[r, c].Top = buttonTop;
                    m_Buttons[r, c].Left = currentButtonLeft;
                    if (GameBoard.IsValidArea(r, c))
                    {
                        m_Buttons[r, c].BackColor = Color.White;
                        m_Buttons[r, c].Row = r;
                        m_Buttons[r, c].Col = c;
                        m_Buttons[r, c].Click += new EventHandler(buttonFirstClick_Click);
                    }
                    else
                    {
                        m_Buttons[r, c].Enabled = false;
                        m_Buttons[r, c].BackColor = Color.Gray;
                    }

                    currentButtonLeft = m_Buttons[r, c].Right;
                    this.Controls.Add(m_Buttons[r, c]);
                }

                currentButtonLeft = buttonLeft;
                buttonTop = m_Buttons[r, c - 1].Bottom;
            }

            this.Controls.AddRange(new Control[] { m_LabelPlayer1, m_LabelPlayer1Result, m_LabelPlayer2, m_LabelPlayer2Result, m_LabelCurrentPawnPlayer1, m_LabelCurrentPawnPlayer2 });
        }

        private bool isValidClick(CheckersPawnButton i_Button)
        {
            return (m_Gameplay.PlayerTurn && m_Gameplay.IsPlayerPawn(1, i_Button.Text)) || (!m_Gameplay.PlayerTurn && m_Gameplay.IsPlayerPawn(2, i_Button.Text));
        }

        private void tickButtonWithColor(CheckersPawnButton i_Button)
        {
            m_Clicked = true;
            i_Button.BackColor = Color.SkyBlue;
            i_Button.Click -= new EventHandler(buttonFirstClick_Click);
            i_Button.Click += new EventHandler(buttonSecondClick_Click);
        }

        private void showCurrentPlayerTurn()
        {
            if (m_Gameplay.PlayerTurn)
            {
                m_LabelPlayer1.ForeColor = Color.Red;
                m_LabelPlayer2.ForeColor = Color.White;
                m_LabelCurrentPawnPlayer1.ForeColor = Color.Red;
                m_LabelCurrentPawnPlayer2.ForeColor = Color.White;
            }
            else
            {
                m_LabelPlayer2.ForeColor = Color.Red;
                m_LabelPlayer1.ForeColor = Color.White;
                m_LabelCurrentPawnPlayer1.ForeColor = Color.White;
                m_LabelCurrentPawnPlayer2.ForeColor = Color.Red;
            }
        }

        private void buttonMoveClick(CheckersPawnButton i_Button)
        {
            m_Gameplay.Movement(m_Board, m_SourceButton.Row, m_SourceButton.Col, i_Button.Row, i_Button.Col);
            disableTickButtonColor(m_SourceButton);
            updateButtonGameBoard();
        }

        private bool checkAdditionalMoves(CheckersPawnButton i_Button)
        {
            return m_Gameplay.IsAdditionalMovement(m_Board, i_Button.Row, i_Button.Col, Math.Abs(i_Button.Row - m_SourceButton.Row));
        }

        private bool isComputerTurn()
        {
            return !m_Gameplay.PlayerTurn && m_GameProperties.Player2.IsComputer;
        }

        private void createComputerMove()
        {
            Movement move;
            int rowSource;
            int colSource;
            int rowDestination;
            int colDestination;

            do
            {
                move = m_Gameplay.GetComputerMovement(m_Board);
                rowSource = move.RowSourceMove;
                colSource = move.ColSourceMove;
                rowDestination = move.RowDestMove;
                colDestination = move.ColDestMove;
                m_SourceButton = m_Buttons[rowSource, colSource];
                m_Gameplay.Movement(m_Board, rowSource, colSource, rowDestination, colDestination);
                updateButtonGameBoard();
            }
            while (checkAdditionalMoves(m_Buttons[rowDestination, colDestination]));
        }

        private bool checkGameOver()
        {
            string winningPawn;
            bool isGameOver = true;

            if (m_Gameplay.IsGameOver(m_Board, false, out winningPawn))
            {
                if (winningPawn == m_Solider.SoliderX)
                {
                    m_GameProperties.Player1.Result = m_Gameplay.CalculateResult(m_Board, winningPawn, false);
                    DialogResult = MessageBox.Show(string.Format("{0} Won! 🥇 \nOne more game?", m_GameProperties.Player1.Name), "Checkers", MessageBoxButtons.YesNo);
                }
                else if (winningPawn == m_Solider.SoliderO)
                {
                    m_GameProperties.Player2.Result = m_Gameplay.CalculateResult(m_Board, winningPawn, false);
                    DialogResult = MessageBox.Show(string.Format("{0} Won! 🥇 \nOne more game?", m_GameProperties.Player2.Name), "Checkers", MessageBoxButtons.YesNo);
                }

                this.Close();
            }
            else if (m_Gameplay.CheckIfDraw(m_Board))
            {
                DialogResult = MessageBox.Show(string.Format("Tie!\nAnother Round?", m_GameProperties.Player2.Name), "Checkers", MessageBoxButtons.YesNo);
            }
            else
            {
                isGameOver = false;
            }

            return isGameOver;
        }

        private void updateButtonGameBoard()
        {
            int countPawnPlayer1 = 0, countPawnPlayer2 = 0;
            for (int r = 0; r < m_GameProperties.BoardSize; r++)
            {
                for (int c = 0; c < m_GameProperties.BoardSize; c++)
                {
                    m_Buttons[r, c].Text = m_Board.GetBoard[r, c];
                    m_Buttons[r, c].SetButtonImage(m_Board.GetBoard[r, c]);
                    if (m_Board.GetBoard[r, c] == m_Solider.SoliderX || m_Board.GetBoard[r, c] == m_Solider.SoldierK)
                    {
                        countPawnPlayer1++;
                    }

                    if (m_Board.GetBoard[r, c] == m_Solider.SoliderO || m_Board.GetBoard[r, c] == m_Solider.SoliderU)
                    {
                        countPawnPlayer2++;
                    }
                }
            }

            m_LabelCurrentPawnPlayer1.Text = m_GameProperties.Player1.Name + " | Current Pawn in board = " + countPawnPlayer1;
            m_LabelCurrentPawnPlayer2.Text = m_GameProperties.Player2.Name + " | Current Pawn in board = " + countPawnPlayer2;
        }

        private void buttonFirstClick_Click(object sender, EventArgs e)
        {
            CheckersPawnButton button = sender as CheckersPawnButton;
            bool isGameOver;

            if (!m_Clicked)
            {
                if (isValidClick(button))
                {
                    m_SourceButton = button;
                    tickButtonWithColor(button);
                }
                else
                {
                    if (button.Text == m_Solider.Space)
                    {
                        MessageBox.Show("Invalid Move ! ", "Error");
                    }
                    else
                    {
                        MessageBox.Show("Opponent's Pawn ! ", "Error");
                    }
                }
            }
            else
            {
                if (m_Gameplay.IsValidMovement(m_Board, m_SourceButton.Row, m_SourceButton.Col, button.Row, button.Col))
                {
                    buttonMoveClick(button);
                    if (!checkAdditionalMoves(button))
                    {
                        m_Gameplay.ChangeTurnOfPlayer();
                        showCurrentPlayerTurn();
                        isGameOver = checkGameOver();
                        if (isComputerTurn() && !isGameOver)
                        {
                            createComputerMove();
                            m_Gameplay.ChangeTurnOfPlayer();
                            showCurrentPlayerTurn();
                            isGameOver = checkGameOver();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Move!", "Error");
                }
            }
        }

        private void disableTickButtonColor(CheckersPawnButton i_Button)
        {
            m_Clicked = false;
            i_Button.BackColor = Color.White;
            i_Button.Click += new EventHandler(buttonFirstClick_Click);
            i_Button.Click -= new EventHandler(buttonSecondClick_Click);
        }

        private void buttonSecondClick_Click(object sender, EventArgs e)
        {
            CheckersPawnButton button = sender as CheckersPawnButton;

            if (m_Clicked)
            {
                if (isValidClick(button))
                {
                    m_SourceButton = null;
                    disableTickButtonColor(button);
                }
            }
        }
    }
}