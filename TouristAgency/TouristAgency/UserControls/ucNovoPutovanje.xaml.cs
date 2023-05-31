using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ucNovoPutovanje : UserControl
    {
        Point startPointAtrakcija = new Point();
        Point startPointSmestaj = new Point();
        Point startPointRestoran = new Point();

        public event RoutedEventHandler KreirajClicked;
        public event RoutedEventHandler PonistiClicked;

        PutovanjaServis putovanjaServis = new PutovanjaServis();
        public ObservableCollection<Atrakcija> Atrakcije
        {
            get;
            set;
        }

        public ObservableCollection<Atrakcija> Atrakcije1
        {
            get;
            set;
        }
        public ObservableCollection<Smestaj> Smestaji
        {
            get;
            set;
        }

        public ObservableCollection<Smestaj> Smestaji1
        {
            get;
            set;
        }

        public ObservableCollection<Restoran> Restorani
        {
            get;
            set;
        }

        public ObservableCollection<Restoran> Restorani1
        {
            get;
            set;
        }

        public ucNovoPutovanje()
        {
            InitializeComponent();
         
            Loaded += async (sender, e) =>
            {
                Atrakcije = await putovanjaServis.SveAtrakcijeAsync();
                Atrakcije1 = new ObservableCollection<Atrakcija>();
                this.DataContext = this;
            };

            Loaded += async (sender, e) =>
            {
                Restorani = await putovanjaServis.SviRestoraniAsync();
                Restorani1 = new ObservableCollection<Restoran>();
                this.DataContext = this;

            };
            

            Loaded += async (sender, e) =>
            {
                Smestaji = await putovanjaServis.SviSmestajiAsync();
                Smestaji1 = new ObservableCollection<Smestaj>();
                this.DataContext = this;
            };
            

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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željanu atrakciju možete prevući u polje izabrane atrakcije.";

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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željanu atrakciju možete prevući u polje izabrane atrakcije.";
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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željani smeštaj možete prevući u polje izabrani smeštaji.";
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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željani smeštaj možete prevući u polje izabrani smeštaji.";
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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željani restoran možete prevući u polje izabrani restorani.";
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
                    txtError.Visibility = Visibility.Visible;
                    txtError.Text = "Samo željani restoran možete prevući u polje izabrani restorani.";
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
            string brDana = txtBrojDana.Text;
            string cena = txtCena.Text;
            string datum = datePicker.Text;

            bool isCredentialsValid = ValidateInput(naziv, brDana,cena,datum);

            if (isCredentialsValid)
            {
                txtError.Visibility = Visibility.Collapsed;
                putovanjaServis.DodajPutovanje(naziv, brDana, cena, DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture), Atrakcije1, Smestaji1, Restorani1);
                txtSucces.Visibility = Visibility.Visible;
                txtSucces.Text = "Uspešno ste se kreirali putovanje.";

            }

            KreirajClicked?.Invoke(this, e);
        }

        private bool ValidateInput(string naziv, string brDana, string cena, string datum)
        {
            int broj;
            double brojCena;
            string format = "dd/MM/yyyy";
            DateTime datumDateTime;
            DateTime trenutniDatum = DateTime.Now.Date;
            if (string.IsNullOrWhiteSpace(naziv))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Naziv treba da bude popunjen.";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(brDana))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Broj dana treba da bude popunjeno.";
                return false;
            }
         
            else if (!int.TryParse(brDana, out broj))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Broj dana treba da bude broj.";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(cena))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Cena treba da bude popunjena.";
                return false;
            }
            else if (!double.TryParse(cena, out brojCena))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Cena treba da bude broj.";
                return false;
            }
            else if (string.IsNullOrWhiteSpace(datum))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Datum treba da bude popunjen.";
                return false;
            }
            else if (!DateTime.TryParseExact(datum, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out datumDateTime))
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Datum treba da bude formata mm/dd/gggg (npr. 04/05/2023) .";
                return false;
            }else if (DateTime.ParseExact(datum, "dd/MM/yyyy", CultureInfo.InvariantCulture) < trenutniDatum)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Datum treba da bude u budućnosti.";
                return false;

            }
            else if (Atrakcije1.Count == 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Treba da izaberete jednu ili više atrakcija.";
                return false;
            }
            else if (Smestaji1.Count == 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Treba da izaberete jedan ili više smeštaja.";
                return false;
            }
            else if (Restorani1.Count == 0)
            {
                txtError.Visibility = Visibility.Visible;
                txtError.Text = "Treba da izaberete jedan ili više restorana.";
                return false;
            }


            return true;
        }


        private void BtnPonisti_Click(object sender, RoutedEventArgs e)
        {
            txtNaziv.Text = "";
            txtBrojDana.Text = "";
            txtCena.Text = "";
            datePicker.Text = "";
            Atrakcije1.Clear();
            Restorani1.Clear();
            Smestaji1.Clear();
            PonistiClicked?.Invoke(this, e);
            txtSucces.Visibility = Visibility.Visible;
            txtSucces.Text = "";
            txtError.Visibility = Visibility.Visible;
            txtError.Text = "";
        }
    }
}
