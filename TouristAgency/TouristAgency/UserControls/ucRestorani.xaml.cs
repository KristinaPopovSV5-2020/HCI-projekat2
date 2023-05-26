using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for ucRestorani.xaml
    /// </summary>
    public partial class ucRestorani : UserControl
    {
        ObservableCollection<Restoran> restorani;
        public ucRestorani()
        {
            InitializeComponent();

            restorani = new ObservableCollection<Restoran>();

            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1","5"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1", "5"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1", "5"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1", "5"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1", "5"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1", "5"));

            restoraniDataGrid.ItemsSource = restorani;
        }

        private void Obrisi_Restoran(object sender, RoutedEventArgs e)
        {
            Restoran selectedItem = restoraniDataGrid.SelectedItem as Restoran;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš restoran " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    restorani.Remove(selectedItem);
                    MessageBox.Show($"Restoran '{selectedItem.Naziv}' je obrisan.", "Restoran obrisan", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }

        private void Izmeni_Restoran(object sender, RoutedEventArgs e)
        {

            Restoran selectedItem = restoraniDataGrid.SelectedItem as Restoran;

            ucRestoranIzmena forma = new ucRestoranIzmena(selectedItem.Id, selectedItem.Naziv, selectedItem.Adresa, selectedItem.Ocena);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Izmeni_Restoran += Promeni;
            forma.VratiSeNa_Restoran += Vrati;




        }

        private void Dodaj_Restoran(object sender, RoutedEventArgs e)
        {
            ucRestoranIzmena forma = new ucRestoranIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Dodaj_Restoran += Ucitaj;
            forma.VratiSeNa_Restoran += Vrati;


        }

        private void Ucitaj(object sender, RestoranArgs e)
        {
            ucRestorani atr = new ucRestorani();
            atr.restorani.Add(e.PovratnaVrednost);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Promeni(object sender, RestoranArgs e)
        {
            ucRestorani atr = new ucRestorani();
            Restoran restoranZaPromenu = atr.restorani.FirstOrDefault(item => item.Id == e.PovratnaVrednost.Id);
            restoranZaPromenu.Id = e.PovratnaVrednost.Id;
            restoranZaPromenu.Naziv = e.PovratnaVrednost.Naziv;
            restoranZaPromenu.Ocena = e.PovratnaVrednost.Ocena;
            restoranZaPromenu.Adresa = e.PovratnaVrednost.Adresa;

            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Vrati(object sender, EventArgs e)
        {
            ucRestorani atr = new ucRestorani();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }
    }
}
