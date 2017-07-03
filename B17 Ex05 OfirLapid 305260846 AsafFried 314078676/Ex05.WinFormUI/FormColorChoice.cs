using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static Ex05.WinFormUI.ColorButton;

namespace Ex05.WinFormUI
{
    public class FormColorChoice : Form
    {
        Dictionary<String, ColorButton> m_ColorButtons;
        const int k_NumberOfButtonInARow = 4;
        const int k_NumberOfButtonInAColumn = 2;
        Nullable<Color> m_UserChocieOfColor = null;
        Nullable<char> m_UserChoiceValue;


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
            m_ColorButtons = new Dictionary<string, ColorButton>();
            InitControls();
        }

       /* protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        */
        private void InitControls()
        {
            foreach(eColor color in Enum.GetValues(typeof (eColor)))
            {
                createColoredButton(color);
            }

            int xPosition = (this.ClientSize.Width - k_NumberOfButtonInARow * ColorButton.k_Width) / 2;
            int yPosition = (this.ClientSize.Height - k_NumberOfButtonInAColumn * ColorButton.k_Height) / 2;
            int numOfButtonInCurrentRow = 0;

            foreach (Button button in m_ColorButtons.Values)
            {
                this.Controls.Add(button);
                button.Location = new Point(xPosition, yPosition);
                button.Click += new EventHandler(OnClickColoredButton);
                xPosition += button.Width + 2;
                numOfButtonInCurrentRow++;

                if (numOfButtonInCurrentRow >= k_NumberOfButtonInARow)
                {
                    xPosition = (this.ClientSize.Width - k_NumberOfButtonInARow * 40) / 2; ;
                    yPosition += button.Height + 2;
                    numOfButtonInCurrentRow = 0;
                }
                
            }
        }

        private void OnClickColoredButton(Object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            ColorButton button = sender as ColorButton;
            if( button != null)
            {
                m_UserChocieOfColor = button.BackColor;
                m_UserChoiceValue = button.ValueOfTheGuessInStringFormat;
                this.Close();
            }
            
        }

        private void createColoredButton(eColor i_Color)
        {
            ColorButton coloredButton = new ColorButton(i_Color);
            m_ColorButtons.Add(Enum.GetName(typeof(eColor),i_Color), coloredButton);
        }
    }
}
