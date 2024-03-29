﻿using MongoDB.Bson;
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
    /// Interaction logic for ucRestoranIzmena.xaml
    /// </summary>
    public partial class ucRestoranIzmena : UserControl
    {
        public Restoran restoran = new Restoran();
        PutovanjaServis putovanjaServis = new PutovanjaServis();

        public ucRestoranIzmena(string id, string naziv, string adresa, string ocena)
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

            this.naziv.Hint = naziv;
            this.adresa.Hint = adresa;
            this.restoran.Id = id;

            restoran.Naziv = this.naziv.Hint;
            restoran.Ocena = this.ocena.SelectedItem.ToString();
            restoran.Adresa = this.adresa.Hint;

            naslov.Text = "Izmeni restoran";
            potvrdi.Content = "Izmeni";
           

        }

        public ucRestoranIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj restoran";
            potvrdi.Content = "Dodaj";
        }

        public event EventHandler VratiSeNa_Restoran;

        private void Vrati_Se(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Restoran?.Invoke(this, EventArgs.Empty);

        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            RestoranArgs args = new RestoranArgs();

            restoran.Naziv = naziv.Hint;
           
            restoran.Adresa = adresa.Hint;

            args.PovratnaVrednost = restoran;

            Button b = (Button)sender;
            if (b.Content.ToString() == "Dodaj")
            {
                string oc = "";
                if (ocena.SelectedItem != null)
                    oc = ocena.SelectedItem.ToString();
                if (ValidateInput(restoran.Naziv, oc, restoran.Adresa))
                {
                    string o = ocena.SelectedItem.ToString();
                    restoran.Ocena = o[o.Length - 1].ToString();
                    restoran.Id = ObjectId.GenerateNewId().ToString();
                    putovanjaServis.DodajRestoran(restoran);
                    OkModule popupUserControl = new OkModule("Dodali ste restoran " + restoran.Naziv);
                    mainComponent.IsHitTestVisible = false;

                    popup.Child = null;
                    popup.Child = popupUserControl;
                    myUserControl.Opacity = 0.4;
                    popup.Height = 180;
                    popup.Width = 400;
                    popup.AllowsTransparency = true;

                    popup.IsOpen = true;

                    popupUserControl.PotvrdiClicked += Zatvori;
                    VratiSeNa_Restoran?.Invoke(this, EventArgs.Empty);
                }

            }
            else
            {
                if (ValidateInput(restoran.Naziv, restoran.Ocena.ToString(), restoran.Adresa))
                {
                    string o = ocena.SelectedItem.ToString();
                    restoran.Ocena = o[o.Length - 1].ToString();
                    putovanjaServis.IzmeniRestoran(restoran);

                    OkModule popupUserControl = new OkModule("Restoran " + restoran.Naziv + " je izmenjen.");

                    mainComponent.IsHitTestVisible = false;
                    mainComponent.Opacity = 0.4;
                    popup.Child = null;
                    popup.Child = popupUserControl;
                    popup.Height = 180;
                    popup.Width = 400;
                    popup.AllowsTransparency = true;

                    popup.IsOpen = true;

                    popupUserControl.PotvrdiClicked += Zatvori;

                    // MessageBox.Show($"Restoran '{restoran.Id}' je izmenjen.", "Restoran izmenjen", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        public void Zatvori(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
            VratiSeNa_Restoran?.Invoke(this, EventArgs.Empty);
            myUserControl.Opacity = 1;
        }

        private bool ValidateInput(string naziv, string ocena, string adresa)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtErrorNaziv.Visibility = Visibility.Visible;
                txtErrorNaziv.Text = "Naziv ne sme biti prazan";
                return false;
            }

            if (string.IsNullOrWhiteSpace(adresa))
            {
                txtErrorNaziv.Visibility = Visibility.Collapsed;
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
