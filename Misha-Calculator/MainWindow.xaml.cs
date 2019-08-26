using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Misha_Calculator.Additional;

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
        }

        private void RepresentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var connector = new Connector(Display);
            _1.Click += connector.DigitsInput;

        }
    }
   
}
