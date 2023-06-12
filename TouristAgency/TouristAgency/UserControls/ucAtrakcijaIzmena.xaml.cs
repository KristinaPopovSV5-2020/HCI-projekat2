using MongoDB.Bson;
using MongoDB.Driver;
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
using TouristAgency.Model;
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucAtrakcijaIzmena.xaml
    /// </summary>
    public partial class ucAtrakcijaIzmena : UserControl
    {
        public Atrakcija atrakcija = new Atrakcija();
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        Popup popup = new Popup();
        public ucAtrakcijaIzmena(string id, string naziv, string opis, string adresa)
        {
            InitializeComponent();

            this.naziv.Hint = naziv;
            this.opis.Hint = opis;
            this.adresa.Hint = adresa;
            this.atrakcija.Id = id;

            atrakcija.Naziv = this.naziv.Hint;
            atrakcija.Opis = this.opis.Hint;
            atrakcija.Adresa = this.adresa.Hint;

            naslov.Text = "Izmeni atrakciju";
            potvrdi.Content = "Izmeni";
           

        }

        public ucAtrakcijaIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj atrakciju";
            potvrdi.Content = "Dodaj";
        }

        public event EventHandler VratiSeNa_Atrakcije;

        private void Vrati_Se(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Atrakcije?.Invoke(this, EventArgs.Empty);

        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {

            atrakcija.Naziv = naziv.Hint;
            atrakcija.Opis = opis.Hint;
            atrakcija.Adresa = adresa.Hint;

            Button b = (Button)sender;
            if (b.Content.ToString() == "Dodaj")
            {
                if (ValidateInput(atrakcija.Naziv, atrakcija.Opis, atrakcija.Adresa))
                {
                    atrakcija.Id = ObjectId.GenerateNewId().ToString();
                    putovanjaServis.DodajAtrakciju(atrakcija);

                    OkModule popupUserControl = new OkModule("Dodali ste atrakciju " + atrakcija.Naziv);

                    popup.Child = null;
                    popup.Child = popupUserControl;
                    popup.HorizontalOffset = 500;
                    popup.VerticalOffset = 570;
                    popup.Height = 180;
                    popup.Width = 400;
                    popup.AllowsTransparency = true;

                    popup.IsOpen = true;

                    popupUserControl.PotvrdiClicked += Zatvori;
                    VratiSeNa_Atrakcije?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (ValidateInput(atrakcija.Naziv, atrakcija.Opis, atrakcija.Adresa))
                {
                    putovanjaServis.IzmeniAtrakciju(atrakcija);
                   

                    OkModule popupUserControl = new OkModule("Atrakcija "+atrakcija.Naziv+" je izmenjena.");
                    mainComponent.IsHitTestVisible = false;


                    popup.Child = null;
                    popup.Child = popupUserControl;
                    popup.HorizontalOffset = 500;
                    popup.VerticalOffset = 570;
                    popup.Height = 180;
                    popup.Width = 400;
                    popup.AllowsTransparency = true;

                    popup.IsOpen = true;

                    popupUserControl.PotvrdiClicked += Zatvori;

                    VratiSeNa_Atrakcije?.Invoke(this, EventArgs.Empty);

                    // MessageBox.Show($"Atrakcija '{atrakcija.Id}' je izmenjena.", "Atrakcija izmenjena", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        public void Zatvori(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
        }

        private bool ValidateInput(string naziv, string opis, string adresa)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtErrorNaziv.Visibility = Visibility.Visible;
                txtErrorNaziv.Text = "Naziv ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(opis))
            {
                txtErrorNaziv.Visibility = Visibility.Collapsed;
                txtErrorOpis.Visibility = Visibility.Visible;
                txtErrorOpis.Text = "Opis ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(adresa))
            {
                txtErrorOpis.Visibility = Visibility.Collapsed;
                txtErrorAdresa.Visibility = Visibility.Visible;
                txtErrorAdresa.Text = "Adresa ne sme biti prazna";
                return false;
            }

            return true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
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
