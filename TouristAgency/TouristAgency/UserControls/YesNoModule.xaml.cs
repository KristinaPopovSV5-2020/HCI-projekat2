using System;
using System.Collections.Generic;
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
    /// Interaction logic for YesNoModule.xaml
    /// </summary>
    public partial class YesNoModule : UserControl
    {
        public event EventHandler PotvrdiClicked;
        public event EventHandler OdustaniClicked;

        public YesNoModule(string poruka)
        {
            InitializeComponent();
            this.poruka.Text = poruka;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            PotvrdiClicked?.Invoke(this, EventArgs.Empty);
        }

        public void Odustani(object sender, RoutedEventArgs e)
        {
            OdustaniClicked?.Invoke(this, EventArgs.Empty);
        }


    }
}

