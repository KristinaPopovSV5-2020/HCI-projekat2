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
        ObservableCollection<Putovanje> putovanja = new ObservableCollection<Putovanje>();

        public ucPutovanja()
        {
            InitializeComponent();
            
            /*putovanja.Add(new Putovanje("1","Putovanje1","4","4000",new DateTime()));
            putovanja.Add(new Putovanje("2", "Putovanje2", "5", "5000", new DateTime()));
            putovanja.Add(new Putovanje("3", "Putovanje3", "4", "6000", new DateTime()));
            putovanja.Add(new Putovanje("4", "Putovanje4", "3", "7000", new DateTime()));
            putovanja.Add(new Putovanje("5", "Putovanje5", "6", "1000", new DateTime()));
            putovanja.Add(new Putovanje("6", "Putovanje6", "4", "3000", new DateTime()));
            putovanja.Add(new Putovanje("7", "Putovanje7", "4", "4000", new DateTime()));
            putovanja.Add(new Putovanje("8", "Putovanje8", "4", "8000", new DateTime()));
            putovanja.Add(new Putovanje("9", "Putovanje9", "4", "4000", new DateTime()));
            putovanja.Add(new Putovanje("10", "Putovanje10", "4", "4000", new DateTime()));
            putovanja.Add(new Putovanje("11", "Putovanje11", "4", "4000", new DateTime()));*/

            putovanjaDataGrid.ItemsSource = putovanja;
        }

        private void Obrisi_Putovanje(object sender, RoutedEventArgs e)
        {
            Putovanje selectedItem = putovanjaDataGrid.SelectedItem as Putovanje;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš putovanje "+ selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    putovanja.Remove(selectedItem);
                    MessageBox.Show($"Putovanje '{selectedItem.Naziv}' je obrisano.", "Putovanje obrisano", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }
    }
}
