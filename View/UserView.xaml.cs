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
    /// UserLogin.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserView : UserControl
    {
        internal UserViewModel userViewModer;
        public UserView()
        {
            InitializeComponent();
            userViewModer = new UserViewModel(this);
            this.DataContext = userViewModer ;
            BtnBinding();
        }
        public void BtnBinding()
        {
            btn1.DataContext = userViewModer.Desks[0];
            btn2.DataContext = userViewModer.Desks[1];
            btn3.DataContext = userViewModer.Desks[2];
            btn4.DataContext = userViewModer.Desks[3];
            btn5.DataContext = userViewModer.Desks[4];
            btn6.DataContext = userViewModer.Desks[5];
            btn7.DataContext = userViewModer.Desks[6];
            btn8.DataContext = userViewModer.Desks[7];
            btn9.DataContext = userViewModer.Desks[8];
            btn10.DataContext = userViewModer.Desks[9];
            btn11.DataContext =userViewModer.Desks[10];
            btn12.DataContext =userViewModer.Desks[11];
            btn13.DataContext =userViewModer.Desks[12];
            btn14.DataContext =userViewModer.Desks[13];
            btn15.DataContext =userViewModer.Desks[14];
            btn16.DataContext =userViewModer.Desks[15];
            btn17.DataContext =userViewModer.Desks[16];
            btn18.DataContext =userViewModer.Desks[17];
            btn19.DataContext =userViewModer.Desks[18];
            btn20.DataContext =userViewModer.Desks[19];
            btn21.DataContext =userViewModer.Desks[20];
            btn22.DataContext =userViewModer.Desks[21];
            btn23.DataContext =userViewModer.Desks[22];
            btn24.DataContext =userViewModer.Desks[23];
            btn25.DataContext =userViewModer.Desks[24];
            btn26.DataContext =userViewModer.Desks[25];
            btn27.DataContext =userViewModer.Desks[26];
            btn28.DataContext =userViewModer.Desks[27];
            btn29.DataContext =userViewModer.Desks[28];
            btn30.DataContext =userViewModer.Desks[29];
            btn31.DataContext =userViewModer.Desks[30];
            btn32.DataContext =userViewModer.Desks[31];
            btn33.DataContext =userViewModer.Desks[32];
            btn34.DataContext =userViewModer.Desks[33];
            btn35.DataContext =userViewModer.Desks[34];
            btn36.DataContext = userViewModer.Desks[35];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            userViewModer.Id = id.Text;
            userViewModer.Password = password.Password;
        }
    }
}
