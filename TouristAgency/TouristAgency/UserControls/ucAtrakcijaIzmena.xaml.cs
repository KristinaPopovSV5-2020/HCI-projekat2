using MongoDB.Bson;
using MongoDB.Driver;
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

        public ucAtrakcijaIzmena(string id, string naziv, string opis, string adresa)
        {
            InitializeComponent();

            this.id.Hint = id;
            this.naziv.Hint = naziv;
            this.opis.Hint = opis;
            this.adresa.Hint = adresa;

            atrakcija.Id = this.id.Hint;
            atrakcija.Naziv = this.naziv.Hint;
            atrakcija.Opis = this.opis.Hint;
            atrakcija.Adresa = this.adresa.Hint;

            naslov.Text = "Izmeni atrakciju";
            potvrdi.Content = "Izmeni";
            this.id.IsEnabled = false;
           

        }

        public ucAtrakcijaIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj atrakciju";
            potvrdi.Content = "Dodaj";
            this.id.Visibility = Visibility.Hidden;
            this.idLabela.Visibility = Visibility.Hidden;
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
                    VratiSeNa_Atrakcije?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (ValidateInput(atrakcija.Naziv, atrakcija.Opis, atrakcija.Adresa))
                {
                    putovanjaServis.IzmeniAtrakciju(atrakcija);
                    VratiSeNa_Atrakcije?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show($"Atrakcija '{atrakcija.Id}' je izmenjena.", "Atrakcija izmenjena", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private bool ValidateInput(string naziv, string opis, string adresa)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Naziv ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(opis))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Opis ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(adresa))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Adresa ne sme biti prazna";
                return false;
            }

            return true;
        }

    }
}
