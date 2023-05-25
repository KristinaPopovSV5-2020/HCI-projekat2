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

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for ucAtrakcijaIzmena.xaml
    /// </summary>
    public partial class ucAtrakcijaIzmena : UserControl
    {
        public string id;
        public string naziv;
        public string opis;
        public string adresa;
        public ucAtrakcijaIzmena(string id,string naziv, string opis,string adresa)
        {
            InitializeComponent();

            this.id = id;
            this.naziv = naziv;
            this.opis = opis;
            this.adresa = adresa;
        }

        public ucAtrakcijaIzmena()
        {
            InitializeComponent();
        }

    }
}
