using MathexprProcessorCs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WindowsDesktop.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "") 
        {
            if (propertyName != null) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        const string DefExpression = "0";

        public string SqrtText => "\u221A";

        public string MulText => "\u00D7";

        public string DivText => "\u00F7";

        public string PiText => "\u03C0";

        string _sinText;

        public string SinText
        {
            get => _sinText;
            set
            {
                _sinText = value;
                OnPropertyChanged(nameof(SinText));
            }
        }

        string _cosText;

        public string CosText
        {
            get => _cosText;
            set
            {
                _cosText = value;
                OnPropertyChanged(nameof(CosText));
            }
        }

        string _tanText;

        public string TanText
        {
            get => _tanText;
            set
            {
                _tanText = value;
                OnPropertyChanged(nameof(TanText));
            }
        }

        string _expression = DefExpression;

        public string Expression
        {
            get => _expression;
            set
            {
                _expression = value;
                OnPropertyChanged(nameof(Expression));
            }
        }

        public ICommand CmdAddCharacters { get; }

        public ICommand CmdAddFunction { get; }

        public ICommand CmdAddOperation { get; }

        public ICommand CmdSwitchFunctions { get; }

        public ICommand CmdClearInput { get; }

        public ICommand CmdBackspace { get; }
        
        public ICommand CmdCalculateResult { get; }

        int _funcIndex = 0;

        readonly static string[] SinTexts = { "sin", "sinh" };
        readonly static string[] CosTexts = { "cos", "cosh" };
        readonly static string[] TanTexts = { "tan", "tanh" };

        // a flag to indicate a new input has been started
        bool _inputInProgress = false;
        // last calculated result
        double _result = 0;

        public MainViewModel()
        {
            SwitchFunctions();
            CmdAddCharacters = new ExecutableCommand(obj => AppendText((string)obj));
            CmdAddFunction = new ExecutableCommand(obj => AppendFunction((string)obj));
            CmdAddOperation = new ExecutableCommand(obj => AppendOperation((string)obj));
            CmdSwitchFunctions = new ExecutableCommand(_ => SwitchFunctions());
            CmdClearInput = new ExecutableCommand(_ => ClearInput());
            CmdBackspace = new ExecutableCommand(_ => Backspace());
            CmdCalculateResult = new ExecutableCommand(_ => CalculateResult());
        }

        void AppendText(string text)
        {
            if (_inputInProgress)
            {
                Expression += text;
            } else
            {
                Expression = text;
                _inputInProgress = true;
            }
        }

        void AppendOperation(string text)
        {
            if (!_inputInProgress && Expression != DefExpression)
            {
                Expression = _result.ToString() + text;
            } else
            {
                Expression += text;
            }            
            _inputInProgress = true;
        }

        void AppendFunction(string text)
        {
            AppendOperation(text + "(");
        }

        void SwitchFunctions()
        {
            SinText = SinTexts[_funcIndex];
            CosText = CosTexts[_funcIndex];
            TanText = TanTexts[_funcIndex];
            _funcIndex = (_funcIndex + 1) % 2;
        }

        void ClearInput()
        {
            Expression = DefExpression;
            _inputInProgress = false;
        }

        static int CountSymbols(string str, char sym)
        {
            int count = 0;
            foreach (char ch in str)
            {
                if (ch == sym)
                {
                    ++count;
                }
            }
            return count;
        }

        void Backspace()
        {
            if (_inputInProgress)
            {
                int length = Expression.Length;
                if (length > 1)
                {
                    Expression = Expression.Substring(0, length - 1);
                }
                else
                {
                    Expression = DefExpression;
                    _inputInProgress = false;
                }
            }
        }

        void CalculateResult()
        {
            if (_inputInProgress)
            {
                try
                {
                    var expression = Expression;
                    int openBracketsCount = CountSymbols(expression, '(');
                    int closeBracketsCount = CountSymbols(expression, ')');
                    if (closeBracketsCount > openBracketsCount)
                    {
                        throw new FormatException("A number of ')' is greater than '('.");
                    }
                    else if (closeBracketsCount < openBracketsCount)
                    {
                        expression += new string(')', openBracketsCount - closeBracketsCount);
                    }
                    expression = expression.Replace(SqrtText, "sqrt");
                    expression = expression.Replace(MulText, "*");
                    expression = expression.Replace(DivText, "/");
                    expression = expression.Replace(PiText, "pi");
                    _result = MathexprProcessor.Process(expression);
                    Expression += Environment.NewLine + $"={_result}";
                }
                catch (Exception ex)
                {
                    Expression += Environment.NewLine + ex.Message;
                }
                _inputInProgress = false;
            }
        }
    }
}
