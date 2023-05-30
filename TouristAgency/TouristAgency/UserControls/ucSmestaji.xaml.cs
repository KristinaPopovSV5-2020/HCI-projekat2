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
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucSmestaji.xaml
    /// </summary>
    public partial class ucSmestaji : UserControl
    {
        ObservableCollection<Smestaj> smestaji;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public ucSmestaji()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                smestaji = await putovanjaServis.SviSmestajiAsync();
                smestajiDataGrid.ItemsSource = smestaji;
            };
        }

        private void Obrisi_Smestaj(object sender, RoutedEventArgs e)
        {
            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš smestaj " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    putovanjaServis.ObrisiSmestaj(selectedItem);
                    smestaji.Remove(selectedItem);
                    smestajiDataGrid.Items.Refresh();
                    MessageBox.Show($"Smestaj '{selectedItem.Naziv}' je obrisan.", "Smestaj obrisan", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }

        private void Izmeni_Smestaj(object sender, RoutedEventArgs e)
        {

            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            ucSmestajIzmena forma = new ucSmestajIzmena(selectedItem.Id, selectedItem.Naziv, selectedItem.Tip.ToString(), selectedItem.Adresa, selectedItem.Ocena);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Smestaj += Vrati;




        }

        private void Dodaj_Smestaj(object sender, RoutedEventArgs e)
        {
            ucSmestajIzmena forma = new ucSmestajIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Smestaj += Vrati;


        }


        private void Vrati(object sender, EventArgs e)
        {
            ucSmestaji atr = new ucSmestaji();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void PretraziSmestaje(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = smestaji.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Ocena.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText) || item.Tip.ToString().ToLower().Contains(searchText));

            smestajiDataGrid.ItemsSource = filteredItems;
        }
    }
}
