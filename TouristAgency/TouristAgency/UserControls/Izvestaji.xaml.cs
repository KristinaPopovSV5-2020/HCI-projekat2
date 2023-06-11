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
using TouristAgency.Servis;
using System.Globalization;
using TouristAgency.Model;
using System.Linq;
using TouristAgency.Helper;

namespace TouristAgency.UserControls
{
    /// <summary>
    /// Interaction logic for Izvestaji.xaml
    /// </summary>
    public partial class Izvestaji : UserControl
    {
        PutovanjaServis servis = new PutovanjaServis();
        List<Putovanje> comboPutovanja = new List<Putovanje>();
        public Izvestaji()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
                izvestaji.ItemsSource = servis.IzvestajPoDatumu(datePicker.SelectedDate.Value);
            else if (putovanjaComboBox.SelectedIndex != -1)
            {
                string naziv = putovanjaComboBox.SelectedItem.ToString();
                string id = comboPutovanja[putovanjaComboBox.SelectedIndex].Id;

                izvestaji.ItemsSource = servis.IzvestajPoPutovanju(naziv, id);
            }
        }

        private void datum_checked(object sender, RoutedEventArgs e)
        {
            biranjePutovanja.Visibility = Visibility.Collapsed;
            biranjeDatuma.Visibility = Visibility.Visible;
        }
        private void putovanje_checked(object sender, RoutedEventArgs e)
        {
            biranjeDatuma.Visibility = Visibility.Collapsed;
            biranjePutovanja.Visibility = Visibility.Visible;
            comboPutovanja = servis.PronadjiPutovanja();
            putovanjaComboBox.ItemsSource = comboPutovanja.Select(p => p.Naziv).ToList();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject)
            {
                UserWindow userWindow = new UserWindow();
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                Console.WriteLine(str);
                HelpProvider.ShowHelp(str, userWindow);
            }
        }
    }
}
