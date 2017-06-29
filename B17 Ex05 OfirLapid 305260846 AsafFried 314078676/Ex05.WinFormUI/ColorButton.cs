using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    public class ColorButton : Button
    {
        public const int k_Height = 40;
        public const int k_Width = 40;

        public ColorButton(Color i_Color): base()
        {
            this.BackColor = i_Color;
            this.Width = k_Width;
            this.Height = k_Height;
        }
    }
}
