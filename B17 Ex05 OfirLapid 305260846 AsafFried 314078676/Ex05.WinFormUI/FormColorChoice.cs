using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static Ex05.WinFormUI.GuessColorButton;

namespace Ex05.WinFormUI
{
    public class FormColorChoice : Form
    {
        private const int k_NumberOfButtonInARow = 4;
        private const int k_NumberOfButtonInAColumn = 2;
        private Dictionary<string, GuessColorButton> m_ColorButtons;
        private Nullable<Color> m_UserChocieOfColor;
        private Nullable<char> m_UserChoiceValue;

        public Nullable<char> UserChoiceValue
        {
            get
            {
                return m_UserChoiceValue;
            }
        }

        public Nullable<Color> UserChoiceOfColor
        {
            get
            {
                return m_UserChocieOfColor;
            }
        }

        public FormColorChoice()
        {
            this.Text = "Pick A Color:";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new System.Drawing.Size(200, 150);
            m_ColorButtons = new Dictionary<string, GuessColorButton>();
            InitControls();
        }

        private void InitControls()
        {
            eColor defaultColor = eColor.Control;

            foreach(eColor color in Enum.GetValues(typeof(eColor)))
            {
                if(!color.Equals(defaultColor))
                {
                    createGuessColoredButton(color);
                }
            }

            int xPosition = (this.ClientSize.Width - (k_NumberOfButtonInARow * ColorButton.k_Width)) / 2;
            int yPosition = (this.ClientSize.Height - (k_NumberOfButtonInAColumn * ColorButton.k_Height)) / 2;
            int numOfButtonInCurrentRow = 0;

            foreach (ColorButton button in m_ColorButtons.Values)
            {
                this.Controls.Add(button);
                button.Location = new Point(xPosition, yPosition);
                button.Click += new EventHandler(OnClickColoredButton);
                xPosition += button.Width + 2;
                numOfButtonInCurrentRow++;

                if (numOfButtonInCurrentRow >= k_NumberOfButtonInARow)
                {
                    xPosition = (this.ClientSize.Width - (k_NumberOfButtonInARow * 40)) / 2;
                    yPosition += button.Height + 2;
                    numOfButtonInCurrentRow = 0;
                }   
            }
        }

        private void OnClickColoredButton(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            GuessColorButton button = sender as GuessColorButton;
            if(button != null)
            {
                m_UserChocieOfColor = button.BackColor;
                m_UserChoiceValue = button.ValueOfTheGuessInStringFormat;
                this.Close();
            } 
        }

        private void createGuessColoredButton(eColor i_Color)
        {
            GuessColorButton coloredButton = new GuessColorButton(i_Color);
            m_ColorButtons.Add(Enum.GetName(typeof(eColor), i_Color), coloredButton);
        }
    }
}
