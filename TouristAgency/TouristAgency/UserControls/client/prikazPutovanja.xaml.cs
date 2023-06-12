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
using System.Linq;


namespace TouristAgency.UserControls.client
{
    /// <summary>
    /// Interaction logic for prikazPutovanja.xaml
    /// </summary>
    public partial class prikazPutovanja : UserControl
    {
        List<Putovanje> ucitanaPutovanja = new List<Putovanje>();
        List<Putovanje> popularnaPutovanja = new List<Putovanje>();

        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public event EventHandler LoginForm;


        public prikazPutovanja()
        {
            InitializeComponent();
            ucitanaPutovanja = putovanjaServis.PronadjiPutovanja();
            listaPutovanja.ItemsSource = ucitanaPutovanja;

            izdvajamo.Visibility = Visibility.Visible;
        }

        private void PronadjiPopularneKupovine()
        {
            ucitanaPutovanja = putovanjaServis.PronadjiPopularneKupovine();
        }
        public prikazPutovanja(string username, string v)
        {
            InitializeComponent();
            izdvajamo.Visibility = Visibility.Collapsed;
            if (v=="rez")
            {
                ucitanaPutovanja = putovanjaServis.PronadjiRezervacije(username);
                listaPutovanja.ItemsSource = ucitanaPutovanja;
                this.title.Text = "Vaše rezervacije";
            } else
            {
                ucitanaPutovanja = putovanjaServis.PronadjiKupovine(username);
                listaPutovanja.ItemsSource = ucitanaPutovanja;
                this.title.Text = "Vaša istorija kupovine";
            }
            if (ucitanaPutovanja.Count == 0) {
                this.title.Text = "Nismo pronašli nijedan rezultat.";
            }


        }


        private void ItemListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaPutovanja.SelectedItem != null)
            {
                Putovanje selectedItem = listaPutovanja.SelectedItem as Putovanje;
                detalji.Children.Clear();
                detaljiPrikaz detaljiPrikaz = new detaljiPrikaz();
                if (this.title.Text == "Vaše rezervacije") {
                    detaljiPrikaz = new detaljiPrikaz(true);
                } else if(this.title.Text == "Vaša istorija kupovine")
                {
                    detaljiPrikaz = new detaljiPrikaz(false);
                }
                detalji.Children.Add(detaljiPrikaz);
                detaljiPrikaz.LoadItemDetails(selectedItem);
                putovanja.Visibility = Visibility.Collapsed;
                detalji.Visibility = Visibility.Visible;
                detaljiPrikaz.VratiSeNa_Putovanja += Vrati;
                detaljiPrikaz.LoginClick += CallLoginForm;
            }
        }

        private void Vrati(object sender, EventArgs e)
        {
            putovanja.Visibility = Visibility.Visible;
            listaPutovanja.SelectedItem = null;
            detalji.Visibility = Visibility.Collapsed;
        }

        private void CallLoginForm(object sender, EventArgs e)
        {
            LoginForm?.Invoke(this, EventArgs.Empty);
        }

        private void PretraziPutovanja(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = ucitanaPutovanja.Where(item => item.Naziv.ToLower().Contains(searchText) || item.BrojDana.ToLower().Contains(searchText) || item.Datum.ToShortDateString().ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText) || item.Cena.ToString().ToLower().Contains(searchText));

            listaPutovanja.ItemsSource = filteredItems;
        }

        private void izdvajamo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PronadjiPopularneKupovine();
            listaPutovanja.ItemsSource = ucitanaPutovanja;
            topTri.Visibility = Visibility.Visible;
            izdvajamo.Foreground = Brushes.Red;
        }

        private void title_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucitanaPutovanja = putovanjaServis.PronadjiPutovanja();
            listaPutovanja.ItemsSource = ucitanaPutovanja;
            topTri.Visibility = Visibility.Collapsed;
            string colorCode = "#121518";
            Color color = (Color)ColorConverter.ConvertFromString(colorCode);
            Brush brush = new SolidColorBrush(color);
            izdvajamo.Foreground = brush;
        }
    }
}
