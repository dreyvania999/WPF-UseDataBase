﻿using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Pages.AdminsPages;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            try
            {
                switch (StaffClass.CurrentStaffEmploe.Table_Role.role)
                {
                    case "Администратор":


                        break;
                    case "Пользователь":
                        btnSeeUsers.Visibility = Visibility.Collapsed;
                        btnSeeSales.Visibility = Visibility.Collapsed;
                        btnSeeStock.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        MessageBox.Show("Что-то пошло не по плану");
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не по плану");
                FrameClass.MainFrame.Navigate(new ActivatedPage());
            }

        }

        private void btnSeeUsers_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AllStaffPage());
        }

        private void btnExitMainMenu_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ActivatedPage());
        }

        private void btnSeeSales_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new ListViewSalesPage());
        }

        private void btnSeeStock_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageProductStock());
        }
    }
}
