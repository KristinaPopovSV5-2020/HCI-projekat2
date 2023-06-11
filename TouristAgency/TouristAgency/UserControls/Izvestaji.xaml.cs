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
            if (datePicker.Visibility == Visibility.Visible)
            {
                if (datePicker.SelectedIndex != -1 && yearComboBox.SelectedIndex != -1)
                {
                    errorMessage.Visibility = Visibility.Collapsed;
                    var selectedItem = (ComboBoxItem)yearComboBox.SelectedItem;
                    int selectedYear = int.Parse(selectedItem.Content.ToString());
                    int selectedMonth = datePicker.SelectedIndex + 1;
                    izvestaji.ItemsSource = servis.IzvestajPoDatumu(new DateTime(selectedYear, selectedMonth, 1));
                    datePicker.SelectedItem = -1;
                    yearComboBox.SelectedItem = -1;
                }
                else if (datePicker.SelectedIndex == -1)
                {
                    errorMessage.Text = "Izabrani mesec ne sme biti prazan";
                    errorMessage.Visibility = Visibility.Visible;
                }
                else if (yearComboBox.SelectedIndex == -1)
                {
                    errorMessage.Text = "Izabrana godina ne sme biti prazna";
                    errorMessage.Visibility = Visibility.Visible;
                }
            }
        }
         private void Button2_Click(object sender, RoutedEventArgs e)
        {
           if(putovanjaComboBox.Visibility == Visibility.Visible)
            {
                if (putovanjaComboBox.SelectedIndex != -1)
                {
                    errorMessage.Visibility = Visibility.Collapsed;
                    string naziv = putovanjaComboBox.SelectedItem.ToString();
                    string id = comboPutovanja[putovanjaComboBox.SelectedIndex].Id;
                    putovanjaComboBox.SelectedItem = null;
                    izvestaji.ItemsSource = servis.IzvestajPoPutovanju(naziv, id);
                }
                else if (putovanjaComboBox.SelectedIndex == -1)
                {
                    errorMessage.Text = "Izabrano putovanje ne sme biti prazno";
                    errorMessage.Visibility = Visibility.Visible;
                }
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
        
        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            datePicker.Visibility = Visibility.Visible;

        }
    }
}
