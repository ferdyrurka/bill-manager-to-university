using MoneyManagerToUniversity.model;
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
    /// Logika interakcji dla klasy Bills.xaml
    /// </summary>
    public partial class Bills : Window
    {
        MoneyManagerEntities context = new MoneyManagerEntities();
        CollectionViewSource expensesTypeSource;
        CollectionViewSource expensesSource;

        /// <summary>
        /// Create context and set source
        /// </summary>
        public Bills()
        {
            InitializeComponent();

            expensesSource = ((CollectionViewSource)(FindResource("expensesViewSource")));
            expensesTypeSource = ((CollectionViewSource)(FindResource("expenses_typeViewSource")));
            DataContext = this;
        }

        /// <summary>
        /// Load data for window from db
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.expenses_type.Load();
            context.expenses.Load();

            expensesTypeSource.Source = context.expenses_type.Local;
            expensesSource.Source = context.expenses.Local;
        }


        /// <summary>
        /// Save expense update or/and create, set createdAt date, check good set relationships id for expense_type
        /// If it encounters a sudden error, it will notify the user about it, e.g.sudden deletion of the table
        /// </summary>
        private void SaveExpensesCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var changesEntities = from ce in context.ChangeTracker.Entries()
                where ce.State != EntityState.Unchanged
            select ce;

            foreach (var change in changesEntities)
            {
                if (change.Entity is expenses)
                {
                    expenses expense = (expenses) change.Entity;

                    if (change.State == EntityState.Added)
                    {
                        expense.created_at = DateTime.Now;
                    }

                    var type = (from o in context.expenses_type
                                where o.id == expense.expenses_type_id
                                select o).FirstOrDefault();

                    if (type == null)
                    {
                        MessageBox.Show("Give invalid relationship id for expense id: " + expense.id);
                        return;
                    }
                }
            }

            try
            {
                expensesTypeSource.View.Refresh();
                expensesTypeSource.View.MoveCurrentToFirst();

                expensesSource.View.Refresh();
                expensesSource.View.MoveCurrentToFirst();

                context.SaveChanges();
            } catch(Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        /// <summary>
        /// Delete expense for clicked object
        /// If it encounters a sudden error, it will notify the user about it, e.g.sudden deletion of the table
        /// </summary>
        private void DeleteExpenseCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            expenses obj = e.Parameter as expenses;

            if (obj == null)
            {
                return;
            }

            try
            {
                context.expenses.Remove(obj);

                expensesSource.View.Refresh();
                expensesSource.View.MoveCurrentToFirst();

                context.SaveChanges();
            } catch(Exception)
            {
                MessageBox.Show("Something went wrong");
            }
}
    }
}
