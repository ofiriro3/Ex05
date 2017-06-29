using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Ex05.WinFormUI
{
    public class FormColorChoice : Form
    {
        Dictionary<String,Button> m_ColorButtons; 

        public FormColorChoice()
        {
            this.Text = "Pick A Color:";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new System.Drawing.Size(300, 150);
            m_ColorButtons = new Dictionary<string, Button>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            createColoredButton(Color.Purple);
            createColoredButton(Color.Red);
            createColoredButton(Color.Green);
            createColoredButton(Color.WhiteSmoke);
            createColoredButton(Color.Blue);
            createColoredButton(Color.Yellow);
            createColoredButton(Color.Brown);
            createColoredButton(Color.White);
            foreach(Button button in m_ColorButtons.Values)
            {
                this.Controls.Add(button);
            }
        }

        private void createColoredButton(Color i_Color)
        {
            Button coloredButton = new Button();
            coloredButton.BackColor = i_Color;
            m_ColorButtons.Add(i_Color.ToString(), coloredButton);
        }
    }
}
