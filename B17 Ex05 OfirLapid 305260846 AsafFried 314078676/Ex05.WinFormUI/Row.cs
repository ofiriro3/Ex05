using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace Ex05.WinFormUI
{
	abstract class Row
	{
        protected const int k_ColorButtonSpacing = 8;
		protected const int k_NumberOfColorBoxes = 4;
		protected List<ColorButton> m_ColorButtons;

		public Row(Point i_Location, int i_PivotTop, Color i_DefualtColorOfColorButtons)
		{
			Point pivotedPoint = new Point(i_Location.X, i_Location.Y + (i_PivotTop * (ColorButton.k_Height + k_ColorButtonSpacing)));
			createButtons(pivotedPoint, i_DefualtColorOfColorButtons);
		}

		private void createButtons(Point i_Location, Color i_Color)
		{
			m_ColorButtons = new List<ColorButton>(k_NumberOfColorBoxes);
			for (int i = 0; i < k_NumberOfColorBoxes; i++)
			{
				m_ColorButtons.Add(CreateButton(i_Color, new Point(i_Location.X, i_Location.Y), i));
			}
		}

		protected virtual ColorButton CreateButton(Color i_Color, Point i_Location, int i_PivotLeft)
		{
			ColorButton colorButton = new ColorButton(i_Color);

			colorButton.Location = i_Location;
			colorButton.Left += i_PivotLeft * (ColorButton.k_Width + k_ColorButtonSpacing);

			return colorButton;
		}

		public void SetEnableOfColorButtons(bool i_Enable)
		{
			foreach (Button button in m_ColorButtons)
			{
				button.Enabled = i_Enable;
			}
		}

		public abstract Control[] GetControls();
	}
}
