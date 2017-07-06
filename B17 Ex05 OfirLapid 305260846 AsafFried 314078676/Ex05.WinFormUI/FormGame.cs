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
        private const int k_GameFormWidth = 300;
		private const int k_GameFormInitialHeight = 125;
		private const int k_GameFormRowHeight = 47;
        private FormLogin m_LoginForm;
        private FormColorChoice m_ColorChoiceForm;
        private List<GuessRow> m_GameRows;
        private int m_SelectedNumberOfChances;
        private SolutionRow m_SolutionRow; 

        public FormGame()
        {
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MaximizeBox = false;
			this.Text = "Bool Pgia";
            m_LoginForm = new FormLogin();
            m_ColorChoiceForm = new FormColorChoice();

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
            const int k_RowLeft = 12;
            const int k_GuessRowTop = 80;
            const int k_SolutionRowTop = 10;
            m_Game = new Game(m_SelectedNumberOfChances);
            SolutionRow solution = new SolutionRow(new Point(k_RowLeft, k_SolutionRowTop), 0);

            m_SolutionRow = solution;
            m_SolutionRow.SetEnableOfColorButtons(false);
            this.Controls.AddRange(solution.GetControls());
            for (int i = 0; i < m_SelectedNumberOfChances; i++)
            {
                GuessRow guess = new GuessRow(new Point(k_RowLeft, k_GuessRowTop), i);
                makeGuessRowForTheGame(guess, i, k_IndexOfFirstRow);
                m_GameRows.Add(guess);
                this.Controls.AddRange(guess.GetControls());
            }
        }

        private void makeGuessRowForTheGame(GuessRow guess , int i_indexOfTheRow, int i_indexOfFirstRow)
        {
            if (i_indexOfTheRow == i_indexOfFirstRow)
            {
                guess.SetEnableOfColorButtons(true);
            }

            else
            {
                guess.SetEnableOfColorButtons(false);
            }

            guess.AfterMakeGuess += doWhenApplyButtonIsClicked;
            guess.AfterGuessColorButtonClick += setGuessButtonFromColorForm;
        }

        private void doWhenApplyButtonIsClicked(string i_Guess, GuessRow io_GuessRow)
        {
            m_Game.PlayTurn(i_Guess);
            Game.eGuessResult[] resultOfTheCurrentTurn = m_Game.getLastGameResult();
            Game.eGameResult gameResult = m_Game.GameResult;
            io_GuessRow.interpretResult(resultOfTheCurrentTurn);

            if (gameResult.Equals(Game.eGameResult.Win))
            {
                finishGame(io_GuessRow.ColorButtons);
            }
            else if(gameResult.Equals(Game.eGameResult.StillPlaying))
            {
                m_GameRows[io_GuessRow.IndexOfRow + 1].SetEnableOfColorButtons(true);
               
            }

            io_GuessRow.SetEnableOfColorButtons(false);
        }

        private void finishGame(List<ColorButton> lastGuess)
		{
            foreach (Control control in this.Controls)
            {
                if(control is Button)
                {
                    control.Enabled = false;
                }
            }

            showSoultionRow(lastGuess);
		}

        private void showSoultionRow(List<ColorButton> lastGuess)
        {
            int counter = 0;
            Control[] controls = m_SolutionRow.GetControls();
            foreach (ColorButton guess in lastGuess)
            {
                (controls[counter] as ColorButton).BackColor = guess.BackColor;
                counter++;
            }
        }

        private void setGuessButtonFromColorForm(GuessColorButton sender)
        {
            m_ColorChoiceForm.ShowDialog();
			if (m_ColorChoiceForm.UserChoiceOfColor != null && m_ColorChoiceForm.DialogResult == DialogResult.OK)
			{
                sender.BackColor = m_ColorChoiceForm.UserChoiceOfColor.Value;
                sender.ValueOfTheGuessInStringFormat = m_ColorChoiceForm.UserChoiceValue.Value;
			}
        }

        void enableGuessRow(int i_IndexOfRow)
        {
            m_GameRows[i_IndexOfRow].SetEnableOfColorButtons(true);
        }

		private class SolutionRow : Row
		{
            public SolutionRow(Point i_Location, int i_PivotTop) : base(i_Location, i_PivotTop, Color.Black)
			{
                
			}

			public override Control[] GetControls()
			{
				return m_ColorButtons.ToArray();
			}
		}

        private class GuessRow : Row
        {
            public delegate void MakeGuessDelegate(string i_GuessOfTheUser, GuessRow i_GuessRow);
			public delegate void GuessColorButtonClickDelegate(GuessColorButton sender);
            public event GuessColorButtonClickDelegate AfterGuessColorButtonClick;
            public event MakeGuessDelegate AfterMakeGuess;
			private int m_IndexOfRow;
            private Button m_ApplyGuessButton;
            private const int k_ApplyButtonXPivot = 10;
            private const int k_ApplyButtonYPivot = 10;
			private const int k_AnswerBoxXPivot = 60;
			private const int k_AnswerBoxYPivot = 0;
            private List<Button> m_AnswersBoxes;

            public int IndexOfRow
            {
                get
                {
                    return m_IndexOfRow;
                }
            }

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
            
            private bool checkValidGuess()
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

			private void ButtonColor_Click(object sender, EventArgs e)
			{
				
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

			private void ButtonApply_Click(object sender, EventArgs e)
			{                
                String guessOfTheUser = CovertColoredButtonsToStringGuessRepresentation();
                if(AfterGuessColorButtonClick != null)
                {
                    AfterMakeGuess.Invoke(guessOfTheUser, this);
                }

                m_ApplyGuessButton.Enabled = false;
			}

            public void interpretResult(Game.eGuessResult[] i_GameResult)
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
        }
    }
}
