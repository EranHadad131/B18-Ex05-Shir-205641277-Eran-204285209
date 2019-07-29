using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Checkers
{
    public class Player
    {
        private string m_PlayerName;
        private int m_Result;
        private bool m_IsComputer;
        private int m_CurrentPawn;

        public Player(string i_Name)
        {
            m_Result = 0;
            if (i_Name == "[Computer]")
            {
                m_IsComputer = true;
                m_PlayerName = "Computer";
            }
            else
            {
                m_IsComputer = false;
                m_PlayerName = i_Name;
            }
        }

        public Player()
        {
            m_PlayerName = "Computer";
            m_Result = 0;
            m_IsComputer = true;
        }

        public string Name
        {
            get
            {
                return m_PlayerName;
            }
        }

        public int Result
        {
            get
            {
                return m_Result;
            }

            set
            {
                m_Result += value;
            }
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }

        public int CurrentPawn
        {
            get
            {
                return m_CurrentPawn;
            }

            set
            {
                m_CurrentPawn = value;
            }
        }
    }
}
