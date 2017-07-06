using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05.BullsEyeLogic
{
    public class Game
    {
        private readonly int r_LengthOfGuess = 4;
        private int m_NumberOfTotalGuesses;
        private Turn[] m_TurnArray;
        private int m_NumOfLeftGuesses;
        private eGameResult m_GameResult;
        private eGuessLetter[] m_ComputerAnswer;
        private string m_ComputerAnswerStringFormat;

        public enum eGuessLetter
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }

        public static class eGuessLetterMethods
        {
            public static eGuessLetter ConvertCharToEGuessLetter(char i_Letter)
            {
                eGuessLetter theConvertedLetter = eGuessLetter.A;
                switch (i_Letter)
                {
                    case 'A': theConvertedLetter = eGuessLetter.A;
                        break;
                    case 'B':
                        theConvertedLetter = eGuessLetter.B;
                        break;
                    case 'C':
                        theConvertedLetter = eGuessLetter.C;
                        break;
                    case 'D':
                        theConvertedLetter = eGuessLetter.D;
                        break;
                    case 'E':
                        theConvertedLetter = eGuessLetter.E;
                        break;
                    case 'F':
                        theConvertedLetter = eGuessLetter.F;
                        break;
                    case 'G':
                        theConvertedLetter = eGuessLetter.G;
                        break;
                    case 'H':
                        theConvertedLetter = eGuessLetter.H;
                        break;
                }

                return theConvertedLetter;
            }
        }

        public enum eGuessResult
        {
            X,
            V,
            WrongGuess
        }

        public enum eGameResult
        {
            Win,
            Loss,
            Abort,
            StillPlaying
        }

        public Turn[] TurnsResult
        {
           get
           {
                return m_TurnArray;
           }
        }

        public eGuessResult[] getLastGameResult()
        {
            int lastTurn = m_NumberOfTotalGuesses - (m_NumOfLeftGuesses + 1);

            return m_TurnArray[lastTurn].GuessResults;
        }

        public Game(int i_NumberOfGuess)
        {
            if(!validGuessNumber(i_NumberOfGuess))
            {
                throw new ArgumentException();
            }

            m_NumberOfTotalGuesses = i_NumberOfGuess;
            m_NumOfLeftGuesses = i_NumberOfGuess;
            m_TurnArray = new Turn[i_NumberOfGuess];
            m_ComputerAnswer = StringToEGuessLetterArrayFormat(GenerateRandomSolution(r_LengthOfGuess));
        }

        private bool validGuessNumber(int i_NumberOfGuesses)
        {
            bool validGuess = (i_NumberOfGuesses > 3 && i_NumberOfGuesses < 11) ? true : false;

            return validGuess;
        }

        public eGameResult GameResult
        {
            get
            {
                return m_GameResult;
            }

            set
            {
                m_GameResult = value;
            }
        }

        public int GetNumOfGuessMade()
        {
            return m_NumberOfTotalGuesses - m_NumOfLeftGuesses + 1;
        }

        public void PlayTurn(string i_GuessFromUser)
        {
            if (m_NumOfLeftGuesses <= 0)
            {
                m_GameResult = eGameResult.Loss;
            }
            else if (!VerifyInputFromUser(i_GuessFromUser))
            {
                throw new ArgumentException();
            }
            else
            {
                m_GameResult = eGameResult.StillPlaying;
                eGuessLetter[] currentGuess = StringToEGuessLetterArrayFormat(i_GuessFromUser);
                Turn currentTurn = new Turn(m_ComputerAnswer, currentGuess);
                int cellToAddCurrentTurn = m_NumberOfTotalGuesses - m_NumOfLeftGuesses;
                m_TurnArray[cellToAddCurrentTurn] = currentTurn;
                m_NumOfLeftGuesses--;
                if (currentTurn.IsCorrect())
                {
                    m_GameResult = eGameResult.Win;
                }
                else if (m_NumOfLeftGuesses == 0)
                {
                    m_GameResult = eGameResult.Loss;
                }
            }
        }

        private eGuessLetter[] StringToEGuessLetterArrayFormat(string i_VerifiedInputString)
        {
            eGuessLetter[] guessArray = new eGuessLetter[r_LengthOfGuess];
            string inputWithoutSpaces = i_VerifiedInputString.Replace(" ", string.Empty);

            for(int i = 0; i < inputWithoutSpaces.Length; i++)
            {
                char currentLetter = inputWithoutSpaces[i];
                guessArray[i] = eGuessLetterMethods.ConvertCharToEGuessLetter(currentLetter);
            }

            return guessArray;
        }

        private bool VerifyInputFromUser(string i_InputFromUser)
        {
               bool isValidInputFromUser = checkValidInputContext(i_InputFromUser) && checkValidInputFormat(i_InputFromUser);

               return isValidInputFromUser;
        }

        private bool checkValidInputFormat(string i_InputFromUser)
        {
            bool validFormat = true;
            string inputWithoutSpaces = i_InputFromUser.Replace(" ", string.Empty);
            if (inputWithoutSpaces.Length == 4 )
            {
                foreach (char currentLetter in inputWithoutSpaces)
                {
                    if (currentLetter < 'A' || currentLetter > 'Z')
                    {
                        validFormat = false;
                        break;
                    }
                }
            }
            
            return validFormat;
        }
    
        private bool checkValidInputContext(string i_InputFromUser)
        {
            string inputWithoutSpaces = i_InputFromUser.Replace(" ", string.Empty);
            bool validContext = true;
            foreach (char currentLetter in inputWithoutSpaces)
            {
                    if (currentLetter < 'A' || currentLetter > 'Z')
                    {
                        validContext = false;
                        break;
                    }
            }
    
            return validContext;
        }

        public string GenerateRandomSolution(int i_lengthOfSolution)
        {
            Random random = new Random();
            StringBuilder solution = new StringBuilder();
            int amountOfEnumOptions = Enum.GetNames(typeof(eGuessLetter)).Length;

            for (int i = 0; i < i_lengthOfSolution; i++)
            {
                int randomNumber = random.Next(amountOfEnumOptions);
                char currentChar = (char)('A' + randomNumber);

                ////checks if the string already contains the current letter
                if (solution.ToString().Contains(char.ToString(currentChar)))    
                {
                    i--;
                }
                else
                { 
                    solution.Append(currentChar);
                }
            }

            m_ComputerAnswerStringFormat = solution.ToString();

            return solution.ToString();
        }
    }
}
