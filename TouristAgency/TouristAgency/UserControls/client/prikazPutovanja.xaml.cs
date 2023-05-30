using BingMapsRESTToolkit;
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


namespace TouristAgency.UserControls.client
{
    /// <summary>
    /// Interaction logic for prikazPutovanja.xaml
    /// </summary>
    public partial class prikazPutovanja : UserControl
    {
        ObservableCollection<Putovanje> Putovanja;


        public prikazPutovanja()
        {
            InitializeComponent();
            Putovanja = new ObservableCollection<Putovanje>();

            Putovanja.Add(new Putovanje("1", "Ime1", "10", "10din", new DateTime().Date));
            Putovanja.Add(new Putovanje("1", "Ime1", "10", "10din", new DateTime().Date));
            Putovanja.Add(new Putovanje("1", "Ime1", "10", "10din", new DateTime().Date));
            listaPutovanja.ItemsSource = Putovanja;
        }


        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPutovanja.SelectedItem != null)
            {
                Putovanje selectedItem = listaPutovanja.SelectedItem as Putovanje;
                detaljiPrikaz itemDetailsControl = new detaljiPrikaz();
                containerGrid.Children.Clear();
                containerGrid.Children.Add(itemDetailsControl);
                itemDetailsControl.LoadItemDetails(selectedItem);
            }
        }



    }
}
