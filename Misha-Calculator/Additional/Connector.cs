using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace Misha_Calculator.Additional
{
    /// <summary>
    /// stores usefull methods for calculator graphical interface
    /// </summary>
    public class Connector
    {
        private string TextPi = "\u03c0", // represents a pi
            TextSqrt = "\u221A"; // represents a sqrt
        /// <summary>
        /// initial text of the calculator
        /// </summary>
        private readonly string InitialText = "0";
        /// <summary>
        /// list of the arithmetical operations
        /// </summary>
        private List<char> ListOfOperations;
        /// <summary>
        /// a number of brackets
        /// </summary>
        private short OpenBrackets, CloseBrackets;
        /// <summary>
        /// a list of the separators
        /// </summary>
        private Stack<string> Separators;
        /// <summary>
        /// a flag of the calculation process
        /// </summary>
        private bool Calculated = false;
        /// <summary>
        /// a reference to the display
        /// </summary>
        private TextBox _display;
        /// <summary>
        /// a right button
        /// </summary>
        private Button _left_bracket;

        private Counter _counter;
        

        #region checking functions
        /// <summary>
        /// checks if symbol is simple opearation
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsSimpleOperation(char symbol) 
        {
            return ListOfOperations.Contains(symbol);
        }
        /// <summary>
        /// checkes if symbol is exclam
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsFactorial(char symbol)
        {
            return symbol == '!';
        }
        /// <summary>
        /// checkes if symbol is left bracket
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsLeftBracket(char symbol)
        {
            return symbol == '(';
        }
        /// <summary>
        /// checkes if symbol is left bracket
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsLeftBracket(string symbol)
        {
            return symbol == "(";
        }
        /// <summary>
        /// checkes if symbol is right bracket
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsRightBracket(char symbol)
        {
            return symbol == ')';
        }
        /// <summary>
        /// checkes if symbol is right bracket
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsRightBracket(string symbol)
        {
            return symbol == ")";
        }
        /// <summary>
        /// checkes if symbol is dot
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsDot(char symbol)
        {
            return symbol == '.';
        }
        /// <summary>
        /// checkes if symbol is dot
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsDot(string symbol)
        {
            return symbol == ".";
        }
        /// <summary>
        /// checkes if symbol is pi number
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        private bool IsPi(char symbol)
        {
            return symbol == TextPi.Last();
        }
        #endregion

        #region logic work functions

        /// <summary>
        /// process a digit input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DigitsInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            if (_display.Text.Last() == TextPi.Last() || 
                IsRightBracket(_display.Text.Last()))
            {
                _display.Text += "*" + (sender as Button).Content.ToString();
                Separators.Push("*");
            }
            else if (_display.Text == InitialText)
            {
                _display.Text = (sender as Button).Content.ToString();
            }
            else
            {
                _display.Text += (sender as Button).Content.ToString();
            }
        }
        /// <summary>
        /// process a dot input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DotInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            string[] separator = { Separators.First() };
            string text = _display.Text.Split(separator, System.StringSplitOptions.None).Last();
            if (IsRightBracket(_display.Text.Last()))
            {
                _display.Text += "*0.";
                Separators.Push("*");
            }
            else if (IsLeftBracket(_display.Text.Last()))
            {
                _display.Text += "0.";
            }
            else if (!text.Contains('.') && text.Last() != TextPi.Last())
            {
                _display.Text += ".";
            }
        }
        /// <summary>
        /// process a backspace input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackspaceInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var text = _display.Text;
            if (IsLeftBracket(text.Last())) { OpenBrackets--; Separators.Pop(); }
            else if (IsRightBracket(text.Last())) { CloseBrackets--; Separators.Pop(); }
            else if (IsSimpleOperation(text.Last())) Separators.Pop();
            if (text.Length == 1)
            {
                _display.Text = InitialText;
            } else
            {
                _display.Text = (text.Remove(text.Length - 1));
            }
        }
        /// <summary>
        /// process a total delete input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DeleteInput(object sender, RoutedEventArgs e)
        {
            CloseBrackets = OpenBrackets = 0;
            Separators.Clear();
            Separators.Push("\n");
            _display.Text = InitialText;
        }
        /// <summary>
        /// process a simple operation input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SimpleFunctionInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var buttext = (sender as Button).Content.ToString();
            var disptext = _display.Text;
            if (IsLeftBracket(disptext.Last()))
            {
                if (buttext == "-") _display.Text += buttext;
                else if (buttext == "/") _display.Text += "1" + buttext;
            }
            else if (IsSimpleOperation(disptext.Last()))
            {
                disptext.Remove(disptext.Length - 1);
                _display.Text = disptext + buttext;
            } 
            else if (IsDot(disptext.Last()))
            {
                _display.Text += "0" + buttext;
            }
            else if (IsRightBracket(disptext.Last()) || char.IsDigit(disptext.Last()))
            {
                _display.Text += buttext;
            }
            Separators.Push(buttext);
        }
        /// <summary>
        /// process a pi number input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PiNumberInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (disptext == InitialText) _display.Text = buttext;
            else if (char.IsDigit(disptext.Last()) || IsRightBracket(disptext.Last()) ||
                IsPi(disptext.Last()))
            {
                _display.Text += "*" + buttext;
                Separators.Push("*");
            }
            else if (IsDot(disptext.Last()))
            {
                _display.Text += "0*" + buttext;
                Separators.Push("*");
            }
            else _display.Text += buttext;
        }
        /// <summary>
        /// process a sqr input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SqrInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (char.IsDigit(disptext.Last()) || IsRightBracket(disptext.Last()) ||
                IsFactorial(disptext.Last()) || IsPi(disptext.Last()))
            {
                _display.Text += buttext;
                Separators.Push("^");
            }
            else if (IsSimpleOperation(disptext.Last()))
            {
                Separators.Pop();
                _display.Text = (disptext.Remove(disptext.Length - 1) + buttext);
                Separators.Push("^");
            }
        }
        /// <summary>
        /// process a left bracket input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftBracketInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (disptext == InitialText) _display.Text = buttext;
            else if (char.IsDigit(disptext.Last()) || IsRightBracket(disptext.Last()) ||
                IsPi(disptext.Last()) || IsFactorial(disptext.Last()))
            {
                _display.Text += "*" + buttext;
                Separators.Append("*");
                OpenBrackets++;
                Separators.Append(buttext);
            }
            else if (IsDot(disptext.Last()))
            {
                _display.Text += "0*" + buttext;
                Separators.Append("*");
                OpenBrackets++;
                Separators.Append(buttext);
            }
            else if (IsLeftBracket(disptext.Last()) || IsSimpleOperation(disptext.Last()))
            {
                _display.Text += buttext;
                OpenBrackets++;
                Separators.Append(buttext);
            }
        }
        /// <summary>
        /// process a right bracket input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightBracketInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (OpenBrackets > CloseBrackets)
            {
                if (IsFactorial(disptext.Last()) || IsRightBracket(disptext.Last()) ||
                    char.IsDigit(disptext.Last()))
                {
                    _display.Text += buttext;
                    CloseBrackets++;
                    Separators.Push(buttext);
                }
                else if (IsDot(disptext.Last()) || IsLeftBracket(disptext.Last()))
                {
                    _display.Text += "0" + buttext;
                    CloseBrackets++;
                    Separators.Push(buttext);
                }
            }
        }
        /// <summary>
        /// process a standard function input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StandardFunctionInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (disptext == InitialText)
            {
                _display.Text = buttext + "(";
                Separators.Push("(");
                OpenBrackets++;
            }
            else if (IsFactorial(disptext.Last()) || IsPi(disptext.Last()) ||
                char.IsDigit(disptext.Last()) || IsRightBracket(disptext.Last()))
            {
                _display.Text += "*" + buttext + "(";
                Separators.Push("*");
                Separators.Push("(");
                OpenBrackets++;
            }
            else if (IsSimpleOperation(disptext.Last()) || IsLeftBracket(disptext.Last()))
            {
                _display.Text += buttext + "(";
                Separators.Push("*");
                Separators.Push("(");
                OpenBrackets++;
            }
            else if (IsDot(disptext.Last()))
            {
                _display.Text += "0*" + buttext + "(";
                Separators.Push("*");
                Separators.Push("(");
                OpenBrackets++;
            }
        }
        /// <summary>
        /// process a pow input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PowInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (char.IsDigit(disptext.Last()) || IsFactorial(disptext.Last()) ||
                IsRightBracket(disptext.Last()) || IsPi(disptext.Last()))
            {
                _display.Text += buttext + "(";
                Separators.Push("^");
                Separators.Push("(");
                OpenBrackets++;
            }
            else if (IsSimpleOperation(disptext.Last()))
            {
                _display.Text += disptext.Remove(disptext.Length - 1) + buttext + "(";
                Separators.Push("^");
                Separators.Push("(");
                OpenBrackets++;
            }
        }
        /// <summary>
        /// process a coma input in logarithm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComaInput(object sender, RoutedEventArgs e)
        {
            RefreshDisplay();
            var disptext = _display.Text;
            var buttext = (sender as Button).Content.ToString();
            if (IsRightBracket(disptext.Last()) || char.IsDigit(disptext.Last()))
            {
                _display.Text += buttext;
                Separators.Push(buttext);
            }
        }
        /// <summary>
        /// puts a display into initail state
        /// </summary>
        private void RefreshDisplay()
        {
            if (Calculated)
            {
                string[] separator = { "\n", };
                _display.Text = _display.Text.Split(separator, System.StringSplitOptions.None).First();
                Calculated = false;
            }
        }
        /// <summary>
        /// process a button result input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CalculateResult(object sender, RoutedEventArgs e)
        {
            if (Calculated) return;
            if (OpenBrackets > CloseBrackets)
            {
                var str = "";
                var difference = OpenBrackets - CloseBrackets;
                for (var index = 0; index < difference; index++)
                {
                    RoutedEventArgs r = new RoutedEventArgs();
                    RightBracketInput(_left_bracket, r);
                }
                _display.Text += str;
            }
            var text = _display.Text.Replace(TextSqrt, "sqrt").Replace(TextPi, "pi");
            Debug.WriteLine(text);
            Calculated = true;
            var result = _counter.Result(text);
            if (result == _counter._error) _display.Text += "\n" + "Wrong input!";
            else _display.Text += "\n= " + result.ToString();
        }
        #endregion

        public Connector(TextBox displaybox, Button righttBracket)
        {
            ListOfOperations = new List<char> { '+', '-', '*', '/' };
            Separators = new Stack<string>();
            Separators.Push("\n");
            OpenBrackets = CloseBrackets = 0;
            _display = displaybox;
            _left_bracket = righttBracket;
            _counter = new Counter();
        }
    }
}
