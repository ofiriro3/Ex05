using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    class FormGame : Form
    {
        FormLogin m_LoginForm = new FormLogin();
        List<Row> m_GameRows;
        private readonly int r_SelectedNumberOfChances;

        public FormGame()
        {
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.Text = "Bool Pgia";
			if (m_LoginForm.ShowDialog() != DialogResult.OK)
			{
				this.Close();
			}

            else
            {
                r_SelectedNumberOfChances = m_LoginForm.SelectedNumberOfChances;
                this.Size = new Size(350, 450 + 20 * r_SelectedNumberOfChances);
                m_GameRows = new List<Row>(r_SelectedNumberOfChances);
				runGame();
			}
        }

        private void runGame()
        {
            int spaceLeftOfRow = 10;
            int spaceTopOfRow = 10;

            for (int i = 0; i < r_SelectedNumberOfChances; i ++)
            {
                m_GameRows.Add(new Row(new Point(spaceLeftOfRow, spaceTopOfRow)), i);
            }
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			InitControls();
		}

		private void InitControls()
		{
			this.Controls.AddRange(new Control[] {});
		}

        class Row
        {
            private const int k_NumberOfColorBoxes = 4;
            private List<ColorButton> m_ColorButtons;

            public Row(Point i_Location, int i_PivotTop)
            {
                int space = 5;

                Point pivotedPoint = new Point(i_Location.X, i_Location.Y + (i_PivotTop * 40 + space));
                createButtons(pivotedPoint);
            }

            private void createButtons(Point i_Location)
            {
                m_ColorButtons = new List<ColorButton>(k_NumberOfColorBoxes);
                for (int i = 0; i < k_NumberOfColorBoxes; i++)
                {
                    m_ColorButtons.Add(createButton(Color.Gray, new Point(i_Location.X , i_Location.Y), i));
                }
            }

            private ColorButton createButton(Color i_Color, Point i_Location, int i_PivotLeft)
            {
                int space = 5;
                ColorButton colorButton = new ColorButton(i_Color);

                colorButton.Location = i_Location;
                colorButton.Left += i_PivotLeft * colorButton.Width + space;

                return colorButton;
            }

            public Control[] GetControls()
            {
                return null;
            }
        }
    }
}
