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

namespace Secvice.MyPages
{
    /// <summary>
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            var CR = MyClass.ConnectDB.atttrEntities.Service.ToList();
            LV.ItemsSource = CR;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyClass.Manager.navFrame.Navigate(new MyPages.AddRedacServicePage(null));
        }

        private void BtRedac_Click(object sender, RoutedEventArgs e)
        {
            MyClass.Manager.navFrame.Navigate(new MyPages.AddRedacServicePage((sender as Button).DataContext as Model.Service));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MyClass.ConnectDB.atttrEntities.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            LV.ItemsSource = MyClass.ConnectDB.atttrEntities.Service.ToList();
        }

        private void BtYdal_Click(object sender, RoutedEventArgs e)
        {
            var service = ((sender as Button).DataContext as Model.Service);
            if (MessageBox.Show($"You sure delete {((sender as Button).DataContext as Model.Service).Title}","Caution", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    MyClass.ConnectDB.atttrEntities.Service.Remove(service);
                    MyClass.ConnectDB.atttrEntities.SaveChanges();
                    MessageBox.Show("Data delete");
                    MyClass.ConnectDB.atttrEntities.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                    LV.ItemsSource = MyClass.ConnectDB.atttrEntities.Service.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
