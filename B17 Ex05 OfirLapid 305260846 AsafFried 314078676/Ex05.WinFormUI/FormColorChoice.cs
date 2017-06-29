using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Ex05.WinFormUI
{
    public class FormColorChoice : Form
    {


        public FormColorChoice()
        {
            this.Text = "Pick A Color:";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Size = new System.Drawing.Size(150, 150);


        }


    }
}
