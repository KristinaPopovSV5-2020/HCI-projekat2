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
using TouristAgency.Servis;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for filterPutovanja.xaml
    /// </summary>
    public partial class filterPutovanja : UserControl
    {
        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public filterPutovanja()
        {
            InitializeComponent();
        }

        public event EventHandler VratiSeNa_Putovanja;
        public event EventHandler<PutovanjaArgs> Filtriraj_Putovanja;

        private void Odustani(object sender, RoutedEventArgs e)
        {
            VratiSeNa_Putovanja?.Invoke(this, EventArgs.Empty);

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

            string minRestoran = "";
            if (minOcenaRestorana.Text != "")
                minRestoran = minOcenaRestorana.Text;
            else
                minRestoran = "1";
            string maxRestoran = "";
            if (maxOcenaRestorana.Text != "")
                maxRestoran = maxOcenaRestorana.Text;
            else
                maxRestoran = "5";

            string minC = "";
            if (minCena.Text != "")
                minC = minCena.Text;
            else
                minC = "0";
            string maxC = "";
            if (maxCena.Text != "")
                maxC = maxCena.Text;
            else
                maxC = "9999999";

            string minD = "";
            if (minDana.Text != "")
                minD = minDana.Text;
            else
                minD = "0";
            string maxD = "";
            if (maxDana.Text != "")
                maxD = maxDana.Text;
            else
                maxD = "9999999";
            if (ValidateInput(min, max) && ValidateInput(minRestoran, maxRestoran) && ValidatePrice(minC, maxC) && ValidateDays(minD, maxD))
            {
                var list = this.tipovi.Children.OfType<CheckBox>().Where(x => x.IsChecked == true).ToList();
                List<string> selected = new List<string>();
                foreach (var i in list)
                {
                    selected.Add(i.Content.ToString().ToUpper());
                }

                if (Filtriraj_Putovanja != null)
                {
                    if (selected.Count == 0)
                        selected = new List<string> { "SOBA", "HOTEL", "APARTMAN" };
                    List<Putovanje> filteredList = putovanjaServis.FiltriranjePutovanja(selected, min, max,minRestoran,maxRestoran,minC,maxC,minD,maxD);
                    PutovanjaArgs sa = new PutovanjaArgs();
                    sa.PovratnaVrednost = filteredList;
                    Filtriraj_Putovanja?.Invoke(this, sa);
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

        private bool ValidatePrice(string minRating, string maxRating)
        {
            if (int.TryParse(minRating, out int min))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. cena treba biti broj";
                return false;
            }
            if (int.TryParse(maxRating, out int max))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. cena treba biti broj";
                return false;
            }
            if (min > 0 && max > 0 && min < max)
            {
                return true;
            }

            if (min > max)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. cena ne sme biti veca od Max. cene";
                return false;
            }

            if (min < 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. cena treba biti veća od 0";
                return false;
            }

            if (max < 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. cena treba biti veća od 0";
                return false;
            }

            return true;
        }

        private bool ValidateDays(string minRating, string maxRating)
        {
            if (int.TryParse(minRating, out int min))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. dana treba biti broj";
                return false;
            }
            if (int.TryParse(maxRating, out int max))
            {

            }
            else
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. dana treba biti broj";
                return false;
            }
            if (min > 0 && max > 0 && min < max)
            {
                return true;
            }

            if (min > max)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. dana ne sme biti vece od Max. dana";
                return false;
            }

            if (min < 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Min. dana treba biti veće od 0";
                return false;
            }

            if (max < 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Max. dana treba biti veće od 0";
                return false;
            }

            return true;
        }

    }
}
