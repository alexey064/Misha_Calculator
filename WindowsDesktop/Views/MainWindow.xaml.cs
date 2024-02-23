using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsDesktop.ViewModels;

namespace WindowsDesktop.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        bool _shift = false;

        void ExecuteButtonCommand(Button button)
        {
            button.Command.Execute(button.CommandParameter);
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Key);
            switch (e.Key)
            {
                case Key.D0: 
                    ExecuteButtonCommand(_shift ? ButtonRightBracket : ButtonZero); 
                    break;
                case Key.D1:
                    ExecuteButtonCommand(_shift ? ButtonFactorial : ButtonOne);
                    break;
                case Key.D2:
                    ExecuteButtonCommand(ButtonTwo);
                    break;
                case Key.D3:
                    ExecuteButtonCommand(ButtonThree);
                    break;
                case Key.D4:
                    ExecuteButtonCommand(ButtonFour);
                    break;
                case Key.D5: 
                    ExecuteButtonCommand(ButtonFive);
                    break;
                case Key.D6:
                    ExecuteButtonCommand(_shift ? ButtonPower : ButtonSix);
                    break;
                case Key.D7:
                    ExecuteButtonCommand(ButtonSeven);
                    break;
                case Key.D8:
                    ExecuteButtonCommand(_shift ? ButtonMul : ButtonEight);
                    break;
                case Key.D9:
                    ExecuteButtonCommand(_shift? ButtonLeftBracket : ButtonNine);
                    break;
                case Key.LeftShift:
                case Key.RightShift:
                    _shift = true;
                    break;
                case Key.Delete:
                    ExecuteButtonCommand(ButtonClear);
                    break;
                case Key.Back:
                    ExecuteButtonCommand(ButtonBackspace);
                    break;
                case Key.OemMinus:
                    ExecuteButtonCommand(ButtonMinus);
                    break;
                case Key.OemPlus:
                    ExecuteButtonCommand(_shift ? ButtonPlus : ButtonResult);
                    break;
                case Key.OemQuestion:
                    ExecuteButtonCommand(ButtonDiv);
                    break;
                case Key.OemComma:
                    ExecuteButtonCommand(ButtonComma);
                    break;
                case Key.OemPeriod:
                    ExecuteButtonCommand(ButtonDot);
                    break;
                case Key.Enter:
                    ExecuteButtonCommand(ButtonResult);
                    break;
            }
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.LeftShift:
                case Key.RightShift:
                    _shift = false;
                    break;
            }
        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBlockDisplay.CaretIndex = TextBlockDisplay.Text.Length;
            Rect rect = TextBlockDisplay.GetRectFromCharacterIndex(TextBlockDisplay.Text.Length - 1);
            TextBlockDisplay.ScrollToHorizontalOffset(rect.Right);
        }
    }
}
