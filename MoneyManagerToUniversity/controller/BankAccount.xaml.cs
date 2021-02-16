using MoneyManagerToUniversity.model;
using MoneyManagerToUniversity.service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace MoneyManagerToUniversity.controller
{
    /// <summary>
    /// Logika interakcji dla klasy BankAccount.xaml
    /// </summary>
    public partial class BankAccount : Window
    {
        MoneyManagerEntities context = new MoneyManagerEntities();
        CollectionViewSource bankSource;
        CollectionViewSource bankAccountSource;

        public BankAccount()
        {
            InitializeComponent();

            bankSource = ((CollectionViewSource)(FindResource("bankViewSource")));
            bankAccountSource = ((CollectionViewSource)(FindResource("bank_accountViewSource")));
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            context.bank.Load();
            context.bank_account.Load();

            bankSource.Source = context.bank.Local;
            bankAccountSource.Source = context.bank_account.Local;
        }

        private void CreateAndSaveBanksCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bank bank;

            bank = new bank
            {
                name = bankName.Text,
            };

            context.bank.Local.Insert(context.bank.Local.Count(), bank);

            bankSource.View.Refresh();
            bankSource.View.MoveCurrentTo(bank);

            context.SaveChanges();
        }

        private void SaveBanksCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bankSource.View.Refresh();
            bankSource.View.MoveCurrentToFirst();

            bankAccountSource.View.Refresh();
            bankAccountSource.View.MoveCurrentToFirst();

            context.SaveChanges();
        }

        private void CreateAndSaveBanksAccountCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            bank_account bank_account;


            int bankId;
            decimal currentBallance;

            try
            {
                bankId = int.Parse(bank_idTextBox.Text);
                currentBallance = decimal.Parse(current_ballanceTextBox.Text);
            } catch(Exception)
            {
                MessageBox.Show("Giva bad data, expceted int and decimal");
                return;
            }

            if (!(new AccountNumberValidator()).Validate(numberTextBox.Text))
            {
                MessageBox.Show("Giva bad account number!");
                return;
            }

            bank_account = new bank_account
            {
                bank_id = bankId,
                name = nameTextBox.Text,
                current_ballance = currentBallance,
                number = numberTextBox.Text,
                created_at = DateTime.Now,
            };

            context.bank_account.Local.Insert(context.bank_account.Local.Count(), bank_account);

            this.SaveBanksAccountCommandHandler(sender, e);
        }

        private void SaveBanksAccountCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var changesEntities = from ce in context.ChangeTracker.Entries()
                                  where ce.State != EntityState.Unchanged
                                  select ce;

            foreach (var change in changesEntities)
            {
                if (change.Entity is bank_account)
                {
                    bank_account bank_account = (bank_account)change.Entity;

                    var type = (from o in context.bank
                                where o.id == bank_account.bank_id
                                select o).FirstOrDefault();

                    if (type == null)
                    {
                        MessageBox.Show("Give invalid relationship id for expense id: " + bank_account.id);
                        return;
                    }
                }
            }

            bankSource.View.Refresh();
            bankSource.View.MoveCurrentToFirst();

            bankAccountSource.View.Refresh();
            bankAccountSource.View.MoveCurrentToFirst();

            context.SaveChanges();
        }
    }
}
