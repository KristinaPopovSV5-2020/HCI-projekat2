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
    /// Interaction logic for ucSmestaji.xaml
    /// </summary>
    public partial class ucSmestaji : UserControl
    {
        ObservableCollection<Smestaj> smestaji;
        ObservableCollection<Smestaj> trenutniSmestaji;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        Popup popup = new Popup();
        Popup popup1 = new Popup();
        Smestaj zaBrisanje;
        public ucSmestaji()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                smestaji = await putovanjaServis.SviSmestajiAsync();
                trenutniSmestaji = smestaji;
                smestajiDataGrid.ItemsSource = smestaji;
            };
        }

        private void Obrisi_Smestaj(object sender, RoutedEventArgs e)
        {
            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;
            zaBrisanje = selectedItem;

            if (selectedItem != null)
            {
                YesNoModule popupUserControl = new YesNoModule("Da li sigurno želiš da izbrišeš smestaj " + selectedItem.Naziv);

                mainComponent.IsHitTestVisible = false;
                mainComponent.Opacity = 0.4;

                popup.Child = null;
                popup.Child = popupUserControl;
                popup.HorizontalOffset = 500;
                popup.VerticalOffset = 570;
                popup.Height = 180;
                popup.Width = 400;
                popup.AllowsTransparency = true;

                popup.IsOpen = true;

                popupUserControl.PotvrdiClicked += Obrisi;
                popupUserControl.OdustaniClicked += Odustani;
               

            }

        }

        public void Obrisi(object sender, EventArgs e)
        {
            putovanjaServis.ObrisiSmestaj(zaBrisanje);
            smestaji.Remove(zaBrisanje);
            smestajiDataGrid.Items.Refresh();

            popup.IsOpen = false;

            OkModule popupUserControl = new OkModule("Smestaj " + zaBrisanje.Naziv + " je obrisan.");

            popup1.Child = null;
            popup1.Child = popupUserControl;
            popup1.HorizontalOffset = 500;
            popup1.VerticalOffset = 570;
            popup1.Height = 180;
            popup1.Width = 400;
            popup1.AllowsTransparency = true;

            popup1.IsOpen = true;

            popupUserControl.PotvrdiClicked += Zatvori;
        }

        public void Zatvori(object sender, EventArgs e)
        {
            popup1.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
            mainComponent.Opacity = 1;
        }

        public void Odustani(object sender, EventArgs e)
        {
            popup.IsOpen = false;
            mainComponent.IsHitTestVisible = true;
            mainComponent.Opacity = 1;
        }


        private void Izmeni_Smestaj(object sender, RoutedEventArgs e)
        {

            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            ucSmestajIzmena forma = new ucSmestajIzmena(selectedItem.Id, selectedItem.Naziv, selectedItem.Tip.ToString(), selectedItem.Adresa, selectedItem.Ocena);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Smestaj += Vrati;



        }

        private void Dodaj_Smestaj(object sender, RoutedEventArgs e)
        {
            ucSmestajIzmena forma = new ucSmestajIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Smestaj += Vrati;


        }


        private void Vrati(object sender, EventArgs e)
        {
            ucSmestaji atr = new ucSmestaji();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);
            popup.IsOpen = false;
            mainComponent.Opacity = 1;
            mainComponent.IsHitTestVisible = true;

        }

        private void PretraziSmestaje(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = trenutniSmestaji.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Ocena.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText) || item.Tip.ToString().ToLower().Contains(searchText));

            smestajiDataGrid.ItemsSource = filteredItems;
        }

        private void Filtriraj(object sender, MouseButtonEventArgs e)
        {
            filterAtrakcije popupUserControl = new filterAtrakcije();

            mainComponent.IsHitTestVisible = false;
            mainComponent.Opacity = 0.4;

            popup.Child = null;
            popup.Child = popupUserControl;
            popup.HorizontalOffset = 500;
            popup.VerticalOffset = 570;
            popup.Height = 500;
            popup.Width = 400;
            popup.AllowsTransparency = true;

            popup.IsOpen = true;

            popupUserControl.VratiSeNa_Smestaj += Vrati;

            popupUserControl.Button2Clicked += async (sender,e) =>
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

        public void UcitajFiltrirano(ObservableCollection<Smestaj> filteredList)
        {
            trenutniSmestaji = filteredList;
            smestajiDataGrid.ItemsSource = trenutniSmestaji;
        }

    }
}
