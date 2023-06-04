using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
    public partial class detaljiPrikaz : UserControl
    {

        PutovanjaServis putovanjaServis = new PutovanjaServis();
        List<Atrakcija> atrakcijas = new List<Atrakcija>();
        List<Smestaj> smestaji = new List<Smestaj>();
        List<Restoran> restorani = new List<Restoran>();


        public detaljiPrikaz()
        {
            InitializeComponent();
            //atrakcijeExpander.PreviewMouseLeftButtonUp += Atrakcije_Click;
            //smestajExpander.PreviewMouseLeftButtonUp += Smestaji_Click;
            //restoraniExpander.PreviewMouseLeftButtonUp += Restorani_Click;
        }

        internal async void LoadItemDetails(Putovanje selectedItem)
        {
            nameInput.Text = selectedItem.Naziv;
            daysInput.Text = " Broj dana: "+ selectedItem.BrojDana;
            startInput.Text = " Polazak: " + selectedItem.Polazak;
            //listaAtrakcija.ItemsSource = selectedItem.Atrakcije;

            atrakcijas = selectedItem.Atrakcije;
            smestaji = selectedItem.Smestaji;
            restorani = selectedItem.Restorani;
            listaAtrakcija.ItemsSource = atrakcijas;
            listaSmestaja.ItemsSource = smestaji;
            listaRestorana.ItemsSource = restorani;
            Canvas group2 = new Canvas();
            await SearchAndAddPushpin(atrakcijas.Select(a => a.Adresa).ToList());
            await SearchAndAddPushpin(smestaji.Select(a => a.Adresa).ToList(), 1);
            await SearchAndAddPushpin(restorani.Select(a => a.Adresa).ToList(), 2);


        }

        public event EventHandler VratiSeNa_Putovanja;


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Putovanja?.Invoke(this, EventArgs.Empty);
        }


        private void Pushpin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;

            Pushpin clickedPin = (Pushpin)sender;
            string additionalInfo = clickedPin.Tag.ToString();
            popupText.Text = additionalInfo;
            pinPopup.IsOpen = true;
        }

        private void Pin_MouseEnter(object sender, MouseEventArgs e)
        {
            Pushpin clickedPin = (Pushpin)sender;
            string additionalInfo = clickedPin.Tag.ToString();
            popupText.Text = additionalInfo;
            pinPopup.IsOpen = true;
        }

        private async void Pin_MouseLeaveAsync(object sender, MouseEventArgs e)
        {
            await Task.Delay(1000);
            pinPopup.IsOpen = false;
        }

        private void ListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (sender as ListBoxItem).DataContext;
            int pinIndex = -1;
            if (selectedItem is Restoran)
            {
                pinIndex = atrakcijas.Count + smestaji.Count;
                pinIndex += listaRestorana.Items.IndexOf(selectedItem);
            }
            else if (selectedItem is Smestaj)
            {
                pinIndex = atrakcijas.Count;
                pinIndex += listaSmestaja.Items.IndexOf(selectedItem);
            }
            else if (selectedItem is Atrakcija)
            {
                pinIndex = listaAtrakcija.Items.IndexOf(selectedItem);
            }

            if (pinIndex >= 0 && pinIndex < map.Children.Count)
            {
                var pin = map.Children[pinIndex] as Pushpin;
                var pinLocation = new Location(pin.Location.Latitude, pin.Location.Longitude);
                // Set the map view to the selected pin location
                map.SetView(pinLocation, 15);
            }
        }





        private async Task SearchAndAddPushpin(List<string> adrese, int list = 0)
        {
            if (adrese.Count > 0)
            {
                int i = 0;
                List<Location> locations = new List<Location>();
                foreach(string address in adrese)
                {
                    string requestUrl = $"http://dev.virtualearth.net/REST/v1/Locations?query={address}&key=nltGyIgw8vEO79Dc9AFY~00N7BTnXTXYeNf3EVNeDNw~AkowgGH4IsDZEM9SmtVES0nD2OD-cD9VWqNSC8e29PF4zPvYoWnCedQQgoaJiDkr";

                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(requestUrl);
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<RootObject>(json);

                        if (data != null && data.resourceSets.Any() && data.resourceSets.First().resources.Any())
                        {
                            var location = data.resourceSets.First().resources.First();
                            var point = new Location(location.point.coordinates[0], location.point.coordinates[1]);
                            locations.Add(point);
                            var pushpin = new Pushpin();
                            if (list == 0)
                            {
                                pushpin.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EC8236"));
                                pushpin.Tag = atrakcijas[i].Naziv + "\n" + atrakcijas[i].Adresa;
                            }
                            else if (list == 1)
                            {
                                pushpin.Background = new SolidColorBrush(Colors.LightSeaGreen);
                                pushpin.Tag = smestaji[i].Naziv + "\n" + smestaji[i].Adresa;
                            }
                            else
                            {
                                pushpin.Background = new SolidColorBrush(Colors.BlueViolet);
                                pushpin.Tag = restorani[i].Naziv + "\n" + restorani[i].Adresa;
                            }
                            pushpin.Location = point;
                            TextBlock textBlock = new TextBlock();
                            i++;
                            textBlock.Text = i.ToString();
                            pushpin.Content = textBlock;

                            pushpin.MouseEnter += Pin_MouseEnter;
                            pushpin.MouseLeave += Pin_MouseLeaveAsync;
                            Console.WriteLine(pushpin.Tag.ToString());
                            map.Children.Add(pushpin);
                        }
                    }
                }
                if (list == 2)
                {
                    var w = new Pushpin().Width;
                    var h = new Pushpin().Height;
                    var margin = new Thickness(w / 2, h, w / 2, 0);

                    map.SetView(locations, margin, 0);
                }
            }

            
        }

        public class Point
        {
            public string type { get; set; }
            public List<double> coordinates { get; set; }
        }

        public class Resource
        {
            public string __type { get; set; }
            public List<double> bbox { get; set; }
            public string name { get; set; }
            public Point point { get; set; }
        }

        public class ResourceSet
        {
            public int estimatedTotal { get; set; }
            public List<Resource> resources { get; set; }
        }

        public class RootObject
        {
            public string authenticationResultCode { get; set; }
            public string brandLogoUri { get; set; }
            public int statusCode { get; set; }
            public string statusDescription { get; set; }
            public string traceId { get; set; }
            public List<ResourceSet> resourceSets { get; set; }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            /*Expander expander = sender as Expander;

            if (expander.IsExpanded)
            {
                if (expander == atrakcijeExpander)
                {
                    SearchAndAddPushpin(atrakcijas.Select(a => a.Adresa).ToList(), 0);
                }
                else if (expander == smestajExpander)
                {
                    SearchAndAddPushpin(smestaji.Select(a => a.Adresa).ToList(), 1);
                }
                else if (expander == restoraniExpander)
                {
                    SearchAndAddPushpin(restorani.Select(a => a.Adresa).ToList(), 2);
                }
            }*/
        }

        private void ListBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is ListBoxItem)
            {
                e.Handled = true;
            }
        }


        public void Kupi_Click(object sender, RoutedEventArgs e)
        {
        }
        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }





    }
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListBoxItem item = (ListBoxItem)value;
            ListBox listView = ItemsControl.ItemsControlFromItemContainer(item) as ListBox;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}





