﻿using System;
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

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucSmestajIzmena.xaml
    /// </summary>
    public partial class ucSmestajIzmena : UserControl
    {
        public Smestaj smestaj = new Smestaj();

        public ucSmestajIzmena(string id, string naziv, string tip, string adresa, string ocena)
        {
            InitializeComponent();

            this.id.Hint = id;
            this.naziv.Hint = naziv;
            this.tip.Hint = tip.ToString();
            this.adresa.Hint = adresa;
            this.ocena.Hint = ocena;

            smestaj.Id = this.id.Hint;
            smestaj.Naziv = this.naziv.Hint;
            smestaj.Tip = (TipSmestaja)Enum.Parse(typeof(TipSmestaja), this.tip.Hint);
            smestaj.Adresa = this.adresa.Hint;
            smestaj.Ocena = this.ocena.Hint;

            naslov.Text = "Izmeni smestaj";
            potvrdi.Content = "Izmeni";
            this.id.IsEnabled = false;

        }
        public ucSmestajIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj smestaj";
            potvrdi.Content = "Dodaj";
        }


        public event EventHandler VratiSeNa_Smestaj;
        public event EventHandler<SmestajArgs> Dodaj_Smestaj;
        public event EventHandler<SmestajArgs> Izmeni_Smestaj;

        private void Vrati_Se(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Smestaj?.Invoke(this, EventArgs.Empty);

        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            SmestajArgs args = new SmestajArgs();

            smestaj.Id = id.Hint;
            smestaj.Naziv = naziv.Hint;
            smestaj.Tip = (TipSmestaja)Enum.Parse(typeof(TipSmestaja), tip.Hint);
            smestaj.Adresa = adresa.Hint;
            smestaj.Ocena = ocena.Hint;

            args.PovratnaVrednost = smestaj;

            Button b = (Button)sender;
            if (b.Content.ToString() == "Dodaj")
            {
                Dodaj_Smestaj?.Invoke(this, args);
            }
            else
            {
                Izmeni_Smestaj?.Invoke(this, args);
                MessageBox.Show($"Smestaj '{smestaj.Id}' je izmenjen.", "Smestaj izmenjen", MessageBoxButton.OK, MessageBoxImage.Information);
            }




        }

    }
}