using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucInput.xaml
    /// </summary>
    public partial class ucInput : UserControl
    {
        public ucInput()
        {
            InitializeComponent();
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty = DependencyProperty.Register("Hint", typeof(string), typeof(ucInput));

        private void Promenjen_Text(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string noviText = textBox.Text;

            // Update the property with the new text
            Hint = noviText;
        }
    }
}
