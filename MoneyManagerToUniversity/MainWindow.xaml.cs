using MoneyManagerToUniversity.controller;
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

namespace MoneyManagerToUniversity
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open bank account window
        /// </summary>
        private void OpenBankAccountCRUD(object sender, RoutedEventArgs e)
        {
            Window window = new BankAccount();
            window.Show();
        }

        /// <summary>
        /// Open bills window
        /// </summary>
        private void OpenBillsCRUD(object sender, RoutedEventArgs e)
        {
            Window window = new Bills();
            window.Show();
        }

        /// <summary>
        /// Open incomes window
        /// </summary>
        private void OpenIncomesCRUD(object sender, RoutedEventArgs e)
        {
            Window window = new Incomes();
            window.Show();
        }
    }
}
