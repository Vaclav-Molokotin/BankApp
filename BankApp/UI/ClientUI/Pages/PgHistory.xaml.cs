﻿using BankApp.Libs;
using BankApp.Models;
using BankApp.UI.ClientUI.UserControls;
using BankApp.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp.UI.ClientUI.Pages
{
    /// <summary>
    /// Логика взаимодействия для pgHistory.xaml
    /// </summary>
    public partial class PgHistory : Page
    {
        public PgHistory()
        {
            InitializeComponent();
            downloadTransactions();
        }

        private void downloadTransactions()
        {
            List<TransactionTransferView> transactions = new List<TransactionTransferView>();
            transactions = LibTransaction.GetTransferTransactionsByClient(LibUser.CurrentUser as Client);

            foreach (TransactionTransferView transaction in transactions)
            {
                UcTransactionBlock transactionBlock = new UcTransactionBlock(transaction);
                SpTransactions.Children.Add(transactionBlock);
            }
        }
    }
}
