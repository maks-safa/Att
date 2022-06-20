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
    /// Логика взаимодействия для AddRedacServicePage.xaml
    /// </summary>
    public partial class AddRedacServicePage : Page
    {
        private Model.Service _currentService = new Model.Service();
        public AddRedacServicePage(Model.Service selectedService)
        {
            InitializeComponent();
            if(selectedService != null)
            {
                _currentService = selectedService;
            }
            DataContext = _currentService;
        }

        private void BnBack_Click(object sender, RoutedEventArgs e)
        {
            MyClass.Manager.navFrame.GoBack();
        }

        private void BnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentService.Title))
                errors.AppendLine("Title");
            if (_currentService.Cost <= 0)
                errors.AppendLine("Cost");
            if (_currentService.DurationInSeconds <= 0)
                errors.AppendLine("Time");
         
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (_currentService.ID == 0)
                    MyClass.ConnectDB.atttrEntities.Service.Add(_currentService);
                
                MyClass.ConnectDB.atttrEntities.SaveChanges();
                MessageBox.Show("Save succsess");
                MyClass.Manager.navFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
