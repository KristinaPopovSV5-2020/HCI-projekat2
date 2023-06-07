using System;
using System.Collections.Generic;
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
    /// Interaction logic for kupovinaPotvrda.xaml
    /// </summary>
    public partial class kupovinaPotvrda : UserControl
    {
        PutovanjaServis putovanjaServis = new PutovanjaServis();

        public event EventHandler VratiSeNa_Detalji;
        public event EventHandler PotvrdiClicked;


        public kupovinaPotvrda(Putovanje p)
        {
            InitializeComponent();

            this.naziv.Text = p.Naziv;
            this.brojDana.Text = p.BrojDana.ToString();
            this.polazak.Text = p.Polazak;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            PotvrdiClicked?.Invoke(this, EventArgs.Empty);
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Detalji?.Invoke(this, EventArgs.Empty);
        }
    }
}
