using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Misha_Calculator
{
    
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool IsDigit(char c)
        {
            return (c == 0 || c == 1 || c == 2 || c == 3 || c == 4 || c == 5 || c == 6 || c == 7 || c == 8 || c == 9);
        }

    bool IsRightBracket(Char c)
        {
            return c == ')';
        }

        bool IsLeftBracket(Char c)
        {
            return c == '(';
        }

    bool IsSimpleOperation(Char c)
        {
            return (c == '+' || c == '-' || c == '/' || c == '*');
        }

        bool IsDot(Char c)
        {
        return c == '.';
        }

    bool IsFactorial(Char c)
        {
            return c == '!';
        }

        Int32 OpenBrackets = 0, CloseBrackets = 0; //Количество открытых и закрытых скобок.
        String separator = "\n"; // как я понял это отделяет символы друг от друга
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Button =(Button) sender;
            Char Name = Button.Content.ToString()[0];
            Digits_numbers(Name);
        }
        public void Digits_numbers(Char Name)
        {
            /*     if (Display.Text[Display.Text.Length - 1] == Pi.Content.ToString().Length || IsrightBracket(Display.Text[Display.Text.Length -1]))
                 {
                     Display.Text+=Name;
                     separator = "*"; 
                 }
                 else if (Display.Text == "0")
                     Display.Text+=Name;
                 else

                 Display.Text+=Name;
         */
            Display.Text += Name;
        }

        //Нажатие кнопки точка
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.ToString().Split(separator[0]).Last().Length == 0)
            {
                Display.Text+="0.";
            }
            else if (!Display.Text.Split(separator[0]).Last().Contains('.'))
            {
                Display.Text+=".";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        { //Очистка всего
            //Display.Enabled=true;
            Display.Text="0";
            CloseBrackets = 0;
            OpenBrackets = 0;
        }

        private void Simple_functions(object sender, RoutedEventArgs e)
        {
            var button= (Button) sender;
            String text = Display.Text;
            if (text[text.Length - 1] == '(')
            {
                if (button.Content.ToString() == "+")
                {
                    Display.Text=text + "0" + button.Content.ToString();
                }
                else if (button.Content.ToString() == "*" || button.Content.ToString() == "/")
                {
                    Display.Text=text + "1" + button.Content;
                }
                else
                {
                    Display.Text=text + button.Content;
                }
            }
            else if (text[text.Length - 1] == '+' ||
                     text[text.Length - 1] == '-' ||
                     text[text.Length - 1] == '/' ||
                     text[text.Length - 1] == '*')
            {
                text.Remove(text.Length - 1, 1);
                Display.Text=text + button.Content;
            }
            else if (text[text.Length - 1] == '.')
            {
                Display.Text=text + "0" + button.Content;
            }
            else
            {
                Display.Text=text + button.Content;
            }
            separator = button.Content.ToString();
        }

        private void Pi_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            if (Display.Text == "0")
            {
                Display.Text=button.Content.ToString();
            }
            // нужно исправить
            else if (IsDigit(Display.Text[Display.Text.Length - 1]) ||
                  IsRightBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text + "*" + button.Content.ToString();
                separator = "*";
            }
            else if (Display.Text[Display.Text.Length - 1] == '.')
            {
                Display.Text=Display.Text + "0*" + button.Content;
                separator = "*";
            }
            else
            {
                Display.Text=Display.Text + button.Content;
            }
        }

        private void Sqr_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            if (IsDigit(Display.Text[Display.Text.Length - 1]) ||
                    IsRightBracket(Display.Text[Display.Text.Length - 1]) ||
                    IsFactorial(Display.Text[Display.Text.Length - 1]) ||
                    Display.Text.Split(separator[0]).Last().ToString() == Pi.Content.ToString())
            {
                Display.Text=Display.Text + button.Content + "*";
                separator = "*";
            }
            else if (IsSimpleOperation(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text.Remove(Display.Text.Length - 1, 1) + button.Content;
                separator = button.Content.ToString();
            }
        }

        private void Brackets(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            if (IsLeftBracket(button.Content.ToString()[0]))
            {
                if (Display.Text == "0")
                {
                    Display.Text=button.Content.ToString();
                    separator = "(";
                    OpenBrackets++;
                }
                else if (IsDigit(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + "*" + button.Content);
                    separator = button.Content.ToString();
                    OpenBrackets++;
                }
                else if (IsDot(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + "0*(");
                    separator = button.Content.ToString();
                    OpenBrackets++;
                }
                else if (IsLeftBracket(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + button.Content);
                    separator = button.Content.ToString();
                    OpenBrackets++;
                }
                else if (IsRightBracket(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + "*(");
                    separator = button.Content.ToString();
                    OpenBrackets++;
                }
                else if (IsSimpleOperation(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + button.Content);
                    separator = button.Content.ToString();
                    OpenBrackets++;
                }
            }
            else if (OpenBrackets > CloseBrackets)
            {
                if (IsFactorial(Display.Text[Display.Text.Length - 1]) ||
                        IsDigit(Display.Text[Display.Text.Length - 1]) ||
                        IsRightBracket(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + button.Content);
                    CloseBrackets++;
                    separator = button.Content.ToString();
                }
                else if (IsDot(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=(Display.Text + "0)");
                    CloseBrackets++;
                    separator = button.Content.ToString();
                }
                else if (IsLeftBracket(Display.Text[Display.Text.Length - 1]))
                {
                    Display.Text=Display.Text + "0)";
                    CloseBrackets++;
                    separator = button.Content.ToString();
                }
            }
        }

        private void standard_functions(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            if (Display.Text == "0" || Display.Text.isEmpty())
            {
                Display.Text=(button.Content + "(");
                separator = "(";
                OpenBrackets++;
            }
            else if (IsFactorial(Display.Text[Display.Text.Length - 1]) ||
                  IsDigit(Display.Text[Display.Text.Length - 1]) ||
                  IsRightBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text + "*" + button.Content + "(";
                separator = "(";
                OpenBrackets++;
            }
            else if (IsSimpleOperation(Display.Text[Display.Text.Length - 1]) ||
                     IsLeftBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=(Display.Text + button.Content + "(");
                separator = "(";
                OpenBrackets++;
            }
            else if (IsDot(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=(Display.Text + "0*" + button.Content + "(");
                separator = "(";
                OpenBrackets++;
            }
        }

        private void Pow_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (IsDigit(Display.Text[Display.Text.Length - 1]) ||
                    IsFactorial(Display.Text[Display.Text.Length - 1]) ||
                    IsRightBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text = Display.Text + button.Content + "(";
                separator = "(";
                OpenBrackets++;
            }
            else if (IsSimpleOperation(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text = (Display.Text.Remove(Display.Text.Length - 1, 1) + button.Content + "(");
                separator = "(";
                OpenBrackets++;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Клавиша Log
            var button = (Button) sender;
            if (Display.Text == "0")
            {
                Display.Text=button.Content + "(";
                separator = "(";
                OpenBrackets++;
            }
            else if (IsDigit(Display.Text[Display.Text.Length - 1]) ||
                  IsFactorial(Display.Text[Display.Text.Length - 1]) ||
                  IsRightBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text + "*" + button.Content + "(";
            }
            else if (IsSimpleOperation(Display.Text[Display.Text.Length - 1]) ||
                    IsLeftBracket(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text + button.Content + "(";
                separator = "(";
                OpenBrackets++;
            }
            else if (IsDot(Display.Text[Display.Text.Length - 1]))
            {
                Display.Text=Display.Text + "0*" + button.Content + "(";
                separator = "(";
                OpenBrackets++;
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 1)
            {   
                //Если первая цифра ввода скобка, то удаляем скобку, ставим ноль и уменьшаем количество открытых скобок
                if (Display.Text[Display.Text.Length - 1] == '(')
                {
                    OpenBrackets--;
                }
                Display.Text="0";
            }
            else
            { //Если в многозначной строке удаляем скобку, то мы должны уменьшить колво скобок
                if (Display.Text[Display.Text.Length - 1] == '(')
                {
                    OpenBrackets--;
                }
                if (Display.Text[Display.Text.Length - 1] == ')')
                {
                    CloseBrackets--;
                }
                Display.Text.Remove(Display.Text.Length - 1, 1); //Показать тавтологию
            }
        }
        

    }
   
}
