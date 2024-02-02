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

namespace BankApp.UI.ClientUI.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ucTransactionBlock.xaml
    /// </summary>
    public partial class UcTransactionBlock : UserControl
    {
        public TransactionTransferView transaction;
        
        public UcTransactionBlock(TransactionTransferView transaction)
        {
            this.transaction = transaction;
            InitializeComponent();
            DataContext = this.transaction;
        }
    }
}
