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
using TouristAgency.Helper;
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
            this.ime.Focus();
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
                txtErrorUsername.Visibility = Visibility.Visible;
                txtError.Text = "Korisničko ime treba da bude popunjeno.";
                return false;
            }else if (string.IsNullOrWhiteSpace(password))
            {
                txtErrorPassword.Visibility = Visibility.Visible;
                txtErrorPassword.Text = "Lozinka treba da bude popunjena.";
                return false;

            }
            else if (string.IsNullOrWhiteSpace(coPassword))
            {
                txtErrorCoPassword.Visibility = Visibility.Visible;
                txtErrorCoPassword.Text = "Potvrda lozinke treba da bude popunjena.";
                return false;

            }

            if (!password.Equals(coPassword))
            {
                txtErrorCoPassword.Visibility = Visibility.Visible;
                txtErrorCoPassword.Text = "Lozinke se ne poklapaju.";
                return false;

            }

            if (KorisnikServis.ProveraKorisnickogImena(username))
            {
                txtErrorUsername.Visibility = Visibility.Visible;
                txtErrorUsername.Text = "Korisničko ime je zauzeto. Unesite drugo.";
                return false;
            }
           
            return true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            Console.WriteLine("aaaa");
            if (focusedControl is DependencyObject)
            {
                e.Handled = true;
                UserWindow userWindow = new UserWindow();
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Console.WriteLine(str);
                HelpProvider.ShowHelp(str, userWindow);
            }
        }

        public void doThings(string param)
        {
          
        }
    }
}
