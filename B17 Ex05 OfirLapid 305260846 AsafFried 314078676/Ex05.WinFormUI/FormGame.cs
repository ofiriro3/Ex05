using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    class FormGame : Form
    {
        private const int k_ColorButtonSpacing = 8;
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
                this.Size = new Size(300, 200 + 40 * r_SelectedNumberOfChances);
                m_GameRows = new List<Row>(r_SelectedNumberOfChances);
				runGame();
			}
        }

        private void runGame()
        {
            const int k_RowLeft = 12;
            const int k_RowTop = 30;

            for (int i = 0; i < r_SelectedNumberOfChances; i ++)
            {
                Row row = new Row(new Point(k_RowLeft, k_RowTop), i);
                m_GameRows.Add(row);
                this.Controls.AddRange(row.GetControls());
            }
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			InitControls();
		}

		private void InitControls()
		{
			//TODO:
		}

        class Row
        {
            private const int k_NumberOfColorBoxes = 4;
            private List<ColorButton> m_ColorButtons;

            public Row(Point i_Location, int i_PivotTop)
            {
                Point pivotedPoint = new Point(i_Location.X, i_Location.Y + (i_PivotTop * (40 + k_ColorButtonSpacing))); //TODO: CHANGE 40 TO THE CONST HEIGHT OF COLOR BOX
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
                ColorButton colorButton = new ColorButton(i_Color);

                colorButton.Location = i_Location;
                colorButton.Left += i_PivotLeft * (40 + k_ColorButtonSpacing); //TODO: 40

                return colorButton;
            }

            public Control[] GetControls()
            {
                //TODO: ADD ALL CONTROLS
                return m_ColorButtons.ToArray();
            }
        }
    }
}
