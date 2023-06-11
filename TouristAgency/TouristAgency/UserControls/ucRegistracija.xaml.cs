using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        Popup popup1 = new Popup();

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
                OkModule popupUserControl = new OkModule("Uspešno ste se registrovali.");

                popup1.Child = null;
                popup1.Child = popupUserControl;
              
                popup1.Height = 180;
                popup1.Width = 400;
                popup1.Placement = PlacementMode.Center;
                popup1.VerticalOffset = (SystemParameters.PrimaryScreenHeight - popup1.Height) / 2 + 100;
                popup1.HorizontalOffset = (SystemParameters.PrimaryScreenWidth - popup1.Width) / 2 + 100;

                popup1.AllowsTransparency = true;

                popup1.IsOpen = true;

                popupUserControl.PotvrdiClicked += Zatvori;
            

           

            RegistracijaClicked?.Invoke(this, e);

            }

            
        }

        public void Zatvori(object sender, EventArgs e)
        {
            popup1.IsOpen = false;
           
        }


        private bool ValidateInput(string username, string password, string coPassword)
        {
            if (string.IsNullOrWhiteSpace(username) )
            {
                txtErrorUsername.Visibility = Visibility.Visible;
                txtErrorUsername.Text = "Korisničko ime treba da bude popunjeno.";
                return false;
            }else if (string.IsNullOrWhiteSpace(password))
            {
                txtErrorUsername.Visibility = Visibility.Collapsed;
                txtErrorPassword.Visibility = Visibility.Visible;
                txtErrorPassword.Text = "Lozinka treba da bude popunjena.";
                return false;

            }
            else if (string.IsNullOrWhiteSpace(coPassword))
            {
                txtErrorPassword.Visibility = Visibility.Collapsed;
                txtErrorCoPassword.Visibility = Visibility.Visible;
                txtErrorCoPassword.Text = "Potvrda lozinke treba da bude popunjena.";
                return false;

            }

            if (!password.Equals(coPassword))
            {
                txtErrorCoPassword.Visibility = Visibility.Collapsed;
                txtErrorCoPassword.Visibility = Visibility.Visible;
                txtErrorCoPassword.Text = "Lozinke se ne poklapaju.";
                return false;

            }

            if (KorisnikServis.ProveraKorisnickogImena(username))
            {
                txtErrorCoPassword.Visibility = Visibility.Collapsed;
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
