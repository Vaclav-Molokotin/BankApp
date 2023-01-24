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
    /// Логика взаимодействия для pgRefill.xaml
    /// </summary>
    public partial class pgRefill : Page
    {
        public Client Client { get; set; }
        private Bill currentBill;
        public pgRefill(Client client)
        {
            InitializeComponent();
            Client = client;
            downloadBills();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            int result = refill();
            switch (result)
            {
                case -2:
                    MessageBox.Show("За один раз можно внести не более 50 000 рублей!");
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
            tbkBalance.Text = "Баланс: " + currentBill.Balance.ToString();

            tbkBalance.Visibility = Visibility.Visible;
        }

        private int refill()
        {
            float sum = 0;
            if (!Single.TryParse(tbSum.Text, out sum) || sum <= 0)
                return -1;
            if (sum > 50000)
                return -2;
            Transaction.WriteTransaction(Client, null, currentBill.Number, TypesOfTransaction.Refill, Single.Parse(tbSum.Text), DateTime.Now);
            currentBill.Balance += sum;
            return 0;
        }

        private void downloadBills()
        {
            for (int i = 0; i < Client.Bills.Count; i++)
            {
                cbxMyBills.Items.Add(Client.Bills[i].Number);
            }
        }
    }
}
