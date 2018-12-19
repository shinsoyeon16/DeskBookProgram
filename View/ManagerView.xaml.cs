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
using WpfApp.Model;
using WpfApp.ViewModel;

namespace WpfApp.View
{
    /// <summary>
    /// ManagerLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ManagerView : UserControl
    {
        internal ManagerViewModel managerViewModel;
        public ManagerView()
        {
            InitializeComponent();
            managerViewModel  = new ManagerViewModel(this);
            this.DataContext = managerViewModel;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            managerViewModel.MPassword = password.Password;
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            managerViewModel.Id = id.Text;
            managerViewModel.Password = password2.Text;
            managerViewModel.UserIdx = lbx.SelectedIndex;
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            managerViewModel.DeskIdx = lbx2.SelectedIndex;
        }
        public void BindingRefresh()
        {
            this.DataContext = managerViewModel;
        }
        private void lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            managerViewModel.Id = id.Text;
            managerViewModel.Password = password2.Text;
            managerViewModel.UserIdx = lbx.SelectedIndex;
        }
        private void lbx2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            managerViewModel.DeskIdx=lbx2.SelectedIndex;
        }
    }
}
