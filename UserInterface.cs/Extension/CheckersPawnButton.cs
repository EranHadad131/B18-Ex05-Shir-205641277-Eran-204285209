using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05_Checkers
{
    public class CheckersPawnButton : Button
    {
        private readonly Gameplay.Soldier m_Soldier;
        private readonly int r_BoardSize;
        private int m_Row;
        private int m_Col;
        private Image m_soldierButtonImage;

        public CheckersPawnButton(int i_BoardSize)
        {
            m_Soldier = new Gameplay.Soldier();
            r_BoardSize = i_BoardSize;
        }

        public void SetButtonImage(string m_soldier)
        {
            if (m_soldier == m_Soldier.SoliderX)
            {
                m_soldierButtonImage = new Bitmap(Properties.Resources.SoldierX);
                this.BackgroundImage = m_soldierButtonImage;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_soldier == m_Soldier.SoliderO)
            {
                m_soldierButtonImage = new Bitmap(Properties.Resources.SoldierO);
                this.BackgroundImage = m_soldierButtonImage;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_soldier == m_Soldier.SoldierK)
            {
                m_soldierButtonImage = new Bitmap(Properties.Resources.KingK);
                this.BackgroundImage = m_soldierButtonImage;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_soldier == m_Soldier.SoliderU)
            {
                m_soldierButtonImage = new Bitmap(Properties.Resources.KingU);
                this.BackgroundImage = m_soldierButtonImage;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (m_soldier == m_Soldier.Space)
            {
                this.BackgroundImage = null;
            }
        }

        public Image ButtonImage
        {
            get { return m_soldierButtonImage; }
        }

        public int Row
        {
            get { return m_Row; }
            set
            {
                    m_Row = value;
            }
        }

        public int Col
        {
            get { return m_Col; }
            set
            {
                    m_Col = value;
            }
        }
    }
}
