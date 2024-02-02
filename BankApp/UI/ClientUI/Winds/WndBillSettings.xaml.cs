using BankApp.Libs;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static BankApp.Libs.LibSettings;

namespace BankApp.UI.ClientUI.Winds
{
    /// <summary>
    /// Логика взаимодействия для WndBillSettings.xaml
    /// </summary>
    public partial class WndBillSettings : Window
    {
        private List<Setting> settings;
        public WndBillSettings()
        {
            InitializeComponent();
            downloadSettings();
        }

        private void downloadSettings()
        {
            settings = ReadSettings();

            for (int i = 0; i < settings.Count; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                GrSettings.RowDefinitions.Add(row);

                TextBlock textBlock = new TextBlock();
                Binding bindingTbl = new Binding("Name");
                bindingTbl.Source = settings[i];
                textBlock.SetBinding(TextBlock.TextProperty, bindingTbl);

                UIElement uiElement = new UIElement();

                #region settings
                // Основной счёт
                switch (settings[i].Key)
                {
                    case "MainBill":
                        uiElement = new ComboBox();
                        ComboBox comboBox = (ComboBox)uiElement;
                        comboBox.ItemsSource = LibClient.GetBillsByClient(LibUser.CurrentUser as Client, true);
                        comboBox.DisplayMemberPath = "Number";

                        Binding bindingCmbx = new Binding("Value");
                        bindingCmbx.Source = settings[i];
                        comboBox.SetBinding(ComboBox.TextProperty, bindingCmbx);

                        if (settings[i].Value != null)
                        {
                            try
                            {
                                foreach (Bill bill in comboBox.Items)
                                {
                                    if (bill.Number == settings[i].Value)
                                    {
                                        comboBox.SelectedItem = bill;
                                        break;
                                    }
                                }
                            }
                            catch
                            {
                                settings[i].Value = null;
                                comboBox.SelectedIndex = 0;
                            }
                        }
                        else
                            comboBox.SelectedIndex = 0;

                        break;

                    case "SeeAllBills":
                        uiElement = new ToggleButton();
                        ToggleButton toggleButton = uiElement as ToggleButton;

                        Binding bindingTbtn = new Binding("Value")
                        {
                            Source = settings[i]
                        };
                        toggleButton.SetBinding(ToggleButton.IsCheckedProperty, bindingTbtn);
                        toggleButton.Content = settings[i].Name;
                        break;
                }

                #endregion

                Grid.SetRow(uiElement, i);
                Grid.SetRow(textBlock, i);

                Grid.SetColumn(uiElement, 1);
                Grid.SetColumn(textBlock, 0);

                GrSettings.Children.Add(uiElement);
                GrSettings.Children.Add(textBlock);
            }
        }

        async private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            await savingSettings();
            if (WriteSettings(settings))
                MessageBox.Show("Настройки сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("При сохранении настроек произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        async Task savingSettings()
        {
            SpContent.IsEnabled = false;
            SpContent.Opacity = 0.2;
            TblSaving.Visibility = Visibility.Visible;
            for (int i = 0; i < 10; i++)
            {
                
                StringBuilder stringBuilder = new StringBuilder();
                int h = i % 4;
                for (int j = 0; j < h; j++)
                    stringBuilder.Append(".");
                TblSaving.Text = "Загрузка" + stringBuilder.ToString();
                UpdateLayout();
                await Task.Delay(500);
            }
            TblSaving.Visibility = Visibility.Collapsed;
            SpContent.Opacity = 1;
            SpContent.IsEnabled = true;
            return;
        }
    }
}
