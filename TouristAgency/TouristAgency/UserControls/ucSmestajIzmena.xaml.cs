using MongoDB.Bson;
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
using TouristAgency.Model;
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucSmestajIzmena.xaml
    /// </summary>
    public partial class ucSmestajIzmena : UserControl
    {
        public Smestaj smestaj = new Smestaj();
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        Popup popup = new Popup();

        public ucSmestajIzmena(string id, string naziv, string tip, string adresa, string ocena)
        {
            InitializeComponent();

            foreach (ComboBoxItem item in this.ocena.Items)
            {
                if (item.Content.ToString() == ocena)
                {
                    this.ocena.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboBoxItem item in this.tip.Items)
            {
                if (item.Content.ToString() == tip)
                {
                    this.tip.SelectedItem = item;
                    break;
                }
            }

            this.naziv.Hint = naziv;
            this.adresa.Hint = adresa;

            smestaj.Naziv = this.naziv.Hint;
            smestaj.Tip = (TipSmestaja)Enum.Parse(typeof(TipSmestaja), (this.tip.SelectedItem as ComboBoxItem).Content.ToString());
            smestaj.Adresa = this.adresa.Hint;
            smestaj.Ocena = this.ocena.SelectedItem.ToString();

            naslov.Text = "Izmeni smestaj";
            potvrdi.Content = "Izmeni";
            

        }
        public ucSmestajIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj smestaj";
            potvrdi.Content = "Dodaj";
        }


        public event EventHandler VratiSeNa_Smestaj;

        private void Vrati_Se(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Smestaj?.Invoke(this, EventArgs.Empty);

        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            SmestajArgs args = new SmestajArgs();

            smestaj.Naziv = naziv.Hint;
           
            smestaj.Adresa = adresa.Hint;

            args.PovratnaVrednost = smestaj;

            Button b = (Button)sender;
            if (b.Content.ToString() == "Dodaj")
            {
                string oc = "";
                if (ocena.SelectedItem != null)
                    oc = ocena.SelectedItem.ToString();
                string ts = "";
                if (tip.SelectedItem != null)
                    ts = tip.SelectedItem.ToString();
                if (ValidateInput(smestaj.Naziv, ts, oc, smestaj.Adresa))
                {
                    smestaj.Tip = (TipSmestaja)Enum.Parse(typeof(TipSmestaja), (this.tip.SelectedItem as ComboBoxItem).Content.ToString());
                    string o = ocena.SelectedItem.ToString();
                    smestaj.Ocena = o[o.Length - 1].ToString();
                    smestaj.Id = ObjectId.GenerateNewId().ToString();
                    putovanjaServis.DodajSmestaj(smestaj);
                    VratiSeNa_Smestaj?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                if (ValidateInput(smestaj.Naziv,smestaj.Tip.ToString(), smestaj.Ocena.ToString(), smestaj.Adresa))
                {
                    smestaj.Tip = (TipSmestaja)Enum.Parse(typeof(TipSmestaja), (this.tip.SelectedItem as ComboBoxItem).Content.ToString());
                    string o = ocena.SelectedItem.ToString();
                    smestaj.Ocena = o[o.Length - 1].ToString();
                    putovanjaServis.IzmeniSmestaj(smestaj);

                    OkModule popupUserControl = new OkModule("Smestaj " + smestaj.Naziv + " je izmenjen.");

                    popup.Child = null;
                    popup.Child = popupUserControl;
                    popup.HorizontalOffset = 500;
                    popup.VerticalOffset = 570;
                    popup.Height = 180;
                    popup.Width = 400;
                    popup.AllowsTransparency = true;

                    popup.IsOpen = true;

                    popupUserControl.PotvrdiClicked += Zatvori;

                    VratiSeNa_Smestaj?.Invoke(this, EventArgs.Empty);
                    //MessageBox.Show($"Smestaj '{smestaj.Id}' je izmenjen.", "Smestaj izmenjen", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

          public void Zatvori(object sender, EventArgs e)
        {
            popup.IsOpen = false;
        }
        private bool ValidateInput(string naziv, string tip, string ocena, string adresa)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtErrorNaziv.Visibility = Visibility.Visible;
                txtErrorNaziv.Text = "Naziv ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(tip))
            {
                txtErrorNaziv.Visibility = Visibility.Collapsed;
                txtErrorTip.Visibility = Visibility.Visible;
                txtErrorTip.Text = "Tip ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(adresa))
            {
                txtErrorTip.Visibility = Visibility.Collapsed;
                txtErrorAdresa.Visibility = Visibility.Visible;
                txtErrorAdresa.Text = "Adresa ne sme biti prazna";
                return false;
            }

            if (string.IsNullOrWhiteSpace(ocena))
            {
                txtErrorAdresa.Visibility = Visibility.Collapsed;
                txtErrorOcena.Visibility = Visibility.Visible;
                txtErrorOcena.Text = "Ocena ne sme biti prazna";
                return false;
            }

            return true;
        }
    }
}
