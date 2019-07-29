using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05_Checkers
{
    public partial class UserInterface : Form
    {
        private int m_GameBoardSize = 6;
        private Player m_player1, m_player2;

        public UserInterface()
        {
            InitializeComponent();
            checkBoxPlayer2.Checked = false;
            radioButton6X6.Checked = true;
        }

        public Player Player1
        {
            get { return m_player1; }
        }

        public Player Player2
        {
            get { return m_player2; }
        }

        public int BoardSize
        {
            get { return m_GameBoardSize; }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            bool isValidInput = false;
            if (textBoxPlayer1.Text.Length != 0 && IsValidName(textBoxPlayer1.Text) && textBoxPlayer2.Text.Length != 0 && IsValidName(textBoxPlayer2.Text))
            {
                m_player1 = new Player(textBoxPlayer1.Text);
                m_player2 = new Player(textBoxPlayer2.Text);
                if (radioButton6X6.Checked == true)
                {
                    m_player1.CurrentPawn = 6;
                    m_player2.CurrentPawn = 6;
                }
                else if (radioButton8X8.Checked == true)
                {
                    m_player1.CurrentPawn = 12;
                    m_player2.CurrentPawn = 12;
                }
                else if (radioButton10X10.Checked == true)
                {
                    m_player1.CurrentPawn = 20;
                    m_player2.CurrentPawn = 20;
                }

                isValidInput = true;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Worng Input", "Error");
            }

            if (isValidInput)
            {
                MessageBox.Show("Good Luck to:" + Environment.NewLine + m_player1.Name + " and " + m_player2.Name, "Checkers");
                if (!m_player2.IsComputer)
                {
                    MessageBox.Show(m_player1.Name + " you are X" + Environment.NewLine + m_player2.Name + " you are O" + Environment.NewLine, "Checkers");
                }
                else
                {
                    MessageBox.Show(m_player1.Name + " you are X", "Checkers");
                }
            }
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Checked == true)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private bool IsValidName(string i_Name)
        {
            return !(i_Name.Length > 20 || i_Name.Contains(" "));
        }

        private void radioButton6X6_CheckedChanged(object sender, EventArgs e)
        {
            m_GameBoardSize = 6;
        }

        private void radioButton8X8_CheckedChanged(object sender, EventArgs e)
        {
            m_GameBoardSize = 8;
        }

        private void textBoxes_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxPlayer1.Text == string.Empty || this.textBoxPlayer2.Text == string.Empty)
            {
                    this.buttonDone.BackColor = Color.Red;
            }
            else
            {
                if (IsValidName(textBoxPlayer1.Text) && IsValidName(textBoxPlayer2.Text))
                {
                    this.buttonDone.BackColor = Color.Green;
                }
                else
                {
                    this.buttonDone.BackColor = Color.Red;
                }
            }
        }

        private void radioButton10X10_CheckedChanged(object sender, EventArgs e)
        {
            m_GameBoardSize = 10;
        }
    }
}
