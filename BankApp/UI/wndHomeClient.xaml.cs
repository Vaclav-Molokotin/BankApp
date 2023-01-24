using BankApp.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankApp.UI
{
    /// <summary>
    /// Логика взаимодействия для wndHomeClient.xaml
    /// </summary>
    public partial class wndHomeClient : Window
    {
        public Client Client { get; set; }
        public wndHomeClient(Client client)
        {
            Client = client;
            InitializeComponent();
            downloadBills();
            frmMain.Navigate(new pgTransfer(Client));
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new pgTransfer(Client));
        }

        private void btnRefill_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new pgRefill(Client));
        }

        private void btnWithdrawal_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new pgWithdrawal(Client));
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            frmMain.Navigate(new pgHistory(Client));
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Owner.Show();
            Close();                       
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void downloadBills()
        {
            StreamReader stream = new StreamReader(@"..\..\..\Data\Bills.txt", true);
            while (!stream.EndOfStream)
            {
                if (stream.ReadLine() == Client.Login)
                {
                    string bill = String.Empty;
                    string balance = String.Empty;
                    bill = stream.ReadLine();
                    balance = stream.ReadLine();
                    do
                    {
                        Client.Bills.Add(new Bill(bill, balance));
                        bill = stream.ReadLine();
                        balance = stream.ReadLine();
                    }
                    while (bill != string.Empty);
                    stream.Close();
                    break;
                }
            }
        }
    }
}
