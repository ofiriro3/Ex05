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
        private char m_ValueOfTheGuessInCharFormat;

        public enum eColor
        {
            Purple = 'A',
            Red = 'B',
            Green = 'C',
            Turquoise = 'D',
            Blue = 'E',
            Yellow = 'F',
            Brown = 'G',
            White = 'H',
            Black = 'b',
            Gray = 'a'
        }

        public char ValueOfTheGuessInStringFormat
        {
            get
            {
                return m_ValueOfTheGuessInCharFormat;
            }
            set
            {
                m_ValueOfTheGuessInCharFormat = value;
            }
        }
        public ColorButton(eColor i_Color): base()
        {
            this.BackColor = Color.FromName(Enum.GetName(typeof(eColor),i_Color));
            this.m_ValueOfTheGuessInCharFormat = (char)i_Color;
            this.Width = k_Width;
            this.Height = k_Height;
        }

        public ColorButton(Color i_Color) : base()
        {
            eColor color =(eColor) Enum.Parse(typeof(eColor), i_Color.Name);

            this.BackColor = i_Color;
            this.m_ValueOfTheGuessInCharFormat = (char) color;
            this.Width = k_Width;
            this.Height = k_Height;
        }

        
    }
}
