using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    /// Interaction logic for ucNovoPutovanje.xaml
    /// </summary>
    public partial class ucNovoPutovanje : UserControl, INotifyPropertyChanged
    {
        public event EventHandler VratiSeNa_Putovanja;

        Point startPointAtrakcija = new Point();
        Point startPointSmestaj = new Point();
        Point startPointRestoran = new Point();

        public event RoutedEventHandler KreirajClicked;
        public event RoutedEventHandler PonistiClicked;

        

        PutovanjaServis putovanjaServis = new PutovanjaServis();
        private ObservableCollection<Atrakcija> atrakcije;
        private ObservableCollection<Atrakcija> atrakcije1;
        private ObservableCollection<Restoran> restorani;
        private ObservableCollection<Restoran> restorani1;
        private ObservableCollection<Smestaj> smestaji;
        private ObservableCollection<Smestaj> smestaji1;

        public ObservableCollection<Atrakcija> Atrakcije
        {
            get { return atrakcije; }
            set
            {
                atrakcije = value;
                OnPropertyChanged(nameof(Atrakcije));
            }
        }

        public ObservableCollection<Atrakcija> Atrakcije1
        {
            get { return atrakcije1; }
            set
            {
                atrakcije1 = value;
                OnPropertyChanged(nameof(Atrakcije1));
            }
        }

        public ObservableCollection<Restoran> Restorani
        {
            get { return restorani; }
            set
            {
                restorani = value;
                OnPropertyChanged(nameof(Restorani));
            }
        }

        public ObservableCollection<Restoran> Restorani1
        {
            get { return restorani1; }
            set
            {
                restorani1 = value;
                OnPropertyChanged(nameof(Restorani1));
            }
        }

        public ObservableCollection<Smestaj> Smestaji
        {
            get { return smestaji; }
            set
            {
                smestaji = value;
                OnPropertyChanged(nameof(Smestaji));
            }
        }

        public ObservableCollection<Smestaj> Smestaji1
        {
            get { return smestaji1; }
            set
            {
                smestaji1 = value;
                OnPropertyChanged(nameof(Smestaji1));
                Loaded += ucNovoPutovanje_Loaded;
            }
        }

        private async void ucNovoPutovanje_Loaded(object sender, RoutedEventArgs e)
        {
            Atrakcije = await putovanjaServis.SveAtrakcijeAsync();
            Atrakcije1 = new ObservableCollection<Atrakcija>();
            Restorani = await putovanjaServis.SviRestoraniAsync();
            Restorani1 = new ObservableCollection<Restoran>();
            Smestaji = await putovanjaServis.SviSmestajiAsync();
            Smestaji1 = new ObservableCollection<Smestaj>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ucNovoPutovanje()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += ucNovoPutovanje_Loaded;


        }


        

        private void ListView_PreviewMouseLeftButtonDownAtrakcija(object sender, MouseButtonEventArgs e)
        {
            startPointAtrakcija = e.GetPosition(null);
        }

        private void ListView_MouseMoveAtrakcija(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPointAtrakcija - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                try
                {
                    Atrakcija atrakcija = (Atrakcija)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);
                    DataObject dragData = new DataObject("myFormat", atrakcija);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
                catch (Exception ex)
                {
                    txtErrorAtrakcije.Visibility = Visibility.Visible;
                    txtErrorAtrakcije.Text = "Samo željanu atrakciju možete prevući u polje izabrane atrakcije.";

                }



            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_DragEnterAtrakcija(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_DropAtrakcija(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                if (e.Data.GetData("myFormat") is Atrakcija atrakcija)
                {
                    Atrakcije.Remove(atrakcija);
                    Atrakcije1.Add(atrakcija);
                }
                else
                {
                    txtErrorAtrakcije.Visibility = Visibility.Visible;
                    txtErrorAtrakcije.Text = "Samo željanu atrakciju možete prevući u polje izabrane atrakcije.";
                }
            }
        }


        //smestaji
        private void ListView_PreviewMouseLeftButtonDownSmestaj(object sender, MouseButtonEventArgs e)
        {
            startPointSmestaj = e.GetPosition(null);
        }

        private void ListView_MouseMoveSmestaj(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPointSmestaj - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
        
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);


                try
                {
                    Smestaj smestaj = (Smestaj)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);
                    DataObject dragData = new DataObject("myFormat", smestaj);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
                catch(Exception ex)
                {
                    txtErrorSmestaji.Visibility = Visibility.Visible;
                    txtErrorSmestaji.Text = "Samo željani smeštaj možete prevući u polje izabrani smeštaji.";
                }

             
                
            }
        }

       
        private void ListView_DragEnterSmestaj(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_DropSmestaj(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                if (e.Data.GetData("myFormat") is Smestaj smestaj)
                {
                    Smestaji.Remove(smestaj);
                    Smestaji1.Add(smestaj);
                }
                else
                {
                    txtErrorSmestaji.Visibility = Visibility.Visible;
                    txtErrorSmestaji.Text = "Samo željani smeštaj možete prevući u polje izabrani smeštaji.";
                }
            }
        }
        //Restorani
        private void ListView_PreviewMouseLeftButtonDownRestoran(object sender, MouseButtonEventArgs e)
        {
            startPointRestoran = e.GetPosition(null);
        }

        private void ListView_MouseMoveRestoran(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPointRestoran - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {

                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);


                try
                {
                    Restoran restoran = (Restoran)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);


                    DataObject dragData = new DataObject("myFormat", restoran);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
                catch(Exception ex)
                {
                    txtErrorRestorani.Visibility = Visibility.Visible;
                    txtErrorRestorani.Text = "Samo željani restoran možete prevući u polje izabrani restorani.";
                }
            }
        }


        private void ListView_DragEnterRestoran(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_DropRestoran(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {

                if (e.Data.GetData("myFormat") is Restoran restoran)
                {
                    Restorani.Remove(restoran);
                    Restorani1.Add(restoran);
                }
                else
                {
                    txtErrorRestorani.Visibility = Visibility.Visible;
                    txtErrorRestorani.Text = "Samo željani restoran možete prevući u polje izabrani restorani.";
                }
            }
        }

        private void PretraziRestorane(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = Restorani.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Ocena.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText));

            restoraniListView.ItemsSource = filteredItems;
        }

        private void PretraziAtrakcije(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = Atrakcije.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Opis.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText));

            atrakcijeListView.ItemsSource = filteredItems;
        }

        private void PretraziSmestaje(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text.ToLower();

            var filteredItems = Smestaji.Where(item => item.Naziv.ToLower().Contains(searchText) || item.Ocena.ToLower().Contains(searchText) || item.Adresa.ToLower().Contains(searchText) || item.Id.ToLower().Contains(searchText) || item.Tip.ToString().ToLower().Contains(searchText));

            smestajiListView.ItemsSource = filteredItems;
        }

        private void BtnKreiraj_Click(object sender, RoutedEventArgs e)
        {
            string naziv = txtNaziv.Text;
            string brDana = txtbrDana.Text;
            string cena = txtCena.Text;
            string datum = datePicker.Text;

            bool isCredentialsValid = ValidateInput(naziv, brDana,cena,datum);

            if (isCredentialsValid)
            {
                string izabraneInformacije = "Informacije koje je korisnik izabrao:\n";
                izabraneInformacije += "Naziv: " + naziv + "\n";
                izabraneInformacije += "Broj dana: " + brDana + "\n";
                izabraneInformacije += "Cena: " + cena + "\n";
                izabraneInformacije += "Datum: " + datum + "\n";
                izabraneInformacije += "Izabrane atrakcije: " + "\n";
                foreach (Atrakcija at in atrakcije1)
                {
                    izabraneInformacije += "- "  +  at.Naziv + "," + at.Adresa + "," + at.Opis + "\n";
               
                }
                izabraneInformacije += "Izabrani smeštaji: " + "\n";
                foreach (Smestaj sm in smestaji1)
                {
                    izabraneInformacije += "- " + sm.Naziv + "," + sm.Adresa + "," + sm.Tip + "," + "Ocena: " +  sm.Ocena +  "\n";

                }

                izabraneInformacije += "Izabrani restorani: " + "\n";
                foreach (Restoran re in restorani1)
                {
                    izabraneInformacije += "- " + re.Naziv + "," + re.Adresa + ","  + "Ocena: " + re.Ocena + "\n";

                }


                MessageBox.Show(izabraneInformacije, "Izabrane informacije", MessageBoxButton.OK, MessageBoxImage.Information);
                //putovanjaServis.DodajPutovanje(naziv, brDana, cena, DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture), Atrakcije1, Smestaji1, Restorani1);


                //VratiSeNa_Putovanja.Invoke(this, e);
            }

            KreirajClicked?.Invoke(this, e);
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            if (parentObject is T parent)
                return parent;
            else
                return FindVisualParent<T>(parentObject);
        }

        private bool ValidateInput(string naziv, string brDana, string cena, string datum)
        {
          
            int broj;
            double brojCena;
            string format = "dd/MM/yyyy";
            DateTime datumDateTime;
            var scrollViewerNaziv = FindVisualParent<ScrollViewer>(txtNaziv);
            var scrollViewerCena = FindVisualParent<ScrollViewer>(txtCena);
            var scrollViewerDana = FindVisualParent<ScrollViewer>(txtbrDana);
            DateTime trenutniDatum = DateTime.Now.Date;
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtErrorNaziv.Visibility = Visibility.Visible;
                txtErrorNaziv.Text = "Naziv treba da bude popunjen.";
                scrollViewerNaziv.ScrollToVerticalOffset(scroll.TransformToAncestor(scrollViewerNaziv).Transform(new Point(0, 0)).Y);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(brDana))
            {
                txtErrorNaziv.Visibility = Visibility.Collapsed;
                txtErrorbrDana.Visibility = Visibility.Visible;
                txtErrorbrDana.Text = "Broj dana treba da bude popunjeno.";
                scrollViewerDana.ScrollToVerticalOffset(scroll.TransformToAncestor(scrollViewerDana).Transform(new Point(0, 0)).Y);
                return false;
            }
         
            else if (!int.TryParse(brDana, out broj))
            {
                txtErrorbrDana.Visibility = Visibility.Visible;
                txtErrorbrDana.Text = "Broj dana treba da bude broj.";
                scrollViewerDana.ScrollToVerticalOffset(scroll.TransformToAncestor(scrollViewerDana).Transform(new Point(0, 0)).Y);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(cena))
            {
                txtErrorbrDana.Visibility = Visibility.Collapsed;
                txtErrorCena.Visibility = Visibility.Visible;
                txtErrorCena.Text = "Cena treba da bude popunjena.";
                scrollViewerCena.ScrollToVerticalOffset(scroll.TransformToAncestor(scrollViewerCena).Transform(new Point(0, 0)).Y);
                return false;
            }
            else if (!double.TryParse(cena, out brojCena))
            {
                txtErrorCena.Visibility = Visibility.Visible;
                txtErrorCena.Text = "Cena treba da bude broj.";
                scrollViewerCena.ScrollToVerticalOffset(scroll.TransformToAncestor(scrollViewerCena).Transform(new Point(0, 0)).Y);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(datum))
            {
                txtErrorCena.Visibility = Visibility.Collapsed;
                txtErrorDatum.Visibility = Visibility.Visible;
                txtErrorDatum.Text = "Datum treba da bude popunjen.";
                return false;
            }
            else if (!DateTime.TryParseExact(datum, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out datumDateTime))
            {
                txtErrorDatum.Visibility = Visibility.Visible;
                txtErrorDatum.Text = "Datum treba da bude formata mm/dd/gggg (npr. 04/05/2023) .";
                return false;
            }else if (DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture) < trenutniDatum)
            {
                txtErrorDatum.Visibility = Visibility.Visible;
                txtErrorDatum.Text = "Datum treba da bude u budućnosti.";
                return false;

            }
            else if (Atrakcije1.Count == 0)
            {
                txtErrorDatum.Visibility = Visibility.Collapsed;
                txtErrorAtrakcije.Visibility = Visibility.Visible;
                txtErrorAtrakcije.Text = "Treba da izaberete jednu ili više atrakcija.";
                return false;
            }
            else if (Smestaji1.Count == 0)
            {
                txtErrorAtrakcije.Visibility = Visibility.Collapsed;
                txtErrorSmestaji.Visibility = Visibility.Visible;
                txtErrorSmestaji.Text = "Treba da izaberete jedan ili više smeštaja.";
                return false;
            }
            else if (Restorani1.Count == 0)
            {
                txtErrorSmestaji.Visibility = Visibility.Collapsed;
                txtErrorRestorani.Visibility = Visibility.Visible;
                txtErrorRestorani.Text = "Treba da izaberete jedan ili više restorana.";
                return false;
            }


            return true;
        }


        private void BtnPonisti_Click(object sender, RoutedEventArgs e)
        {
            txtNaziv.Text = "";
            txtbrDana.Text = "";
            txtCena.Text = "";
            datePicker.Text = "";
            Atrakcije1.Clear();
            Restorani1.Clear();
            Smestaji1.Clear();
            PonistiClicked?.Invoke(this, e);
            txtErrorNaziv.Visibility = Visibility.Visible;
            txtErrorNaziv.Text = "";
            txtErrorCena.Visibility = Visibility.Visible;
            txtErrorCena.Text = "";
            txtErrorbrDana.Visibility = Visibility.Visible;
            txtErrorbrDana.Text = "";
            txtErrorDatum.Visibility = Visibility.Visible;
            txtErrorDatum.Text = "";
            txtErrorAtrakcije.Visibility = Visibility.Visible;
            txtErrorAtrakcije.Text = "";
            txtErrorSmestaji.Visibility = Visibility.Visible;
            txtErrorSmestaji.Text = "";
            txtErrorRestorani.Visibility = Visibility.Visible;
            txtErrorRestorani.Text = "";
        }

        private void restoraniListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
