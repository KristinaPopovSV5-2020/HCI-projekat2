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
    /// Interaction logic for ucAtrakcije.xaml
    /// </summary>
    public partial class ucAtrakcije : UserControl
    {
        ObservableCollection<Atrakcija> atrakcije = new ObservableCollection<Atrakcija>();
        public ucAtrakcije()
        {
            InitializeComponent();

            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));
            atrakcije.Add(new Atrakcija("1", "Atrakcija1", "Opis atrakcije"));

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
    }
}
