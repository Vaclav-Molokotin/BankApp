using BankApp.Libs;
using BankApp.Models;
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
    /// Логика взаимодействия для pgTransfer.xaml
    /// </summary>
    public partial class PgTransfer : Page
    {   
        public PgTransfer()
        {
            InitializeComponent();
            downloadBills();
        }

        private void downloadBills()
        {
            List<Bill> bills = LibClient.GetBillsByClient(LibUser.CurrentUser as Client);

            foreach (Bill bill in bills)
            {
                if (bill.Status == BillStatus.Активен)
                    CmbxBillFrom.Items.Add(bill);
            }
            CmbxBillFrom.SelectedIndex = 0;
        }

        private void BtnTransfer_Click(object sender, RoutedEventArgs e)
        {
            Transaction? transaction;
            float amount = float.Parse(TbxAmount.Text);
            Bill billFrom = CmbxBillFrom.SelectedItem as Bill;

            if (amount > billFrom.Balance)
            {
                MessageBox.Show("Недостаточно средств!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(ChbxEnterCard.IsChecked == true)
                transaction = transferByCard();
            else           
               transaction = transferByBill();

            if (transaction == null)
                return;

            if (LibTransaction.AddTransaction(transaction))
                MessageBox.Show("Перевод отправлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Во время перевода произошла ошибка, попробуйте снова!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private Transaction? transferByCard()
        {
            Bill billFrom = CmbxBillFrom.SelectedItem as Bill;
            Card? cardTo = LibCard.GetCardByNumber(TbxCardTo.Text);
            Bill? billTo = LibBill.GetBillByCard(cardTo);

            if (cardTo == null)
            {
                MessageBox.Show("Карты с таким номером не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (cardTo.Status != CardStatus.Активна)
            {
                MessageBox.Show($"Карта получателя {cardTo.Status}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Transaction transaction = new Transaction
            {
                Type = TransactionType.Перевод,
                BillToNumber = billTo.Number,
                BillFromNumber = billFrom.Number,
                Amount = float.Parse(TbxAmount.Text),
                Sender = billFrom.Owner
            };

            return transaction;
        }

        private Transaction? transferByBill()
        {
            Bill billFrom = CmbxBillFrom.SelectedItem as Bill;
            Bill billTo = LibBill.GetBillByNumber(TbxBillTo.Text);
            if (billTo == null)
            {
                MessageBox.Show("Счёта с таким номером не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (billTo.Status != BillStatus.Активен)
            {
                MessageBox.Show($"Счёт получателя {billTo.Status}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Transaction transaction = new Transaction
            {
                Type = TransactionType.Перевод,
                BillToNumber = TbxBillTo.Text,
                BillFromNumber = billFrom.Number,
                Amount = float.Parse(TbxAmount.Text),
                Sender = billFrom.Owner
            };
            return transaction;
        }

        private void ChbxEnterCard_Click(object sender, RoutedEventArgs e)
        {
            if(ChbxEnterCard.IsChecked == true) 
            {
                TbxBillTo.Visibility = Visibility.Collapsed;
                TbxCardTo.Visibility = Visibility.Visible;
            }
            else
            {
                TbxBillTo.Visibility = Visibility.Visible;
                TbxCardTo.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbxBillFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Bill bill = CmbxBillFrom.SelectedItem as Bill;
            string ruble;
            int lastCypher = (int)bill.Balance % 10;
            if (lastCypher == 1)
                ruble = "рубль";
            else if (lastCypher < 5 && lastCypher > 1)
                ruble = "рубля";
            else
                ruble = "рублей";
            TblBalance.Text = $"Баланс: {bill.Balance} {ruble}";
        }

        private void ChbxEnterCard_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
