using System;
using System.Windows.Forms;
using System.Drawing;
namespace Ex05.WinFormUI
{
    public class GuessColorButton : ColorButton
    {
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
			Control = 'a'
		}

		public GuessColorButton(eColor i_Color): base()
        {
			this.BackColor = Color.FromName(Enum.GetName(typeof(eColor), i_Color));
			this.m_ValueOfTheGuessInCharFormat = (char)i_Color;
		}

        public GuessColorButton(Color i_Color) : base(i_Color)
        {
			eColor color = (eColor)Enum.Parse(typeof(eColor), i_Color.Name);

			this.BackColor = i_Color;
			this.m_ValueOfTheGuessInCharFormat = (char)color;
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
    }
}
