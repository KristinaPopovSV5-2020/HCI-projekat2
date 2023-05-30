using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public partial class detaljiPrikaz : UserControl
    {

        public detaljiPrikaz()
        {
            InitializeComponent();
        }

        internal void LoadItemDetails(Putovanje selectedItem)
        {
            nameInput.Text = selectedItem.Naziv;
            //listaAtrakcija.ItemsSource = selectedItem.Atrakcije;
            listaAtrakcija.ItemsSource = new List<string>
            {
                "New York",
                "Paris",
                "London",
                "Tokyo",
                "Rome"
            };
            SearchAndAddPushpin("Cara dušana 20, Subotica");
            SearchAndAddPushpin("Cara dušana 30, Subotica");

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void SearchAndAddPushpin(string address)
        {
            string requestUrl = $"http://dev.virtualearth.net/REST/v1/Locations?query={address}&key=";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestUrl);
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<RootObject>(json);

                if (data != null && data.resourceSets.Any() && data.resourceSets.First().resources.Any())
                {
                    var location = data.resourceSets.First().resources.First();
                    var point = new Location(location.point.coordinates[0], location.point.coordinates[1]);

                    var pushpin = new Pushpin();
                    pushpin.Location = point;

                    map.Children.Add(pushpin);
                    map.SetView(point, 15);
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




    }
}
