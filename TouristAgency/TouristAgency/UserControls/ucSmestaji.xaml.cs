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

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucSmestaji.xaml
    /// </summary>
    public partial class ucSmestaji : UserControl
    {
        ObservableCollection<Smestaj> smestaji;
        public ucSmestaji()
        {
            InitializeComponent();

            smestaji = new ObservableCollection<Smestaj>();

            smestaji.Add(new Smestaj("1", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));
            smestaji.Add(new Smestaj("2", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));
            smestaji.Add(new Smestaj("3", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));
            smestaji.Add(new Smestaj("4", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));
            smestaji.Add(new Smestaj("5", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));
            smestaji.Add(new Smestaj("6", "Ime1", "adresa", TipSmestaja.APARTMAN, "5"));

            smestajiDataGrid.ItemsSource = smestaji;
        }

        private void Obrisi_Smestaj(object sender, RoutedEventArgs e)
        {
            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno želiš da izbrišeš smestaj " + selectedItem.Naziv, "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    smestaji.Remove(selectedItem);
                    MessageBox.Show($"Smestaj '{selectedItem.Naziv}' je obrisan.", "Smestaj obrisan", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

        }

        private void Izmeni_Smestaj(object sender, RoutedEventArgs e)
        {

            Smestaj selectedItem = smestajiDataGrid.SelectedItem as Smestaj;

            ucSmestajIzmena forma = new ucSmestajIzmena(selectedItem.Id, selectedItem.Naziv, selectedItem.Tip.ToString(), selectedItem.Adresa, selectedItem.Ocena);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Izmeni_Smestaj += Promeni;
            forma.VratiSeNa_Smestaj += Vrati;




        }

        private void Dodaj_Smestaj(object sender, RoutedEventArgs e)
        {
            ucSmestajIzmena forma = new ucSmestajIzmena();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(forma);

            forma.Dodaj_Smestaj += Ucitaj;
            forma.VratiSeNa_Smestaj += Vrati;


        }

        private void Ucitaj(object sender, SmestajArgs e)
        {
            ucSmestaji atr = new ucSmestaji();
            atr.smestaji.Add(e.PovratnaVrednost);
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Promeni(object sender, SmestajArgs e)
        {
            ucSmestaji atr = new ucSmestaji();
            Smestaj smestajZaPromenu = atr.smestaji.FirstOrDefault(item => item.Id == e.PovratnaVrednost.Id);
            smestajZaPromenu.Id = e.PovratnaVrednost.Id;
            smestajZaPromenu.Naziv = e.PovratnaVrednost.Naziv;
            smestajZaPromenu.Tip = e.PovratnaVrednost.Tip;
            smestajZaPromenu.Adresa = e.PovratnaVrednost.Adresa;
            smestajZaPromenu.Ocena = e.PovratnaVrednost.Ocena;

            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }

        private void Vrati(object sender, EventArgs e)
        {
            ucSmestaji atr = new ucSmestaji();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atr);


        }
    }
}
