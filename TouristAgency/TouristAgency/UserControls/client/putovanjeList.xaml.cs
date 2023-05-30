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

namespace TouristAgency.UserControls.client
{
    /// <summary>
    /// Interaction logic for putovanjeList.xaml
    /// </summary>
    public partial class putovanjeList : UserControl
    {
        public static readonly DependencyProperty polazakProperty =
        DependencyProperty.Register("Polazak", typeof(string), typeof(putovanjeList));
        public static readonly DependencyProperty povratakProperty =
        DependencyProperty.Register("Povratak", typeof(string), typeof(putovanjeList));
        public static readonly DependencyProperty danaProperty =
        DependencyProperty.Register("Dana", typeof(DateTime), typeof(putovanjeList));
        public static readonly DependencyProperty nazivProperty =
        DependencyProperty.Register("Naziv", typeof(string), typeof(putovanjeList));
        public static readonly DependencyProperty cenaProperty =
        DependencyProperty.Register("Cena", typeof(string), typeof(putovanjeList));

        public string Polazak
        {
            get { return (string)GetValue(polazakProperty); }
            set { SetValue(polazakProperty, value); }
        }
        public string Povratak
        {
            get { return (string)GetValue(povratakProperty); }
            set { SetValue(povratakProperty, value); }
        }
        public string Dana
        {
            get { return (string)GetValue(danaProperty); }
            set { SetValue(danaProperty, value); }
        }
        public string Naziv
        {
            get { return (string)GetValue(nazivProperty); }
            set { SetValue(nazivProperty, value); }
        }
        public string Cena
        {
            get { return (string)GetValue(cenaProperty); }
            set { SetValue(cenaProperty, value); }
        }

        public putovanjeList()
        {
            InitializeComponent();
        }
    }
}
