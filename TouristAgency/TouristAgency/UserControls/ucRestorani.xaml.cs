using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Restoran> restorani = new ObservableCollection<Restoran>();
        public ucRestorani()
        {
            InitializeComponent();

            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));
            restorani.Add(new Restoran("1", "Naziv1", "Lokacija1"));

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
    }
}
