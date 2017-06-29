using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05.WinFormUI
{
    public class ApplyButton : Button
    {


        public static String ConvertColorsToString(List<Color> i_ListOfColors)
        {
            StringBuilder colorsInStringFormat = new StringBuilder();

            foreach(Color color in i_ListOfColors)
            {
                colorsInStringFormat.Append(covertColorToChar(color));
            }

            return colorsInStringFormat.ToString();
        }

        public static char covertColorToChar(Color color)
        {
            char letterThatColorRepresent = 'z';

            switch (color.Name)
            {
                case "Purple": letterThatColorRepresent = 'a'; break;
                case "Red": letterThatColorRepresent = 'b'; break;
                case "Green": letterThatColorRepresent = 'c'; break;
                case "AliceBlue": letterThatColorRepresent = 'd'; break;
                case "Blue": letterThatColorRepresent = 'e'; break;
                case "Yellow": letterThatColorRepresent = 'f'; break;
                case "Brown": letterThatColorRepresent = 'g'; break;
                case "White": letterThatColorRepresent = 'h'; break;
            }

            return letterThatColorRepresent;
        }
    }
}
