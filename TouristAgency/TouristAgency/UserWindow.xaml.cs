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
using System.Windows.Shapes;
using TouristAgency.Model;
using TouristAgency.UserControls;
using TouristAgency.UserControls.client;


namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            loggedOut.LoginClick += Prijava_click;
            loggedOut.RegistracijaClick += Registracija_Click;
            loggedOut.PutovanjaClick += PrikazPutovanja;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        private void Atrakcije_Click(object sender, EventArgs e)
        {
            ucAtrakcije atrakcije = new ucAtrakcije();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atrakcije);
        }

        private void Smestaji_Click(object sender, EventArgs e)
        {
            ucSmestaji smestaji = new ucSmestaji();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(smestaji);
        }

        private void Restorani_Click(object sender, EventArgs e)
        {
            ucRestorani restorani = new ucRestorani();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(restorani);
        }

        private void Putovanja_Click(object sender, EventArgs e)
        {
            ucPutovanja putovanja = new ucPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);
        }
    

    private void Prijava_click(object sender, EventArgs e)
        {
            ucPrijava prijava = new ucPrijava();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(prijava);

            prijava.LoginClicked += UcitajProzor;
            prijava.RegistracijaClicked += Registracija_Click;
        }

        private void Registracija_Click(object sender, EventArgs e)
        {
            ucRegistracija registracija = new ucRegistracija();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(registracija);
            registracija.RegistracijaClicked += Prijava_click;
        }



        private void UcitajProzor(object sender, string e)
    {
        if (e == "goran")
        {
            agentMenu agentMenu = new agentMenu();
            agentMenu.Atrakcije += Atrakcije_Click;
            agentMenu.Putovanja += Putovanja_Click;
            agentMenu.Smestaji += Smestaji_Click;
            agentMenu.Restorani += Restorani_Click;
            agentMenu.Odjava += Odjava_Click;
            menu.Children.Clear();
            menu.Children.Add(agentMenu);

            ucPutovanja putovanja = new ucPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);

        }
        else if (e == "korisnik")
        {
            logged logged = new logged();
            logged.Odjava += Odjava_Click;
            logged.Putovanja += PrikazPutovanja;
            menu.Children.Clear();
            menu.Children.Add(logged);

            prikazPutovanja putovanja = new prikazPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);
        }
    }


    private void Odjava_Click(object sender, EventArgs e)
    {
            loggedOut loggedOut = new loggedOut();
            menu.Children.Clear();
            menu.Children.Add(loggedOut);

            prikazPutovanja putovanja = new prikazPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);

            loggedOut.LoginClick += Prijava_click;
            loggedOut.RegistracijaClick += Registracija_Click;
            loggedOut.PutovanjaClick += PrikazPutovanja;

            

        }

        private void PrikazPutovanja(object sender,EventArgs e)
        {
            prikazPutovanja putovanja = new prikazPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);
        }

    }

}
