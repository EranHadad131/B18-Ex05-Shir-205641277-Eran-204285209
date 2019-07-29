using System.Windows.Forms;
using System.Media;

namespace Ex05_Checkers
{
    public static class Program
    {
        public static void Main()
        {
            UserInterface UserInterface = new UserInterface();
            GameBoardForm gameBoardForm;
            SoundPlayer music = new SoundPlayer(Properties.Resources.GameofThrones);
            music.Play();
            UserInterface.ShowDialog();

            if (UserInterface.DialogResult == DialogResult.OK)
            {
                do
                {
                    gameBoardForm = new GameBoardForm(UserInterface);
                    gameBoardForm.ShowDialog();
                }
                while (gameBoardForm.DialogResult == DialogResult.Yes);
            }

            music.Stop();
        }
    }
}