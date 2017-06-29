using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    public class FormGame : Form
    {
        private const int k_ColorButtonSpacing = 8;
        FormLogin m_LoginForm = new FormLogin();
        static FormColorChoice m_ColorChoiceForm = new FormColorChoice();
        List<GuessRow> m_GameRows;
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
                m_GameRows = new List<GuessRow>(r_SelectedNumberOfChances);
				runGame();
			}
        }

        private void runGame()
        {
            const int k_RowLeft = 12;
            const int k_GuessRowTop = 80;
            const int k_SolutionRowTop = 10;

            SolutionRow solution = new SolutionRow(new Point(k_RowLeft, k_SolutionRowTop), 0);
            this.Controls.AddRange(solution.GetControls());

            for (int i = 0; i < r_SelectedNumberOfChances; i ++)
            {
                GuessRow guess = new GuessRow(new Point(k_RowLeft, k_GuessRowTop), i);
                m_GameRows.Add(guess);
                this.Controls.AddRange(guess.GetControls());
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


		abstract class Row
		{
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

            public abstract Control[] GetControls();
		}

		class SolutionRow : Row
		{
            public SolutionRow(Point i_Location, int i_PivotTop) : base(i_Location, i_PivotTop, Color.Black)
			{
                
			}

			public override Control[] GetControls()
			{
				//TODO: ADD ALL CONTROLS
				return m_ColorButtons.ToArray();
			}
		}

        class GuessRow : Row
        {

            public GuessRow(Point i_Location, int i_PivotTop) : base(i_Location, i_PivotTop, Color.Gray)
            {

            }

            protected override ColorButton CreateButton(Color i_Color, Point i_Location, int i_PivotLeft)
            {
                ColorButton colorButton = base.CreateButton(i_Color, i_Location, i_PivotLeft);

                colorButton.Click += new EventHandler(ButtonColor_Click);

                return colorButton;
            }

            public override Control[] GetControls()
            {
                //TODO: ADD ALL CONTROLS
                return m_ColorButtons.ToArray();
            }

			void ButtonColor_Click(object sender, EventArgs e)
			{
                m_ColorChoiceForm.ShowDialog();
				if (m_ColorChoiceForm.UserChoiceOfColor != null)
				{
					(sender as Button).BackColor = m_ColorChoiceForm.UserChoiceOfColor.Value;
				}
			}
        }
    }
}
