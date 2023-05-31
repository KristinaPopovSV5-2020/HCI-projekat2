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
   
    public partial class ucRegistracija : UserControl
    {
        public event RoutedEventHandler RegistracijaClicked;
        KorisnikServis KorisnikServis = new KorisnikServis();

        public ucRegistracija()
        {
            InitializeComponent();
        }

        private void BtnRegistracija_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string coPassword = txtCoPassword.Password;

            bool isCredentialsValid = ValidateInput(username, password, coPassword);

            if (isCredentialsValid)
            {
                txtError.Visibility = Visibility.Collapsed;
                 KorisnikServis.Registracija(username, password);
                txtSucces.Visibility = Visibility.Visible;
                txtSucces.Text = "Uspešno ste se registrovali.";

            }

            RegistracijaClicked?.Invoke(this, e);
        }


        private bool ValidateInput(string username, string password, string coPassword)
        {
            if (string.IsNullOrWhiteSpace(username) )
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
            else if (string.IsNullOrWhiteSpace(coPassword))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Potvrda lozinke treba da bude popunjena.";
                return false;

            }

            if (!password.Equals(coPassword))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Lozinke se ne poklapaju.";
                return false;

            }

            if (KorisnikServis.ProveraKorisnickogImena(username))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Korisničko ime je zauzeto. Unesite drugo.";
                return false;
            }
           
            return true;
        }
    }
}
