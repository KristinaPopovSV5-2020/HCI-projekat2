using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
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
using TouristAgency.Helper;
using TouristAgency.Model;
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucAtrakcije.xaml
    /// </summary>
    public partial class ucAtrakcije : UserControl
    {
        public ObservableCollection<Atrakcija> atrakcije;
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        Atrakcija zaBrisanje;
        public ucAtrakcije()
        {
            InitializeComponent();

            Loaded += async (sender, e) =>
            {
                atrakcije=await putovanjaServis.SveAtrakcijeAsync();
                atrakcijeDataGrid.ItemsSource = atrakcije;
            };
        }

        private void Obrisi_Atrakciju(object sender, RoutedEventArgs e)
        {
            Atrakcija selectedItem = atrakcijeDataGrid.SelectedItem as Atrakcija;
            zaBrisanje = selectedItem;

            if (selectedItem != null)
            {
                YesNoModule popupUserControl = new YesNoModule("Da li sigurno želiš da izbrišeš atrakciju " + selectedItem.Naziv);

                mainComponent.IsHitTestVisible = false;
                mainComponent.Opacity = 0.4;
                popup.Width = 570;
                popup.Height = 180;
                popup.Child = null;
                popup.Child = popupUserControl;

                popup.HorizontalOffset = -100;
                popup.AllowsTransparency = true;

                popup.IsOpen = true;

                popupUserControl.PotvrdiClicked += Obrisi;
                popupUserControl.OdustaniClicked += Odustani;

                //MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš atrakciju " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);


            }

        }

        private void Obrisi(object sender, EventArgs e)
        {
            putovanjaServis.ObrisiAtrakciju(zaBrisanje);
            atrakcije.Remove(zaBrisanje);
            atrakcijeDataGrid.Items.Refresh();
            popup.IsOpen = false;

            //MessageBox.Show($"Atrakcija '{zaBrisanje.Naziv}' je obrisana.", "Atrakcija obrisana", MessageBoxButton.OK, MessageBoxImage.Information);
            OkModule popupUserControl = new OkModule("Atrakcija " + zaBrisanje.Naziv + " je obrisana.");



            popup.Width = 400;
            popup.Height = 200;

            mainComponent.IsHitTestVisible = false;
            mainComponent.Opacity = 0.4;

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

        private void Izmeni_Atrakciju(object sender, RoutedEventArgs e)
        {

            Atrakcija selectedItem = atrakcijeDataGrid.SelectedItem as Atrakcija;
            ucAtrakcijaIzmena forma = new ucAtrakcijaIzmena(selectedItem.Id,selectedItem.Naziv,selectedItem.Opis,selectedItem.Adresa);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Atrakcije += Vrati;




        }

        private void Dodaj_Atrakciju(object sender, RoutedEventArgs e)
        {
            ucAtrakcijaIzmena forma = new ucAtrakcijaIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.VratiSeNa_Atrakcije += Vrati;
            

        }


        private void Vrati(object sender, EventArgs e)
        {
            ucAtrakcije atr = new ucAtrakcije();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }


        private void PretraziAtrakcije(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = atrakcije.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Opis.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText));

            atrakcijeDataGrid.ItemsSource = filteredItems;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                UserWindow userWindow = new UserWindow();
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, userWindow);
            }
        }

    }
}
