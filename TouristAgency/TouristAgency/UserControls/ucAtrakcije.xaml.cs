using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for ucAtrakcije.xaml
    /// </summary>
    public partial class ucAtrakcije : UserControl
    {
        public ObservableCollection<Atrakcija> atrakcije;
        public ucAtrakcije()
        {
            InitializeComponent();

            atrakcije = new ObservableCollection<Atrakcija>();

            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije","Adresa1"));
            atrakcije.Add(new Atrakcija("2", "Atrakcija1", "Opis atrakcije", "Adresa1"));
            atrakcije.Add(new Atrakcija("3", "Atrakcija1", "Opis atrakcije", "Adresa1"));
            atrakcije.Add(new Atrakcija("4", "Atrakcija1", "Opis atrakcije", "Adresa1"));
            atrakcije.Add(new Atrakcija("5", "Atrakcija1", "Opis atrakcije", "Adresa1"));
            atrakcije.Add(new Atrakcija("6", "Atrakcija1", "Opis atrakcije", "Adresa1"));
            atrakcije.Add(new Atrakcija("7", "Atrakcija1", "Opis atrakcije", "Adresa1"));

            atrakcijeDataGrid.ItemsSource = atrakcije;
        }

        private void Obrisi_Atrakciju(object sender, RoutedEventArgs e)
        {
            Atrakcija selectedItem = atrakcijeDataGrid.SelectedItem as Atrakcija;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš atrakciju " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    atrakcije.Remove(selectedItem);
                    MessageBox.Show($"Atrakcija '{selectedItem.Naziv}' je obrisana.", "Atrakcija obrisana", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }

        private void Izmeni_Atrakciju(object sender, RoutedEventArgs e)
        {

            Atrakcija selectedItem = atrakcijeDataGrid.SelectedItem as Atrakcija;

            ucAtrakcijaIzmena forma = new ucAtrakcijaIzmena(selectedItem.Id,selectedItem.Naziv,selectedItem.Opis,selectedItem.Adresa);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Izmeni_Atrakciju += Promeni;
            forma.VratiSeNa_Atrakcije += Vrati;




        }

        private void Dodaj_Atrakciju(object sender, RoutedEventArgs e)
        {
            ucAtrakcijaIzmena forma = new ucAtrakcijaIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Dodaj_Atrakciju += Ucitaj;
            forma.VratiSeNa_Atrakcije += Vrati;
            

        }

        private void Ucitaj(object sender, AtrakcijaArgs e)
        {
            ucAtrakcije atr = new ucAtrakcije();
            atr.atrakcije.Add(e.PovratnaVrednost);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Promeni(object sender, AtrakcijaArgs e)
        {
            ucAtrakcije atr = new ucAtrakcije();
            Atrakcija atrakcijaZaPromenu = atr.atrakcije.FirstOrDefault(item => item.Id == e.PovratnaVrednost.Id);
            atrakcijaZaPromenu.Id = e.PovratnaVrednost.Id;
            atrakcijaZaPromenu.Naziv = e.PovratnaVrednost.Naziv;
            atrakcijaZaPromenu.Opis = e.PovratnaVrednost.Opis;
            atrakcijaZaPromenu.Adresa = e.PovratnaVrednost.Adresa;

            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Vrati(object sender, EventArgs e)
        {
            ucAtrakcije atr = new ucAtrakcije();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }
    }
}
