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

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucRestoranIzmena.xaml
    /// </summary>
    public partial class ucRestoranIzmena : UserControl
    {
        public Restoran restoran = new Restoran();


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

            this.id.Hint = id;
            this.naziv.Hint = naziv;
            this.adresa.Hint = adresa;

            restoran.Id = this.id.Hint;
            restoran.Naziv = this.naziv.Hint;
            restoran.Ocena = this.ocena.SelectedItem.ToString();
            restoran.Adresa = this.adresa.Hint;

            naslov.Text = "Izmeni restoran";
            potvrdi.Content = "Izmeni";
            this.id.IsEnabled = false;
           

        }

        public ucRestoranIzmena()
        {
            InitializeComponent();

            naslov.Text = "Dodaj restoran";
            potvrdi.Content = "Dodaj";
            this.id.Visibility = Visibility.Hidden;
            this.idLabela.Visibility = Visibility.Hidden;
        }

        public event EventHandler VratiSeNa_Restoran;
        public event EventHandler<RestoranArgs> Dodaj_Restoran;
        public event EventHandler<RestoranArgs> Izmeni_Restoran;

        private void Vrati_Se(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Restoran?.Invoke(this, EventArgs.Empty);

        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            RestoranArgs args = new RestoranArgs();

            restoran.Id = id.Hint;
            restoran.Naziv = naziv.Hint;
            string o=ocena.SelectedItem.ToString();
            restoran.Ocena = o[o.Length - 1].ToString();
            restoran.Adresa = adresa.Hint;

            args.PovratnaVrednost = restoran;

            Button b = (Button)sender;
            if (b.Content.ToString() == "Dodaj")
            {
                Dodaj_Restoran?.Invoke(this, args);
            }
            else
            {
                Izmeni_Restoran?.Invoke(this, args);
                MessageBox.Show($"Restoran '{restoran.Id}' je izmenjen.", "Restoran izmenjen", MessageBoxButton.OK, MessageBoxImage.Information);
            }




        }
    }
}
