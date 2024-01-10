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
        private int antiRecursion = 0;
        private int selectedBill = 0;
        public PgHome()
        {

            InitializeComponent();
            downloadBills();
        }

        private void setInterface()
        {
            if (CmbxBills.Items.Count > 0)
            {
                Bill bill = CmbxBills.SelectedItem as Bill;
                TblBalance.Text = $"Ваш баланс: {bill.Balance} рублей\nСчёт {bill.Status}";

                TblBalance.Visibility = Visibility.Visible;
                TblCard.Visibility = Visibility.Visible;
                ImgInfo.Visibility = Visibility.Visible;

                BtnCloseBill.IsEnabled = true;
                CmbxBills.IsEnabled = true;

                if (bill.Status == BillStatus.Заморожен)
                {
                    BtnCloseCard.IsEnabled = false;
                    BtnBindCard.IsEnabled = false;
                    TblCard.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (bill.CardNumber == null || bill.Card?.Status != CardStatus.Активна)
                    {
                        if (bill.CardNumber == null)
                            TblCard.Text = "Карта не привязана";
                        else
                            TblCard.Text = $"Карта {bill.Card?.Status}";
                        BtnCloseCard.IsEnabled = false;
                        BtnBindCard.IsEnabled = true;
                    }
                    else
                    {
                        TblCard.Text = $"Карта {bill.Card?.Status}";
                        BtnCloseCard.IsEnabled = true;
                        BtnBindCard.IsEnabled = false;
                    }
                    TblCard.Visibility = Visibility.Visible;
                }
            }
            else
            {
                TblBalance.Visibility = Visibility.Collapsed;
                TblCard.Visibility = Visibility.Collapsed;
                ImgInfo.Visibility = Visibility.Collapsed;

                CmbxBills.IsEnabled = false;
                BtnCloseCard.IsEnabled = false;
                BtnBindCard.IsEnabled = false;
            }
        }

        private void downloadBills()
        {
            User user = LibUser.CurrentUser;
            if (CmbxBills.SelectedIndex != -1)
                selectedBill = CmbxBills.SelectedIndex;
            CmbxBills.Items.Clear();
            try
            {
                CmbxBills.SelectedIndex = selectedBill;
            }
            catch
            {
                CmbxBills.SelectedIndex = 0;
            }

            if ((user as Client).Bills is not null)
            {
                foreach (Bill bill in (user as Client).Bills)
                {
                    bool isRepeat = false;
                    for (int i = 0; i < CmbxBills.Items.Count; i++)
                    {
                        if (bill.Number == (CmbxBills.Items[i] as Bill).Number)
                            isRepeat = true;
                    }
                    if (!isRepeat)
                        CmbxBills.Items.Add(bill);
                }
            }

            setInterface();
        }

        private void BtnCloseBill_Click(object sender, RoutedEventArgs e)
        {
            WndCloseBill wnd = new WndCloseBill();
            if (wnd.ShowDialog() is not null)
            {
                downloadBills();
            }
        }

        private void BtnOpenBill_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите открыть новый счёт?",
                "Внимание", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            if (LibBill.AddBill(LibUser.CurrentUser))
            {
                MessageBox.Show("Счёт создан!");
                downloadBills();
            }
            else
                MessageBox.Show("При создании счёта произошла ошибка!");
        }


        private void BtnCloseCard_Click(object sender, RoutedEventArgs e)
        {
            Card card = LibCard.GetCardByNumber((CmbxBills.SelectedItem as Bill).CardNumber);
            card.Status = CardStatus.Заблокирована;

            if (LibCard.UpdateCard(card))
            {
                MessageBox.Show("Карта заблокирована", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                card.Status = CardStatus.Активна;
                MessageBox.Show("Произошла ошибка, попробуйте снова!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            downloadBills();
        }

        private void BtnBindCard_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите создать карту для текущего счёта?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
                return;

            string cardNumber = LibCard.AddCard();
            if (cardNumber == null)
            {
                MessageBox.Show("При создании карты произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Card card = LibCard.GetCardByNumber(cardNumber);
            (CmbxBills.SelectedItem as Bill).Card = card;

            MessageBox.Show("Карта создана!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            downloadBills();
        }

        private void ChbxHistorySettings_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void CmbxBills_Selected(object sender, RoutedEventArgs e)
        {
            if (antiRecursion == 0)
            {
                antiRecursion = 2;
                downloadBills();
            }
            else
                antiRecursion--;
        }

        private void ImgInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                WndCardInfo wnd = new WndCardInfo(CmbxBills.SelectedItem as Bill);
                wnd.ShowDialog();
            }
        }

        private void BtnBillsSetting_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
