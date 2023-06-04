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


        private void Prijava_click(object sender, RoutedEventArgs e)
        {
            LoginClick?.Invoke(this, EventArgs.Empty);
        }


    }
}
