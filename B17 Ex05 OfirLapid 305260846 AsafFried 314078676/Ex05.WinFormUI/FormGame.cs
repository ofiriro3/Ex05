﻿﻿﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Ex05.BullsEyeLogic;
namespace Ex05.WinFormUI
{
    public class FormGame : Form
    {
       
        private Game m_Game;
        private const int k_ColorButtonSpacing = 8;
        private const int k_GameFormWidth = 300;
		private const int k_GameFormInitialHeight = 125;
		private const int k_GameFormRowHeight = 47;
		private FormLogin m_LoginForm = new FormLogin();
        private FormColorChoice m_ColorChoiceForm = new FormColorChoice();
        private List<GuessRow> m_GameRows;
        private int m_SelectedNumberOfChances;
        private SolutionRow m_SolutionRow; 

        public FormGame()
        {
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.Text = "Bool Pgia";
        }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if (m_LoginForm.ShowDialog() != DialogResult.OK)
			{
				this.Close();
			}

			else
			{
				m_SelectedNumberOfChances = m_LoginForm.SelectedNumberOfChances;
                this.Size = new Size(k_GameFormWidth, k_GameFormInitialHeight + k_GameFormRowHeight * m_SelectedNumberOfChances);
				m_GameRows = new List<GuessRow>(m_SelectedNumberOfChances);
				runGame();
			}
		}

        private void runGame()
        {
            const int k_IndexOfFirstRow = 0;
            m_Game = new Game(m_SelectedNumberOfChances);
            const int k_RowLeft = 12;
            const int k_GuessRowTop = 80;
            const int k_SolutionRowTop = 10;

            SolutionRow solution = new SolutionRow(new Point(k_RowLeft, k_SolutionRowTop), 0);
            m_SolutionRow = solution;
            this.Controls.AddRange(solution.GetControls());

            for (int i = 0; i < m_SelectedNumberOfChances; i ++)
            {
                GuessRow guess = new GuessRow(new Point(k_RowLeft, k_GuessRowTop), i);
                if (i == k_IndexOfFirstRow)
                {
                    guess.SetEnableOfRowGuessButtons(true); 
                }

                else
                {
                    guess.SetEnableOfRowGuessButtons(false);
				}

                guess.AfterSuccessfulGuess += FinishGame;
                guess.AfterMakeGuess += m_Game.PlayTurn;
                guess.WhenGetResultGuess += m_Game.getLastGameResult;
                guess.AfterGuessColorButtonClick += SetGuessButtonFromColorForm;
                guess.ValidateCorrect += IsWonTheGame;
                m_GameRows.Add(guess);
                this.Controls.AddRange(guess.GetControls());
            }

            for (int i = 0; i < m_GameRows.Count; i++)
            {
				if (i < m_SelectedNumberOfChances - 1)
				{
                    m_GameRows[i].AfterWrongGuess += EnableGuessRow;
				}
            }
        }

        public bool IsWonTheGame()
        {
            return m_Game.GameResult.Equals(Game.eGameResult.Win);
        }

        public void FinishGame(List<ColorButton> lastGuess)
		{
            foreach (Control control in this.Controls)
            {
                if(control is Button)
                {
                    control.Enabled = false;
                }
            }

            int counter = 0;
            Control[] controls = m_SolutionRow.GetControls();
            foreach(ColorButton guess in lastGuess)
            {
                (controls[counter] as ColorButton).BackColor = guess.BackColor;
                counter++;
            } 
		}


        public void SetGuessButtonFromColorForm(GuessColorButton sender)
        {
            m_ColorChoiceForm.ShowDialog();

			if (m_ColorChoiceForm.UserChoiceOfColor != null && m_ColorChoiceForm.DialogResult == DialogResult.OK)
			{
                sender.BackColor = m_ColorChoiceForm.UserChoiceOfColor.Value;
                sender.ValueOfTheGuessInStringFormat = m_ColorChoiceForm.UserChoiceValue.Value;
			}
        }

        void EnableGuessRow(int i_IndexOfRow)
        {
            m_GameRows[i_IndexOfRow].SetEnableOfRowGuessButtons(true);
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
            public delegate void CorrectGuessDelegate(List<ColorButton> i_LastGuess);
            public delegate void WrongGuessDelegate(int i_IndexOfNextGuessRow);
            public delegate void MakeGuessDelegate(string i_GuessOfTheUser);
			public delegate Game.eGuessResult[] GetResultGuessDelegate();
            public delegate bool CheckIfCorrectGuessDelegate();
            public delegate void GuessColorButtonClickDelegate(GuessColorButton sender);
            public event CheckIfCorrectGuessDelegate ValidateCorrect;
            public event GuessColorButtonClickDelegate AfterGuessColorButtonClick;
            public event MakeGuessDelegate AfterMakeGuess;
			public event GetResultGuessDelegate WhenGetResultGuess;
            public event CorrectGuessDelegate AfterSuccessfulGuess;
            public event WrongGuessDelegate AfterWrongGuess;
            private int m_IndexOfRow;
            private Button m_ApplyGuessButton;
            private const int k_ApplyButtonXPivot = 10;
            private const int k_ApplyButtonYPivot = 10;
			private const int k_AnswerBoxXPivot = 60;
			private const int k_AnswerBoxYPivot = 0;
            private List<Button> m_AnswersBoxes;

            public GuessRow(Point i_Location, int i_Index) : base(i_Location, i_Index, Control.DefaultBackColor)
            {
                int locationY = i_Location.Y + i_Index * (ColorButton.k_Height + k_ColorButtonSpacing);
                int locationX = k_NumberOfColorBoxes * (ColorButton.k_Width + k_ColorButtonSpacing);
                m_IndexOfRow = i_Index;

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
                        button.Enabled = false;
                        m_AnswersBoxes.Add(button);
                     }
            }

            private void createApplyGuessButton(int XValue, int YValue)
            {
                m_ApplyGuessButton = new Button();
				m_ApplyGuessButton.Width = 40;
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
                GuessColorButton colorButton = new GuessColorButton(i_Color);

				colorButton.Location = i_Location;
				colorButton.Left += i_PivotLeft * (ColorButton.k_Width + k_ColorButtonSpacing);
                colorButton.Click += new EventHandler(ButtonColor_Click);

                return colorButton;
            }

            public override Control[] GetControls()
            {
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
                    if(button.BackColor.Equals(Control.DefaultBackColor))
                    {
                        validGuess = false;
                        break;
                    }
                }

                return validGuess;
            }

			void ButtonColor_Click(object sender, EventArgs e)
			{
				if ((sender as Button).Enabled == false)
				{
					return;
				}

                if (AfterGuessColorButtonClick != null)
				{
                    AfterGuessColorButtonClick.Invoke(sender as GuessColorButton);
				}

				bool validGuess = checkValidGuess();

                if(validGuess)
                {
                    m_ApplyGuessButton.Enabled = true;
                }
                 
			}

			void ButtonApply_Click(object sender, EventArgs e)
			{
                if((sender as Button).Enabled == false)
                {
                    return;
                }
                String guessOfTheUser = CovertColoredButtonsToStringGuessRepresentation();
                interpretResult(OnGuess(guessOfTheUser));
                if(ValidateCorrect != null)
                {
                    if (ValidateCorrect.Invoke())
					{
						OnSuccessfulGuess();
					}   
                }
                else
                {
                    SetEnableOfRowGuessButtons(false);
                    OnWrongGuess();
                }

                m_ApplyGuessButton.Enabled = false;
			}

            protected virtual Game.eGuessResult[] OnGuess(string i_GuessOfTheUser)
            {
                if (AfterMakeGuess != null)
				{
                    AfterMakeGuess.Invoke(i_GuessOfTheUser);
				}
                return WhenGetResultGuess.Invoke();
            }

			protected virtual void OnSuccessfulGuess()
			{
                if (AfterSuccessfulGuess != null)
				{
                    AfterSuccessfulGuess.Invoke(m_ColorButtons);
		        }
			}

			protected virtual void OnWrongGuess()
			{
                int indexOfNextGuessRow = m_IndexOfRow + 1;

				if (AfterWrongGuess != null)
				{
                    AfterWrongGuess.Invoke(indexOfNextGuessRow);
				}
			}

            private bool isWonTheGame()
            {
                bool wonTheGame = true;

                foreach (Button box in m_AnswersBoxes)
                {
                    if(!(box.BackColor == Color.Black))
                    {
                        wonTheGame = false;
                    }
                }

                return wonTheGame;
            }

            private void interpretResult(Game.eGuessResult[] i_GameResult)
            {
                int counter = 0;
                foreach (Button box in m_AnswersBoxes)
                {
                    if(i_GameResult[counter].Equals(Game.eGuessResult.V))
                    {
                        box.BackColor = Color.Black;
                    }
                    else if (i_GameResult[counter].Equals(Game.eGuessResult.X))
                    {
                        box.BackColor = Color.Yellow;
                    }

                    counter++;
                }
            }

            private String CovertColoredButtonsToStringGuessRepresentation()
            {

                StringBuilder guessOfTheUser = new StringBuilder();

                foreach (ColorButton guessButton in m_ColorButtons)
                {
                    guessOfTheUser.Append((guessButton as GuessColorButton).ValueOfTheGuessInStringFormat);
                }

                return guessOfTheUser.ToString();
            }

            public void SetEnableOfRowGuessButtons(bool i_Enable)
            {
				foreach (Button button in m_ColorButtons)
				{
                    button.Enabled = i_Enable;
				}
            }

        }
    }
}
