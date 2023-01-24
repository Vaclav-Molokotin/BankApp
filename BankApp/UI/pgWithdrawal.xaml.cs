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
    /// Логика взаимодействия для pgWithdrawal.xaml
    /// </summary>
    public partial class pgWithdrawal : Page
    {
        public Client Client { get; set; }
        private Bill currentBill;
        public pgWithdrawal(Client client)
        {
            InitializeComponent();
            Client = client;
            downloadBills();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int result = withdrawal();
            switch (result)
            {
                case -2:
                    MessageBox.Show("На выбранном счёте недостаточно средств!");
                    break;
                case -1:
                    MessageBox.Show("Укажите число, большее нуля!");
                    break;
                case 0:
                    MessageBox.Show("Транзакция выполнена успешно!");
                    break;
            }
        }

        private void cbxMyBills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentBill = Client.Bills[cbxMyBills.SelectedIndex];
            tbkBalance.Text = currentBill.Balance.ToString();

            tbkBalance.Visibility = Visibility.Visible;            
        }

        private int withdrawal()
        {
            float sum = 0;
            if (!Single.TryParse(tbSum.Text, out sum) || sum <= 0)
                return -1;
            if (sum > currentBill.Balance)
                return -2;
            Transaction.WriteTransaction(Client, currentBill.Number, null, TypesOfTransaction.Withdrawal, Single.Parse(tbSum.Text), DateTime.Now);
            currentBill.Balance -= sum;
            return 0;
        }

        private void downloadBills()
        {
            for(int i = 0; i < Client.Bills.Count; i++)
            {
                cbxMyBills.Items.Add(Client.Bills[i].Number);
            }
        }
    }
}
