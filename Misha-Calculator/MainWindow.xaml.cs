using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Misha_Calculator.Additional;
using System.Windows.Input;
using System.Diagnostics;

namespace Misha_Calculator
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Sqrt.Content = "\u221A";
            Pi.Content = "\u03c0";
        }
        private Connector connector;

        private void RepresentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            connector = new Connector(Display, RightBracket);

            _1.Click += connector.DigitsInput;
            _2.Click += connector.DigitsInput;
            _3.Click += connector.DigitsInput;
            _4.Click += connector.DigitsInput;
            _5.Click += connector.DigitsInput;
            _6.Click += connector.DigitsInput;
            _7.Click += connector.DigitsInput;
            _8.Click += connector.DigitsInput;
            _9.Click += connector.DigitsInput;
            _0.Click += connector.DigitsInput;

            Plus.Click += connector.SimpleFunctionInput;
            Minus.Click += connector.SimpleFunctionInput;
            Multi.Click += connector.SimpleFunctionInput;
            Divide.Click += connector.SimpleFunctionInput;
            Factorial.Click += connector.SimpleFunctionInput;

            Sqr.Click += connector.SqrInput;

            LeftBracket.Click += connector.LeftBracketInput;
            RightBracket.Click += connector.RightBracketInput;

            Backspace.Click += connector.BackspaceInput;
            Clear.Click += connector.DeleteInput;

            Result.Click += connector.CalculateResult;

            Pi.Click += connector.PiNumberInput;

            Dot.Click += connector.DotInput;

            Power.Click += connector.PowInput;
            Coma.Click += connector.ComaInput;

            TenPower.Click += connector.StandardFunctionInput;
            Sh.Click += connector.StandardFunctionInput;
            Ch.Click += connector.StandardFunctionInput;
            Th.Click += connector.StandardFunctionInput;
            Ln.Click += connector.StandardFunctionInput;
            Lg.Click += connector.StandardFunctionInput;
            Log.Click += connector.StandardFunctionInput;
            Exp.Click += connector.StandardFunctionInput;
            Sqrt.Click += connector.StandardFunctionInput;
            
            
        }

        private void RepresentWindow_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.Key);
            switch (e.Key)
            {
                case Key.D1: connector.DigitsInput(_1, e); break;
                case Key.D2: connector.DigitsInput(_2, e); break;
                case Key.D3: connector.DigitsInput(_3, e); break;
                case Key.D4: connector.DigitsInput(_4, e); break;
                case Key.D5: connector.DigitsInput(_5, e); break;
                case Key.D6: connector.DigitsInput(_6, e); break;
                case Key.D7: connector.DigitsInput(_7, e); break;
                case Key.D8: connector.DigitsInput(_8, e); break;
                case Key.D9: connector.DigitsInput(_9, e); break;
                case Key.D0: connector.DigitsInput(_0, e); break;

                
                
            }
        }
    }

}
