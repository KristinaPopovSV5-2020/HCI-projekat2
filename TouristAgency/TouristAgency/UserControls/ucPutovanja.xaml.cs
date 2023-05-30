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
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    
    public partial class ucPutovanja : UserControl
    {
        
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        ObservableCollection<Putovanje> putovanja;

        public ucPutovanja()
        {
            InitializeComponent();

            putovanja = new ObservableCollection<Putovanje>();

            putovanja.Add(new Putovanje("1", "Naziv1", "4", "5000", new DateTime()));
            putovanja.Add(new Putovanje("1", "Naziv1", "4", "5", new DateTime()));
            putovanja.Add(new Putovanje("1", "Naziv1", "5", "5", new DateTime()));
            putovanja.Add(new Putovanje("1", "Naziv1", "6", "5", new DateTime()));
            putovanja.Add(new Putovanje("1", "Naziv1", "3", "5", new DateTime()));
            putovanja.Add(new Putovanje("1", "Naziv1", "4", "5", new DateTime()));

            putovanjaDataGrid.ItemsSource = putovanja;
        }

        private void Obrisi_Putovanje(object sender, RoutedEventArgs e)
        {
            Putovanje selectedItem = putovanjaDataGrid.SelectedItem as Putovanje;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš putovanje "+selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    putovanja.Remove(selectedItem);
                    MessageBox.Show($"Putovanje '{selectedItem.Naziv}' je obrisano.", "Putovanje obrisano", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }
    }
}
