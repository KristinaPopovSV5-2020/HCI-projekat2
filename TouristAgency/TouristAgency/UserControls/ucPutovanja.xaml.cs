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
using TouristAgency.UserControls.client;

namespace TouristAgency.UserControls
{
    
    public partial class ucPutovanja : UserControl
    {
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        ObservableCollection<Putovanje> putovanja;
        Putovanje zaBrisanje;


        public ucPutovanja()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                putovanja = new ObservableCollection<Putovanje>(putovanjaServis.PronadjiPutovanja());
                putovanjaDataGrid.ItemsSource = putovanja;
            };

            putovanjaDataGrid.ItemsSource = putovanja;
        }

        private void Obrisi_Putovanje(object sender, RoutedEventArgs e)
        {
            Putovanje selectedItem = putovanjaDataGrid.SelectedItem as Putovanje;

            if (selectedItem != null)
            {
                zaBrisanje = selectedItem;
                YesNoModule popupUserControl = new YesNoModule("Da li sigurno želiš da izbrišeš putovanje " + selectedItem.Naziv);

                mainComponent.IsHitTestVisible = false;
                mainComponent.Opacity = 0.4;

                popup.Child = null;
                popup.Child = popupUserControl;
                popup.Width = 500;
                popup.Height = 200;
                popup.HorizontalOffset = -100;
                popup.AllowsTransparency = true;

                popup.IsOpen = true;

                popupUserControl.PotvrdiClicked += Obrisi;
                popupUserControl.OdustaniClicked += Odustani;

            }

        }

        public void Obrisi(object sender, EventArgs e)
        {
            
            putovanja.Remove(zaBrisanje);
            putovanjaDataGrid.Items.Refresh();
            putovanjaServis.ObrisiPutovanje(zaBrisanje);

            popup.IsOpen = false;

            OkModule popupUserControl = new OkModule("Putovanje " + zaBrisanje.Naziv + " je obrisano.");

            popup.Width = 400;
            mainComponent.Opacity = 0.4;
            popup.Height = 180;
            popup.Child = null;
            popup.Child = popupUserControl;

            popup.HorizontalOffset = -100;
            popup.AllowsTransparency = true;


            popup.IsOpen = true;

            popupUserControl.PotvrdiClicked += Zatvori;
        }

        public void Zatvori(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
            mainComponent.Opacity = 1;
        }

        public void Odustani(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
            mainComponent.Opacity = 1;
        }

        private void Dodaj_Putovanje(object sender, RoutedEventArgs e)
        {
            ucNovoPutovanje forma = new ucNovoPutovanje();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Putovanja += Vrati;


        }


        private void PretraziPutovanja(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = putovanja.Where(item => item.Naziv.ToLower().Contains(searchText) || item.BrojDana.ToLower().Contains(searchText) || item.Datum.ToShortDateString().ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText) || item.Cena.ToString().ToLower().Contains(searchText));

            putovanjaDataGrid.ItemsSource = filteredItems;
        }


        private void Vrati(object sender, EventArgs e)
        {
            ucPutovanja puto = new ucPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(puto);


        }

        private void Izmeni_Putovanje(object sender, RoutedEventArgs e)
        {
            Putovanje selectedItem = putovanjaDataGrid.SelectedItem as Putovanje;
            ucNovoPutovanje forma = new ucNovoPutovanje(selectedItem.Id, selectedItem.Naziv, selectedItem.Cena, selectedItem.BrojDana, selectedItem.Datum, new ObservableCollection<Atrakcija>(selectedItem.Atrakcije), new ObservableCollection<Smestaj>(selectedItem.Smestaji), new ObservableCollection<Restoran>(selectedItem.Restorani));
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Putovanja += Vrati; 


        }

        private void Vidi_Putovanje(object sender, RoutedEventArgs e)
        {
            Putovanje selectedItem = putovanjaDataGrid.SelectedItem as Putovanje;
            detaljiPrikaz detaljiPrikaz= new detaljiPrikaz(false);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(detaljiPrikaz);
            detaljiPrikaz.LoadItemDetails(selectedItem);
            detaljiPrikaz.VratiSeNa_Putovanja += Vrati;

        }
    }
}
