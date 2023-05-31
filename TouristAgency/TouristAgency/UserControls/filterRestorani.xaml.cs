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

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for filterRestorani.xaml
    /// </summary>
    public partial class filterRestorani : UserControl
    {
        PutovanjaServis putovanjaServis = new PutovanjaServis();

        public filterRestorani()
        {
            InitializeComponent();
        }

        public event EventHandler VratiSeNa_Smestaj;
        public event EventHandler<RestoraniArgs> Button2Clicked;

        private void Odustani(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Smestaj?.Invoke(this, EventArgs.Empty);

        }

        private async void Filtriraj(object sender, RoutedEventArgs e)
        {
            string min = "";
            if (minOcena.Text != "")
                min = minOcena.Text;
            else
                min = "1";
            string max = "";
            if (maxOcena.Text != "")
                max = maxOcena.Text;
            else
                max = "5";
            if (ValidateInput(min, max))
            {

                if (Button2Clicked != null)
                {
                    
                    ObservableCollection<Restoran> filteredList = await putovanjaServis.FiltriranjeRestorana(min, max);
                    RestoraniArgs sa = new RestoraniArgs();
                    sa.PovratnaVrednost = filteredList;
                    Button2Clicked?.Invoke(this, sa);
                }

            }


        }


        private bool ValidateInput(string minRating, string maxRating)
        {
            if (int.TryParse(minRating, out int min))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. ocena treba biti broj";
                return false;
            }
            if (int.TryParse(maxRating, out int max))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. ocena treba biti broj";
                return false;
            }
            if (min > 0 && min < 6 && max > 0 && max < 6 && min < max)
            {
                return true;
            }

            if (min > max)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. ocena ne sme biti veca od Max. ocene";
                return false;
            }

            if (min > 5 || min < 1)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. ocena treba biti izmedju 1 i 5";
                return false;
            }

            if (max > 5 || max < 1)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. ocena treba biti izmedju 1 i 5";
                return false;
            }

            return true;
        }
    }
}
