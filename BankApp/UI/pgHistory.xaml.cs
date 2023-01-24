using BankApp.Libs;
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

namespace BankApp.UI
{
    /// <summary>
    /// Логика взаимодействия для pgHistory.xaml
    /// </summary>
    public partial class pgHistory : Page
    {
        public Client Client { get; set; }
        public pgHistory(Client client)
        {
            InitializeComponent();
            Client = client;
            downloadTransactions();
        }

        private void downloadTransactions()
        {
            List<string[]> transactions = Transaction.ReadTransactionsByClient(Client);
            for(int i = 0; i < transactions.Count; i++)
            {
                ucTransaction transaction = new ucTransaction(transactions[i]);
                stpnTransactions.Children.Add(transaction);
            }
        }
    }
}
