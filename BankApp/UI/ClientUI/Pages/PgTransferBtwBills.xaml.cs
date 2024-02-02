using BankApp.Libs;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для pgTransferBtwBills.xaml
    /// </summary>
    public partial class PgTransferBtwBills : Page
    {
        public PgTransferBtwBills()
        {
            InitializeComponent();
            downloadBills();
        }

        private void downloadBills()
        {
            CmbxBillFrom.Items.Clear();
            CmbxBillTo.Items.Clear();
            List<Bill> bills = LibClient.GetBillsByClient(LibUser.CurrentUser as Client);

            foreach (Bill bill in bills)
            {
                if (bill.Status == BillStatus.Активен)
                {
                    CmbxBillFrom.Items.Add(bill);
                    CmbxBillTo.Items.Add(bill);
                }
            }
            CmbxBillFrom.SelectedIndex = 0;
            CmbxBillTo.SelectedIndex = 0;
        }

        private void BtnTransfer_Click(object sender, RoutedEventArgs e)
        {
            if((CmbxBillFrom.SelectedItem as Bill).Number == (CmbxBillTo.SelectedItem as Bill).Number)
            {
                MessageBox.Show("Вы не можете перевести деньги на такой же счёт!");
                downloadBills();
                return;
            }

            Transaction? transaction;
            float amount = float.Parse(TbxAmount.Text);
            Bill billFrom = CmbxBillFrom.SelectedItem as Bill;

            if (amount > billFrom.Balance)
            {
                MessageBox.Show("Недостаточно средств!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                transaction = transferByBill();

            if (transaction == null)
                return;

            if (LibTransaction.AddTransaction(transaction))
                MessageBox.Show("Перевод отправлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Во время перевода произошла ошибка, попробуйте снова!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            downloadBills();
            updateBalance(CmbxBillFrom, TblBalanceFrom);
            updateBalance(CmbxBillTo, TblBalanceTo);
        }

        private Transaction? transferByBill()
        {
            Bill billFrom = CmbxBillFrom.SelectedItem as Bill;
            Bill billTo = CmbxBillTo.SelectedItem as Bill;

            if (billTo.Status != BillStatus.Активен)
            {
                MessageBox.Show($"Счёт получателя {billTo.Status}!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Transaction transaction = new Transaction
            {
                Type = TransactionType.ПереводМеждуСчетами,
                BillToNumber = billTo.Number,
                BillFromNumber = billFrom.Number,
                Amount = float.Parse(TbxAmount.Text),
                Sender = billFrom.Owner
            };
            return transaction;
        }

        private void CmbxBillFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBalance(CmbxBillFrom, TblBalanceFrom);
        }

        private string getRuble(int lastCypher)
        {
            if (lastCypher == 1)
                return "рубль";
            else if (lastCypher < 5 && lastCypher > 1)
                return "рубля";
            else
                return "рублей";
        }

        private void updateBalance(ComboBox combobox, TextBlock textBlock)
        {
            try
            {
                Bill bill = combobox.SelectedItem as Bill;
                int lastCypher = (int)bill.Balance % 10;
                string ruble = getRuble(lastCypher);

                textBlock.Text = $"Баланс: {bill.Balance} {ruble}";
            }
            catch { }
        }

        private void CmbxBillTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateBalance(CmbxBillTo, TblBalanceTo);           
        }
    }
}
