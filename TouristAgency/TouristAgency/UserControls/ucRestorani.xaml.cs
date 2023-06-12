using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        ObservableCollection<Restoran> trenutniRestorani;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        Restoran zaBrisanje;
        public ucRestorani()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                restorani = await putovanjaServis.SviRestoraniAsync();
                trenutniRestorani = restorani;
                restoraniDataGrid.ItemsSource = restorani;
            };

        }

        private void Obrisi_Restoran(object sender, RoutedEventArgs e)
        {
            Restoran selectedItem = restoraniDataGrid.SelectedItem as Restoran;
            zaBrisanje = selectedItem;

            if (selectedItem != null)
            {
                YesNoModule popupUserControl = new YesNoModule("Da li sigurno želiš da izbrišeš restoran " + selectedItem.Naziv);

                mainComponent.IsHitTestVisible = false;
                mainComponent.Opacity = 0.4;

                popup.Child = null;
                popup.Child = popupUserControl;

                popup.HorizontalOffset = -100;
                popup.AllowsTransparency = true;

                popup.IsOpen = true;

                popupUserControl.PotvrdiClicked += Obrisi;
                popupUserControl.OdustaniClicked += Odustani;
                //MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš restoran " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

            }

        }

        public void Obrisi(object sender,EventArgs e)
        {
            putovanjaServis.ObrisiRestoran(zaBrisanje);
            restorani.Remove(zaBrisanje);
            restoraniDataGrid.Items.Refresh();

            popup.IsOpen = false;

            OkModule popupUserControl = new OkModule("Resotran " + zaBrisanje.Naziv + " je obrisan.");

            popup.Width = 200;
            popup.Height = 200;
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
            popup.IsOpen = false;
            mainComponent.Opacity = 1;
            mainComponent.IsHitTestVisible = true;

        }

        private void PretraziRestorane(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = trenutniRestorani.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Ocena.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText));

            restoraniDataGrid.ItemsSource = filteredItems;
        }

        private void Filtriraj(object sender, MouseButtonEventArgs e)
        {
            filterRestorani popupUserControl = new filterRestorani();

            mainComponent.IsHitTestVisible = false;
            mainComponent.Opacity = 0.4;

            popup.Child = null;
            popup.Child = popupUserControl;
            popup.HorizontalOffset = 500;
            popup.VerticalOffset = 570;
            popup.Height = 350;
            popup.Width = 400;
            popup.AllowsTransparency = true;

            popup.IsOpen = true;

            popupUserControl.VratiSeNa_Smestaj += Vrati;

            popupUserControl.Button2Clicked += async (sender, e) =>
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    UcitajFiltrirano(e.PovratnaVrednost);
                    popup.IsOpen = false;
                    mainComponent.Opacity = 1;
                    mainComponent.IsHitTestVisible = true;
                });
            };


        }

        public void UcitajFiltrirano(ObservableCollection<Restoran> filteredList)
        {
            trenutniRestorani = filteredList;
            restoraniDataGrid.ItemsSource = trenutniRestorani;
        }


    }
}
