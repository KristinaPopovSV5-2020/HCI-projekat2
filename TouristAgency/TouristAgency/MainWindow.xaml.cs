﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using TouristAgency.UserControls;

namespace TouristAgency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;


           
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

        private void Atrakcije_Click(object sender, RoutedEventArgs e)
        {
            ucAtrakcije atrakcije = new ucAtrakcije();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(atrakcije);
        }

        private void Smestaji_Click(object sender, RoutedEventArgs e)
        {
            ucSmestaji smestaji = new ucSmestaji();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(smestaji);
        }

        private void Restorani_Click(object sender, RoutedEventArgs e)
        {
            ucRestorani restorani = new ucRestorani();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(restorani);
        }

        private void Putovanja_Click(object sender, RoutedEventArgs e)
        {
            ucPutovanja putovanja = new ucPutovanja();
            mainComponent.Children.Clear();
            mainComponent.Children.Add(putovanja);
        }
    }

    
}
