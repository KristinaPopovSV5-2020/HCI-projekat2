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
    /// Interaction logic for OkModule.xaml
    /// </summary>
    public partial class OkModule : UserControl
    {
        public event EventHandler PotvrdiClicked;

        public OkModule(string uspesnost, string poruka)
        {
            InitializeComponent();
            this.uspesnost.Text = uspesnost;
            this.poruka.Text = poruka;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            PotvrdiClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
