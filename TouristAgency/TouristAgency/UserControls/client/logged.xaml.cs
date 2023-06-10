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

namespace TouristAgency.UserControls.client
{
    /// <summary>
    /// Interaction logic for logged.xaml
    /// </summary>
    public partial class logged : UserControl
    {
        public logged()
        {
            InitializeComponent();
        }

        public event EventHandler Odjava;
        public event EventHandler Putovanja;
        public event EventHandler Rezervacije;
        public event EventHandler Kupljeni;

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            Odjava.Invoke(this, e);
        }

        private void Putovanja_Click(object sender, RoutedEventArgs e)
        {
            Putovanja.Invoke(this, e);
        } 
        private void Rezervacije_Click(object sender, RoutedEventArgs e)
        {
            Rezervacije.Invoke(this, e);
        }

        private void Kupljeni_Click(object sender, RoutedEventArgs e)
        {
            Kupljeni.Invoke(this, e);
        }
    }
}
