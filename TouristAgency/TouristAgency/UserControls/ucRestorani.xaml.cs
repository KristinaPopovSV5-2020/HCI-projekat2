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
    /// Interaction logic for ucRestorani.xaml
    /// </summary>
    public partial class ucRestorani : UserControl
    {
        ObservableCollection<Restoran> restorani;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public ucRestorani()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                restorani = await putovanjaServis.SviRestoraniAsync();
                restoraniDataGrid.ItemsSource = restorani;
            };
        }

        private void Obrisi_Restoran(object sender, RoutedEventArgs e)
        {
            Restoran selectedItem = restoraniDataGrid.SelectedItem as Restoran;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš restoran " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    putovanjaServis.ObrisiRestoran(selectedItem);
                    restorani.Remove(selectedItem);
                    restoraniDataGrid.Items.Refresh();
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

            forma.VratiSeNa_Restoran += Vrati;




        }

        private void Dodaj_Restoran(object sender, RoutedEventArgs e)
        {
            ucRestoranIzmena forma = new ucRestoranIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Restoran += Vrati;


        }

        private void Vrati(object sender, EventArgs e)
        {
            ucRestorani atr = new ucRestorani();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }
    }
}
