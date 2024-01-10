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
using System.Windows.Shapes;

namespace BankApp.UI.ClientUI.Winds
{
    /// <summary>
    /// Логика взаимодействия для WndCloseBill.xaml
    /// </summary>
    public partial class WndCloseBill : Window
    {
        public WndCloseBill()
        {          
            InitializeComponent();
            downloadBills();
            CmbxBills.SelectedIndex = 0;            
        }

        private void downloadBills()
        {
            CmbxBills.Items.Clear();
            foreach (Bill bill in (LibUser.CurrentUser as Client).Bills)
            {
                if (bill.Status != BillStatus.Заморожен)
                    CmbxBills.Items.Add(bill);
            }
        }

        private void BtnCloseBill_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Вы уверены, что хотите закрыть выбранный счёт?", "Внимание", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
                return;

            if (LibBill.DeleteBill(CmbxBills.SelectedItem as Bill))
            {
                MessageBox.Show("Счёт удалён!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("При закрытии счёта произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CmbxBills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string ruble = string.Empty;
            Bill bill = (LibUser.CurrentUser as Client).Bills[CmbxBills.SelectedIndex];
            byte cypher = (byte)(bill.Balance % 10);
            if (cypher == 1)
                ruble = "рубль";
            else if (cypher < 5 && cypher > 1)
                ruble = "рубля";
            else
                ruble = "рублей";
            TblBalance.Text = $"Баланс на счету: {(CmbxBills.SelectedItem as Bill).Balance} {ruble}";
        }
    }
}
