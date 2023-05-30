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
    /// Interaction logic for ucPrijava.xaml
    /// </summary>
    public partial class ucPrijava : UserControl
    {
        public event RoutedEventHandler LoginClicked;
        KorisnikServis KorisnikServis = new KorisnikServis();

        public ucPrijava()
        {
            InitializeComponent();
        }


        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            bool isCredentialsValid = ValidateInput(username, password);

            if (isCredentialsValid)
            {
                txtError.Visibility = Visibility.Collapsed;
            }
           
            LoginClicked?.Invoke(this, e);
        }


        private bool ValidateInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Korisničko ime treba da bude popunjeno.";
                return false;
            }else if (string.IsNullOrWhiteSpace(password))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Lozinka treba da bude popunjena.";
                return false;
            }

            if (!KorisnikServis.Prijava(username, password))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Neispravno korisničko ime ili lozinka.";
                return false;
            }

            return true;
        }
    }
}
