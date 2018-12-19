using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Model;
using WpfApp.View;

namespace WpfApp.ViewModel
{
     class UserViewModel : ViewModelBase
    {
        #region 필드랑 프로퍼티

        public Command Join { get; set; }
        public Command Login { get; set; }
        public Command Logout { get; set; }
        public Command Book { get; set; }
        public Command Return { get; set; }
        private string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Password { get; set; }
        public static int LoginIndex { get; set; }
        private string loginV;
        public string LoginV
        {
            get { return loginV; }
            set { loginV = value;
                OnPropertyChanged("LoginV");
            }
        }
        private string bookV;
        public string BookV
        {
            get { return bookV; }
            set { bookV = value;
                OnPropertyChanged("BookV"); }
        }
        
        private List<Desk> desks;

        public List<Desk> Desks
        {
            get { return desks; }
            set { desks = value; OnPropertyChanged("Desks"); }
        }
        public static UserView _userView { get; set; }
        #endregion

        #region 생성자
        public UserViewModel(UserView userView)
        {
            _userView = userView;
            Desks = DataManager.Desks;
            Join = new Command(JoinMethod);
            Login = new Command(LoginMethod);
            Logout = new Command(LogoutMethod);
            Book = new Command(BookMethod);
            Return = new Command(ReturnMethod);
            LoginV = "Visible"; BookV = "Hidden";}
        public UserViewModel()
        {
            Desks = DataManager.Desks;
            Join = new Command(JoinMethod);
            Login = new Command(LoginMethod);
            Logout = new Command(LogoutMethod);
            Book = new Command(BookMethod);
            Return = new Command(ReturnMethod);
            LoginV = "Visible"; BookV = "Hidden";

        }
        #endregion

        #region 함수
        private void ReturnMethod(object obj)
        {
            if (DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].isBooked == true)
            {
                DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].isBooked = false;
                int dn = DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].DeskNumber;
                DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].DeskNumber = 0;
                DataManager.Desks[dn-1].State = false;
                DataManager.Desks[dn - 1].UserID="";
                DataManager.Save();
                Desks = DataManager.Desks;
                _userView.BtnBinding();
                ManagerViewModel._managerView.managerViewModel.Users = DataManager.Users;
                ManagerViewModel._managerView.managerViewModel.Desks = DataManager.Desks;
            }
        }

        private void BookMethod(object obj)
        {
            if (DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].isBooked == true)
            {
                MessageBox.Show("퇴실 후 예약할 수 있습니다.");
            }
            else if (DataManager.Desks[int.Parse(Desk.Selected) - 1].State == true)
            {
                MessageBox.Show("이미 예약한 사용자가 있습니다. 다른 좌석을 선택해 주세요.");
            }
            else
            {
                DataManager.Desks[int.Parse(Desk.Selected) - 1].State = true;
                DataManager.Desks[int.Parse(Desk.Selected) - 1].UserID = Id;
                DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].isBooked = true;
                DataManager.Users[DataManager.Users.FindIndex(x => x.ID == Id)].DeskNumber = int.Parse(Desk.Selected);
                DataManager.Save();
                Desks = DataManager.Desks;
                _userView.BtnBinding();
                ManagerViewModel._managerView.managerViewModel.Users = DataManager.Users;
                ManagerViewModel._managerView.managerViewModel.Desks = DataManager.Desks;
            }
        }


        private bool isValidId(User user)
        { 
            int a = -1; 
            a = DataManager.Users.FindIndex(x => x.ID == user.ID);
            return a == -1;
            throw new NotImplementedException();
        }

        private void LoginMethod(object obj)
        {
            int idx = -1;
            User u = new User();
            u.ID = Id;
            u.Password = Password;
            idx = DataManager.Users.FindIndex(x => x.ID == Id);
            if (Id == "" || Password == "") { MessageBox.Show("다시 입력해주세요"); }
            else if (idx!=-1)
            {
                if (DataManager.Users[idx].Password == Password)
                {
                    LoginIndex = idx;
                    LoginV = "Hidden";
                    BookV = "Visible";
                }
                else MessageBox.Show("로그인 실패! 비밀번호가 다릅니다.");
            }
            else MessageBox.Show("로그인 실패! 아이디가 다릅니다.");
        }
    
        private void JoinMethod(object obj)
        {
            User u = new User();
            u.ID = Id;
            u.Password = Password;
            if (Id == "" || Password=="") { MessageBox.Show("다시 입력해주세요"); }
            else if (isValidId(u))
            {
                DataManager.Users.Add(u);
                DataManager.Save();
                ManagerViewModel._managerView.managerViewModel.Users = DataManager.Users;
                ManagerViewModel._managerView.managerViewModel.Desks = DataManager.Desks;
                MessageBox.Show(Id + " 님 가입되었습니다.");
            }
            else MessageBox.Show("중복된 ID가 있습니다.");
        }
        private void LogoutMethod(object obj)
        {
            UserViewModel.LoginIndex = -1;
            LoginV = "Visible";
            BookV = "Hidden";
        }
        #endregion
    }
}
