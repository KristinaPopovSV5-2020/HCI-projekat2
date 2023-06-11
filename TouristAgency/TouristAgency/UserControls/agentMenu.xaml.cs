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
    /// Interaction logic for agentMenu.xaml
    /// </summary>
    public partial class agentMenu : UserControl
    {
        public agentMenu()
        {
            InitializeComponent();
        }

        public event EventHandler Smestaji;
        public event EventHandler Atrakcije;
        public event EventHandler Putovanja;
        public event EventHandler Restorani;
        public event EventHandler Odjava;
        public event EventHandler Izvestaji;

        private void Putovanja_Click(object sender, RoutedEventArgs e)
        {
            Putovanja.Invoke(this, e);
        }

        private void Atrakcije_Click(object sender, RoutedEventArgs e)
        {
            Atrakcije.Invoke(this, e);
        }

        private void Izvestaji_Click(object sender, RoutedEventArgs e)
        {
            Izvestaji.Invoke(this, e);
        }
        private void Restorani_Click(object sender, RoutedEventArgs e)
        {
            Restorani.Invoke(this, e);
        }
        private void Smestaji_Click(object sender, RoutedEventArgs e)
        {
            Smestaji.Invoke(this, e);
        }

        private void Odjava_Click(object sender, RoutedEventArgs e)
        {
            Odjava.Invoke(this, e);
        }
    }
}
