using BankApp.UI.ClientUI.Pages;
using System;
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
using System.Windows.Shapes;

namespace BankApp.UI.ClientUI.Windows
{
    /// <summary>
    /// Логика взаимодействия для wndHome.xaml
    /// </summary>
    public partial class WndHome : Window
    {
        object pageType;
        const string classNamePrefix = "BankApp.UI.Client.Pages.";

        private bool isExit = false;
        public WndHome()
        {
            InitializeComponent();
            PgHome page = new PgHome();
            frame.Navigate(page);
            pageType = page.GetType();
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            if (pageType.ToString() != classNamePrefix + "pgHome")
            {
                PgHome page = new PgHome();
                frame.Navigate(page);
                pageType = page.GetType();
            }
        }

        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if (pageType.ToString() != classNamePrefix + "pgTransfer")
            {
                PgTransfer page = new PgTransfer();
                frame.Navigate(page);
                pageType = page.GetType();
            }
        }

        private void btnTranferBtwBills_Click(object sender, RoutedEventArgs e)
        {
            if (pageType.ToString() != classNamePrefix + "pgTransferBtwBills")
            {
                PgTransferBtwBills page = new PgTransferBtwBills();
                frame.Navigate(page);
                pageType = page.GetType();
            }
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (pageType.ToString() != classNamePrefix + "pgHistory")
            {
                PgHistory page = new PgHistory();
                frame.Navigate(page);
                pageType = page.GetType();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            isExit = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isExit)
                Application.Current.Shutdown();
        }
    }
}
