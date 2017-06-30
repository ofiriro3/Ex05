﻿using System;
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
				return m_ColorButtons.ToArray();
			}
		}

        class GuessRow : Row
        {

            private Button m_ApplyGuessButton;
            private const int k_ApplyButtonXPivot = 10;
            private const int k_ApplyButtonYPivot = 10;
			private const int k_AnswerBoxXPivot = 60; //TODO : USE CONST
			private const int k_AnswerBoxYPivot = 0;
            private List<Button> m_AnswersBoxes;

            public GuessRow(Point i_Location, int i_PivotTop) : base(i_Location, i_PivotTop, Color.Gray)
            {
                int locationY = i_Location.Y + i_PivotTop * (ColorButton.k_Height + k_ColorButtonSpacing);
                int locationX = k_NumberOfColorBoxes * (ColorButton.k_Width + k_ColorButtonSpacing);

                createApplyGuessButton(locationX, locationY);
                createAnswersBoxes(locationX, locationY);

            }

            private void createAnswersBoxes(int XValue, int YValue)
            {
                const int k_ButtonWidth = 15;
                const int k_ButtonHeight = 15;
                const int k_Pivot = 3;
                m_AnswersBoxes = new List<Button>();
                int locationY = YValue + k_AnswerBoxYPivot - k_ButtonWidth;
                int locationX = XValue + k_AnswerBoxXPivot;

                for (int i = 0; i < k_NumberOfColorBoxes; i++)
                    {
                        Button button = new Button();
                        button.Width = k_ButtonWidth;
                        button.Height = k_ButtonHeight;
                        if (i % 2 == 0)
                        {
                            locationX = XValue + k_AnswerBoxXPivot;
                            locationY += button.Height + k_Pivot;
                        }

                        button.Location = new Point(locationX, locationY);
                        locationX += button.Width + k_Pivot;
                        m_AnswersBoxes.Add(button);
                     }
            }

            private void createApplyGuessButton(int XValue, int YValue)
            {
                m_ApplyGuessButton = new Button();
				m_ApplyGuessButton.Width = 40; //TODO : ADD const
				m_ApplyGuessButton.Height = 20;
				m_ApplyGuessButton.Text = "-->>";
                int locationY = YValue + k_ApplyButtonYPivot;
                int locationX = XValue + k_ApplyButtonXPivot;
                m_ApplyGuessButton.Location = new Point(locationX, locationY);
                m_ApplyGuessButton.Enabled = false;
                m_ApplyGuessButton.Click += new EventHandler(ButtonApply_Click);
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
                List<Control> controls = new List<Control>();

                foreach (ColorButton button in m_ColorButtons){
                    controls.Add(button);
                }

                foreach (Button button in m_AnswersBoxes)
                {
                    controls.Add(button);
                }


                controls.Add(m_ApplyGuessButton);
                
                return controls.ToArray();
            }

            bool checkValidGuess()
            {
                bool validGuess = true;

                foreach(ColorButton button in m_ColorButtons)
                {
                    if(button.BackColor.Equals(Color.Gray))
                    {
                        validGuess = false;
                        break;
                    }
                }

                return validGuess;
            }

			void ButtonColor_Click(object sender, EventArgs e)
			{

                m_ColorChoiceForm.ShowDialog();
				if (m_ColorChoiceForm.UserChoiceOfColor != null)
				{
					(sender as Button).BackColor = m_ColorChoiceForm.UserChoiceOfColor.Value;
				}

				bool validGuess = checkValidGuess();

                if(validGuess)
                {
                    m_ApplyGuessButton.Enabled = true;
                }
                 
			}

			void ButtonApply_Click(object sender, EventArgs e)
			{
                
			}
        }
    }
}
