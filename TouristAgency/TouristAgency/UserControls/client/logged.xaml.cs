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

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            Odjava.Invoke(this, e);
        }

        private void Putovanja_Click(object sender, RoutedEventArgs e)
        {
            Putovanja.Invoke(this, e);
        }
    }
}
