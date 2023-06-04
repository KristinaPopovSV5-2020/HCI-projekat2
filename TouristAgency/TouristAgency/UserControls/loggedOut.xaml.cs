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
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for loggedOut.xaml
    /// </summary>
    public partial class loggedOut : UserControl
    {
        KorisnikServis korisnikServis = new KorisnikServis();
        public loggedOut()
        {
            InitializeComponent();
        }

        public event EventHandler LoginClick;
        public event EventHandler RegistracijaClick;
        public event EventHandler PutovanjaClick;


        private void Prijava_click(object sender, RoutedEventArgs e)
        {
            LoginClick?.Invoke(this, EventArgs.Empty);
        }

        private void Registracija_Click(object sender, RoutedEventArgs e)
        {
            RegistracijaClick?.Invoke(this, EventArgs.Empty);
        }

        private void Putovanja_Click(object sender, RoutedEventArgs e)
        {
            PutovanjaClick?.Invoke(this, EventArgs.Empty);
        }


    }
}
