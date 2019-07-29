using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Checkers
{
    public class Movement
    {
        private int m_RowSourceMove;
        private int m_ColSourceMove;
        private int m_RowDestMove;
        private int m_ColDestMove;

        public Movement(int i_RowPrevMove, int i_ColPrevMove, int i_RowDestMove, int i_ColDestMove)
        {
            m_RowSourceMove = i_RowPrevMove;
            m_ColSourceMove = i_ColPrevMove;
            m_RowDestMove = i_RowDestMove;
            m_ColDestMove = i_ColDestMove;
        }

        public int RowSourceMove
        {
            get
            {
                return m_RowSourceMove;
            }
        }

        public int ColSourceMove
        {
            get
            {
                return m_ColSourceMove;
            }
        }

        public int RowDestMove
        {
            get
            {
                return m_RowDestMove;
            }
        }

        public int ColDestMove
        {
            get
            {
                return m_ColDestMove;
            }
        }
    }
}
