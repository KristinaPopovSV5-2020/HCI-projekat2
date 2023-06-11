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
 
    /// <summary>
    /// Interaction logic for ucPrijava.xaml
    /// </summary>
    public partial class ucPrijava : UserControl
    {
        public event EventHandler<string> LoginClicked;
        public event EventHandler RegistracijaClicked;
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

            string res = "";

            if (isCredentialsValid)
            {
                txtError.Visibility = Visibility.Collapsed;
                res=KorisnikServis.Prijava(username, password);
            }
           
            LoginClicked?.Invoke(this, res);
        }


        private bool ValidateInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
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

            if (KorisnikServis.Prijava(username, password)=="")
            {
                txtErrorUsername.Visibility = Visibility.Collapsed;
                txtErrorPassword.Visibility = Visibility.Collapsed;
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Neispravno korisničko ime ili lozinka.";
                return false;
            }

            return true;
        }

        public void Registracija(object sender, EventArgs e)
        {
            RegistracijaClicked.Invoke(this, e);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            Console.WriteLine("aaaa");
            if (focusedControl is DependencyObject)
            {
                UserWindow userWindow = new UserWindow();
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Console.WriteLine(str);
                HelpProvider.ShowHelp(str, userWindow);
            }
        }
    }
}
