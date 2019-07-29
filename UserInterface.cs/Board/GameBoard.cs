using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Checkers
{
    public class GameBoard
    {
        private readonly Gameplay.Soldier m_Soldier;
        private string[,] m_GameBoard;
        private int m_BoardSize;

        public GameBoard(int i_BoardSize)
        {
            m_Soldier = new Gameplay.Soldier();
            m_BoardSize = i_BoardSize;
            m_GameBoard = new string[i_BoardSize, i_BoardSize];
            initBoard();
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
        }

        public string[,] GetBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public static bool IsValidArea(int i_Row, int i_Col)
        {
            bool isValid = false;

            if ((i_Row % 2 == 0 && i_Col % 2 != 0) || (i_Row % 2 != 0 && i_Col % 2 == 0))
            {
                isValid = true;
            }

            return isValid;
        }

        public bool CheckPlayerZone(int i_Row, int i_PlayerNumber)
        {
            bool playerZone = false;

            if (i_Row > m_BoardSize / 2 && i_Row < m_BoardSize && i_PlayerNumber == 1)
            {
                playerZone = true;
            }
            else if (i_Row >= 0 && i_Row < (m_BoardSize / 2) - 1 && i_PlayerNumber == 2)
            {
                playerZone = true;
            }

            return playerZone;
        }

        private void initBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (IsValidArea(i, j) && CheckPlayerZone(i, 2))
                    {
                        m_GameBoard[i, j] = m_Soldier.SoliderO;
                    }
                    else if (IsValidArea(i, j) && CheckPlayerZone(i, 1))
                    {
                        m_GameBoard[i, j] = m_Soldier.SoliderX;
                    }
                    else
                    {
                        m_GameBoard[i, j] = m_Soldier.Space;
                    }
                }
            }
        }
    }
}
