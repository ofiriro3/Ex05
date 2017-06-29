using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ex05.WinFormUI
{
    public class FormColorChoice : Form
    {
        Dictionary<String,Button> colorButtons; 

        public FormColorChoice()
        {
            this.Text = "Pick A Color:";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new System.Drawing.Size(300, 150);
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {
            createColoredButton("Blue");

        }

        private void createColoredButton(string color)
        {
            
        }
    }
}
