using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    public class GuessBoxes : Form
    {
        public GuessBoxes()
        {
            initMembers();
        }

        private void initMembers()
        {
            createApplyButton();
            createFourSquare();
        }

        private void createApplyButton()
        {
            Button applyButton = new Button();
            applyButton.Width = 40;
            applyButton.Height = 20;
            applyButton.Text = "-->>";
            applyButton.Location = new Point(30, 20);
            this.Controls.Add(applyButton);

        }
        private void createFourSquare()
        {
            List<Control> squaredButtons = new List<Control>();
            int x = 50;
            int y = 50;
            for (int i = 0; i < 4; i++)
            {
                Button button = new Button();
                button.Width = 15;
                button.Height = 15;
                if( i % 2 == 0)
                {
                    x = 50;
                    y += button.Height + 3;
                }

                button.Location = new Point(x, y);
                x += button.Width + 3;
                squaredButtons.Add(button);
                this.Controls.Add(button);
            }
           

        }
    }
}
