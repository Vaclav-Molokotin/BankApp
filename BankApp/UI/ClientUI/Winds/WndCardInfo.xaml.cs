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
    /// Логика взаимодействия для WndCardInfo.xaml
    /// </summary>
    public partial class WndCardInfo : Window
    {
        private Bill bill;
        public WndCardInfo(Bill bill)
        {
            InitializeComponent();

            Card card = bill.Card;

            string year = card.DateTo.Year.ToString();
            string month = (card.DateTo.Month < 10)?"0":"" + card.DateTo.Month;

            this.bill = bill;
            TblBillStatus.Text = bill.Status.ToString();
            TblCardStatus.Text = card.Status.ToString();
            TblDateTo.Text = $"{year}/{month}";
            
            DataContext = this.bill;
        }
    }
}
