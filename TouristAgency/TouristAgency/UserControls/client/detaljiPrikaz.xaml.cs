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
        }

        internal void LoadItemDetails(Putovanje selectedItem)
        {
            nameInput.Text = selectedItem.Naziv;
            //listaAtrakcija.ItemsSource = selectedItem.Atrakcije;

            atrakcijas = putovanjaServis.AtrakcijeZaPutovanje("123");
            smestaji = putovanjaServis.SmestajZaPutovanje("123");
            restorani = putovanjaServis.RestoraniZaPutovanje("123");
            listaAtrakcija.ItemsSource = atrakcijas;
            listaSmestaja.ItemsSource = putovanjaServis.SmestajZaPutovanje("123");
            listaRestorana.ItemsSource = putovanjaServis.RestoraniZaPutovanje("123");
            SearchAndAddPushpin(atrakcijas.Select(a => a.Adresa).ToList());
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private async void SearchAndAddPushpin(List<string> adrese)
        {
            map.Children.Clear();
            if (adrese.Count > 0)
            {
                int i = 0;
                foreach(string address in adrese)
                {
                    i++;
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

                            var pushpin = new Pushpin();
                            pushpin.Location = point;
                            TextBlock textBlock = new TextBlock();
                            textBlock.Text = i.ToString(); // Set the text for the pin
                            pushpin.Content = textBlock;

                            map.Children.Add(pushpin);
                            map.SetView(point, 15);
                        }
                    }
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

        public void Smestaji_Click(object sender, RoutedEventArgs e)
        {
            SearchAndAddPushpin(smestaji.Select(a => a.Adresa).ToList());
        }
        public void Atrakcije_Click(object sender, RoutedEventArgs e)
        {
            SearchAndAddPushpin(atrakcijas.Select(a => a.Adresa).ToList());

        }
        public void Restorani_Click(object sender, RoutedEventArgs e)
        {
            SearchAndAddPushpin(restorani.Select(a => a.Adresa).ToList());

        }




    }
}
