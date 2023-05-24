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
    /// Interaction logic for ucSmestaji.xaml
    /// </summary>
    public partial class ucSmestaji : UserControl
    {
        ObservableCollection<Smestaj> smestaji = new ObservableCollection<Smestaj>();
        public ucSmestaji()
        {
            InitializeComponent();

            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));
            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));
            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));
            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));
            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));
            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "opis"));

            smestajiDataGrid.ItemsSource = smestaji;
        }

        private void Obrisi_Smestaj(object sender, RoutedEventArgs e)
        {
            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš smestaj " + selectedItem.Ime, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    smestaji.Remove(selectedItem);
                    MessageBox.Show($"Smestaj '{selectedItem.Ime}' je obrisan.", "Smestaj obrisan", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }
    }
}
