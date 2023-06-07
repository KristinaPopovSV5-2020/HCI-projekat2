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
using TouristAgency.Servis;

namespace TouristAgency.UserControls.client
{
    /// <summary>
    /// Interaction logic for prikazPutovanja.xaml
    /// </summary>
    public partial class prikazPutovanja : UserControl
    {
        ObservableCollection<Putovanje> Putovanja;
        PutovanjaServis putovanjaServis = new PutovanjaServis();


        public prikazPutovanja()
        {
            InitializeComponent();
            Putovanja = new ObservableCollection<Putovanje>();

            
            listaPutovanja.ItemsSource = putovanjaServis.PronadjiPutovanja();
        }


        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPutovanja.SelectedItem != null)
            {
                Putovanje selectedItem = listaPutovanja.SelectedItem as Putovanje;
                detalji.Children.Clear();
                detaljiPrikaz detaljiPrikaz = new detaljiPrikaz();
                detalji.Children.Add(detaljiPrikaz);
                detaljiPrikaz.LoadItemDetails(selectedItem);
                putovanja.Visibility = Visibility.Collapsed;
                detalji.Visibility = Visibility.Visible;
                detaljiPrikaz.VratiSeNa_Putovanja += Vrati;
            }
        }

        private void Vrati(object sender, EventArgs e)
        {
            putovanja.Visibility = Visibility.Visible;
            listaPutovanja.SelectedItem = null;
            detalji.Visibility = Visibility.Collapsed;
        }


    }
}
