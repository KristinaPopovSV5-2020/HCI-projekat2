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
    /// Interaction logic for ucNovoPutovanje.xaml
    /// </summary>
    public partial class ucNovoPutovanje : UserControl
    {
        Point startPointAtrakcija = new Point();
        Point startPointSmestaj = new Point();
        Point startPointRestoran = new Point();

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
            this.DataContext = this;
            List<Atrakcija> l = new List<Atrakcija>();
            l.Add(new Atrakcija ("1", "Petar",  "Petrovic", "SW 1\\2061"));
            l.Add(new Atrakcija("1", "Petar", "Petrovic", "SW 1\\2061"));
            l.Add(new Atrakcija("1", "Petar", "Petrovic", "SW 1\\2061"));
            l.Add(new Atrakcija("1", "Petar", "Petrovic", "SW 1\\2061"));
            l.Add(new Atrakcija("1", "Petar", "Petrovic", "SW 1\\2061"));
            Atrakcije = new ObservableCollection<Atrakcija>(l);
            Atrakcije1 = new ObservableCollection<Atrakcija>();

          
            Restorani = new ObservableCollection<Restoran>();
            Restorani1 = new ObservableCollection<Restoran>();

            List<Smestaj> s = new List<Smestaj>();
            s.Add(new Smestaj("1", "naziv", "adresa", TipSmestaja.APARTMAN, "2"));
            s.Add(new Smestaj("1", "naziv", "adresa", TipSmestaja.APARTMAN, "2"));
            s.Add(new Smestaj("1", "naziv", "adresa", TipSmestaja.APARTMAN, "2"));
            s.Add(new Smestaj("1", "naziv", "adresa", TipSmestaja.APARTMAN, "2"));
            s.Add(new Smestaj("1", "naziv", "adresa", TipSmestaja.APARTMAN, "2"));

            Smestaji = new ObservableCollection<Smestaj>(s);
            Smestaji1 = new ObservableCollection<Smestaj>();

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
                Atrakcija atrakcija = e.Data.GetData("myFormat") as Atrakcija;
                Atrakcije.Remove(atrakcija);
                Atrakcije1.Add(atrakcija);
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
                Smestaj smestaj = e.Data.GetData("myFormat") as Smestaj;
                Smestaji.Remove(smestaj);
                Smestaji1.Add(smestaj);
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
                Restoran restoran = e.Data.GetData("myFormat") as Restoran;
                Restorani.Remove(restoran);
                Restorani1.Add(restoran);
            }
        }







    }
}
