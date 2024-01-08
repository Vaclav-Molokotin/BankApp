using BankApp.Libs;
using BankApp.Models;
using BankApp.UI.ClientUI.Windows;
using BankApp.UI.ClientUI.Winds;
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
    /// Логика взаимодействия для pgHome.xaml
    /// </summary>
    public partial class PgHome : Page
    {
        public PgHome()
        {

            InitializeComponent();
            
            downloadBills();
            setInterface();
        }

        private void setInterface()
        {
            if (CmbxBills.Items.Count > 0)
            {
                BtnCloseBill.IsEnabled = true;
                CmbxBills.IsEnabled = true;
                if (((Bill)CmbxBills.SelectedItem).IsFrozen)
                {
                    BtnReissueCard.IsEnabled = false;
                    BtnCloseCard.IsEnabled = false;
                    BtnBindCard.IsEnabled = false;
                }
                else
                {
                    BtnReissueCard.IsEnabled = true;
                    BtnCloseCard.IsEnabled = true;
                    BtnBindCard.IsEnabled = true;
                }
                Client client = (Client)LibUser.CurrentUser;
                LabBalance.Content = $"Ваш баланс: {client.Bills[CmbxBills.SelectedIndex].Balance} рублей";
            }
            else
            {
                CmbxBills.IsEnabled = false;
                BtnCloseBill.IsEnabled = false;
                BtnReissueCard.IsEnabled = false;
                BtnCloseCard.IsEnabled = false;
                BtnBindCard.IsEnabled = false;
            }
        }

        private void downloadBills()
        {
            User user = LibUser.CurrentUser;

            CmbxBills.Items.Clear();

            if ((user as Client).Bills is not null)
            {
                foreach (Bill bill in (user as Client).Bills)
                {
                    CmbxBills.Items.Add(bill);
                }
                CmbxBills.SelectedIndex = 0;
            }
        }

        private void BtnCloseBill_Click(object sender, RoutedEventArgs e)
        {
            WndCloseBill wnd = new WndCloseBill();
            if (wnd.ShowDialog() is not null)
                downloadBills();
        }

        private void BtnOpenBill_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите открыть новый счёт?",
                "Внимание", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            if (LibBill.CreateBill(LibUser.CurrentUser))
            {
                MessageBox.Show("Счёт создан!");
                downloadBills();
            }
            else
                MessageBox.Show("При создании счёта произошла ошибка!");
        }

        private void BtnReissueCard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnCloseCard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBindCard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChbxHistorySettings_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void CmbxBills_Selected(object sender, RoutedEventArgs e)
        {
            setInterface();
        }
    }
}
