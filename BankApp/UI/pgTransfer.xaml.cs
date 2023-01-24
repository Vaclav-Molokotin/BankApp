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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp.UI
{
    /// <summary>
    /// Логика взаимодействия для pgTransfer.xaml
    /// </summary>
    public partial class pgTransfer : Page
    {
        Client client;          // Клиент
        Bill currentBill;       // Текущий счёт для перевода
       
        /// <summary>
        /// Конструктор. Инициализирует компоненты.
        /// </summary>
        /// <param name="client"></param>
        public pgTransfer(Client client)
        {
            InitializeComponent();
            this.client = client;
            downloadBills();
        }

        /// <summary>
        /// Метод. Загружает счета клиента.
        /// </summary>
        private void downloadBills()
        { 
            for(int i = 0; i < client.Bills.Count; i++)
            {
                cbxMyBillsTransfer.Items.Add(client.Bills[i].Number);
                cbxMyBillsRecieve.Items.Add(client.Bills[i].Number);
            }
        }

        /// <summary>
        /// Обработчик события нажатия кнопки "Далее". Запускает алгоритм перевода денег.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int result = toTransfer();
            switch(result)
            {
                case -4:
                    MessageBox.Show("Введите число, больше нуля!");
                    break;
                case -3:
                    MessageBox.Show("На выбранном счёте недостаточно средств!");
                    break;
                case -2:
                    MessageBox.Show("Не найден счёт с таким номером!");
                    break;
                case -1:
                    MessageBox.Show("Выбранный счёт совпадает с текущим!");
                    break;
                case 0:
                    MessageBox.Show("Транзакция прошла успешно!");
                    break;
            }
        }

        /// <summary>
        /// Метод. Реализует перевод денег с одного счёта на другой.
        /// </summary>
        /// <returns></returns>
        int toTransfer()
        {
            float sum;
            string benefitBillNumber;

            if (!Single.TryParse(tbSum.Text, out sum) || sum <= 0)
                return -4;

            if (sum > Single.Parse(tbBalance.Text))
                return -3;

            if ((bool)rbtnToOtherBill.IsChecked)
            {
                Bill otherBill = Bill.FindBillByNumber(tbkOtherBill.Text);
                if (otherBill == null)
                    return -2;
                if (otherBill.Number == currentBill.Number)
                    return -1;
                benefitBillNumber = otherBill.Number;
            }
            else
            {
                if (cbxMyBillsRecieve.Text == currentBill.Number)
                    return -1;
                benefitBillNumber = cbxMyBillsRecieve.Text;
            }

            Transaction.WriteTransaction(client, currentBill.Number, benefitBillNumber, TypesOfTransaction.Transfer, sum, DateTime.Now);
            currentBill.Balance -= sum;
            return 0;
        }

        /// <summary>
        /// Обработчик события. меняет текущий счёт для перевода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxMyBillsTransfer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentBill = client.Bills[cbxMyBillsTransfer.SelectedIndex];
            tbBalance.Text = currentBill.Balance.ToString();

            tbBalance.Visibility = Visibility.Visible;
        }
    }
}
