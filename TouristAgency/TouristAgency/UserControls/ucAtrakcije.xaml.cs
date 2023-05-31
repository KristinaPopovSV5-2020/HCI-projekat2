using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucAtrakcije.xaml
    /// </summary>
    public partial class ucAtrakcije : UserControl
    {
        public ObservableCollection<Atrakcija> atrakcije;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public ucAtrakcije()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                atrakcije=await putovanjaServis.SveAtrakcijeAsync();
                atrakcijeDataGrid.ItemsSource = atrakcije;
            };
        }

        private void Obrisi_Atrakciju(object sender, RoutedEventArgs e)
        {
            Atrakcija selectedItem = atrakcijeDataGrid.SelectedItem as Atrakcija;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš atrakciju " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    putovanjaServis.ObrisiAtrakciju(selectedItem);
                    atrakcije.Remove(selectedItem);
                    atrakcijeDataGrid.Items.Refresh();
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

            forma.VratiSeNa_Atrakcije += Vrati;




        }

        private void Dodaj_Atrakciju(object sender, RoutedEventArgs e)
        {
            ucAtrakcijaIzmena forma = new ucAtrakcijaIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Atrakcije += Vrati;
            

        }


        private void Vrati(object sender, EventArgs e)
        {
            ucAtrakcije atr = new ucAtrakcije();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void PretraziAtrakcije(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = atrakcije.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Opis.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText));

            atrakcijeDataGrid.ItemsSource = filteredItems;
        }
    }
}
