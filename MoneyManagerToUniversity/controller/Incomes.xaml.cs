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
    /// Logika interakcji dla klasy Incomes.xaml
    /// </summary>
    public partial class Incomes : Window
    {
        MoneyManagerEntities context = new MoneyManagerEntities();
        CollectionViewSource incomesTypeSource;
        CollectionViewSource incomesSource;

        /// <summary>
        /// Create context and set source
        /// </summary>
        public Incomes()
        {
            InitializeComponent();

            incomesSource = ((CollectionViewSource)(FindResource("incomesViewSource")));
            incomesTypeSource = ((CollectionViewSource)(FindResource("incomes_typeViewSource")));
            DataContext = this;
        }

        /// <summary>
        /// Load data for window from db
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            context.incomes_type.Load();
            context.incomes.Load();

            incomesTypeSource.Source = context.incomes_type.Local;
            incomesSource.Source = context.incomes.Local;
        }

        /// <summary>
        /// Save incomes update or/and create, set createdAt date, check good set relationships id for incomes_type
        /// If it encounters a sudden error, it will notify the user about it, e.g.sudden deletion of the table
        /// </summary>
        private void SaveIncomesCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            var changesEntities = from ce in context.ChangeTracker.Entries()
                                  where ce.State != EntityState.Unchanged
                                  select ce;

            foreach (var change in changesEntities)
            {
                if (change.Entity is incomes)
                {
                    incomes income = (incomes)change.Entity;

                    if (change.State == EntityState.Added)
                    {
                        income.created_at = DateTime.Now;
                    }

                    var type = (from o in context.incomes_type
                                where o.id == income.incomes_type_id
                                select o).FirstOrDefault();

                    if (type == null)
                    {
                        MessageBox.Show("Give invalid relationship id for income id: " + income.id);
                        return;
                    }
                }
            }

            try
            {
                incomesTypeSource.View.Refresh();
                incomesTypeSource.View.MoveCurrentToFirst();

                incomesSource.View.Refresh();
                incomesSource.View.MoveCurrentToFirst();

                context.SaveChanges();
            } catch(Exception)
            {
                MessageBox.Show("something went wrong");
            }
            
        }

        /// <summary>
        /// Delete income for clicked object
        /// If it encounters a sudden error, it will notify the user about it, e.g.sudden deletion of the table
        /// </summary>
        private void DeleteIncomesCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            incomes obj = e.Parameter as incomes;

            if (obj == null)
            {
                return;
            }

            context.incomes.Remove(obj);

            try
            {
                incomesSource.View.Refresh();
                incomesSource.View.MoveCurrentToFirst();

                context.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("something went wrong");
            }
        }
    }
}
