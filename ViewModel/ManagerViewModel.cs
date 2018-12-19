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
    class ManagerViewModel : ViewModelBase
    {
        #region 필드
        public Command Login { get; set; }
        public Command Logout { get; set; }
        public Command AddUser { get; set; }
        public Command DeleteUser { get; set; }
        public Command AddDesk { get; set; }
        public Command DeleteDesk { get; set; }
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
        private string mPassword;

        public string MPassword
        {
            get { return mPassword; }
            set { mPassword = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        private int deskIdx;

        public int DeskIdx
        {
            get { return deskIdx; }
            set
            {
                deskIdx = value;
            }
        }
        private int userIdx;

        public int UserIdx
        {
            get { return userIdx; }
            set { userIdx = value; }
        }

        private string loginV;
        public string LoginV
        {
            get { return loginV; }
            set
            {
                loginV = value;
                OnPropertyChanged("LoginV");
            }
        }
        private string manageV;

        public string ManageV
        {
            get { return manageV; }
            set
            {
                manageV = value;
                OnPropertyChanged("ManageV");
            }
        }
        private List<User> users;

        public List<User> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged("Users"); }
        }

        private List<Desk> desks;

        public List<Desk> Desks
        {
            get { return desks; }
            set { desks = value; OnPropertyChanged("Desks"); }
        }
        public static ManagerView _managerView { get; set; }
        #endregion
        #region 생성자
        public ManagerViewModel(ManagerView managerView)
        {
            _managerView = managerView;
            Users = DataManager.Users;
            Desks = DataManager.Desks;
            Login = new Command(LoginMethod);
            Logout = new Command(LogoutMethod);
            AddUser = new Command(AddUserMethod);
            DeleteUser = new Command(DeleteUserMethod);
            AddDesk = new Command(AddDeskMethod);
            DeleteDesk = new Command(DeleteDeskMethod);
            Book = new Command(BookMethod);
            Return = new Command(ReturnMethod);
            LoginV = "Visible"; ManageV = "Hidden";
        }
        #endregion

        #region 함수
        private void ReturnMethod(object obj)
        {
            if (obj.ToString() == "All")
            {
                foreach (Desk d in DataManager.Desks)
                {
                    d.State = false;
                    d.UserID = "";
                }
                foreach (User u in DataManager.Users)
                {
                    u.isBooked = false;
                    u.DeskNumber = 0;
                }
            }
            else
            {
                if (DataManager.Desks[DeskIdx].State == true)
                {
                    DataManager.Desks[DeskIdx].State = false;
                    string uid = DataManager.Desks[DeskIdx].UserID;
                    DataManager.Desks[DeskIdx].UserID = "";
                    UserIdx = DataManager.Users.FindIndex(x => x.ID == uid);
                    DataManager.Users[UserIdx].isBooked = false;
                    DataManager.Users[UserIdx].DeskNumber = 0;
                }
            }
            DataManager.Save();
            Desks = DataManager.Desks;
            Users = DataManager.Users;
            UserViewModel._userView.userViewModer.Desks = DataManager.Desks;
            UserViewModel._userView.BtnBinding();
        }


        private void BookMethod(object obj)
        {
            if (int.Parse(obj.ToString())>0 && int.Parse(obj.ToString())<DataManager.Desks.Count==false) { }
            else
            {
                DeskIdx = int.Parse(obj.ToString()) - 1;
                if (DataManager.Users[UserIdx].isBooked == true)
                {
                    MessageBox.Show("퇴실 후 예약할 수 있습니다.");
                }
                else if (DataManager.Desks[DeskIdx].State == true)
                {
                    MessageBox.Show("이미 예약한 사용자가 있습니다. 다른 좌석을 선택해 주세요.");
                }
                else if (DeskIdx > 0 && DeskIdx < DataManager.Desks.Count == false) { MessageBox.Show("다른 좌석을 선택해 주세요."); }
                else
                {
                    DataManager.Desks[DeskIdx].State = true;
                    DataManager.Desks[DeskIdx].UserID = DataManager.Users[UserIdx].ID;
                    DataManager.Users[UserIdx].isBooked = true;
                    DataManager.Users[UserIdx].DeskNumber = DeskIdx + 1;
                    DataManager.Save();
                    Desks = DataManager.Desks;
                    Users = DataManager.Users;
                    UserViewModel._userView.userViewModer.Desks=DataManager.Desks;
                    UserViewModel._userView.BtnBinding();
                }
            }
        }
        private bool isValidId(User user)
        {
            int a = -1;
            a = DataManager.Users.FindIndex(x => x.ID == user.ID);
            return a == -1;
            throw new NotImplementedException();
        }
        private void DeleteDeskMethod(object obj)
        {
            if (DeskIdx > 0 && DeskIdx < DataManager.Desks.Count == false) { }
            else
            {
                Desk d = DataManager.Desks[DeskIdx];
                DataManager.Desks.Remove(d);
                DataManager.Save();
                Desks = DataManager.Desks;
            }
        }

        private void AddDeskMethod(object obj)
        {
            Desk nd = new Desk();
            if (DataManager.Desks.Count == 0) nd.DeskNumber = 1;
            else nd.DeskNumber = DataManager.Desks[DataManager.Desks.Count - 1].DeskNumber + 1;
            DataManager.Desks.Add(nd);
            DataManager.Save();
            Desks = DataManager.Desks;

        }

        private void DeleteUserMethod(object obj)
        {
            if (UserIdx > 0 && UserIdx < DataManager.Users.Count == false) { }
            else
            {
                User u = DataManager.Users[UserIdx];
                DataManager.Users.Remove(u);
                DataManager.Save();
                Users = DataManager.Users;
            }
        }

        private void AddUserMethod(object obj)
        {
            User u = new User();
            u.ID = Id;
            u.Password = Password;
            if (Id == "" || Password == "") { MessageBox.Show("다시 입력해주세요"); }
            else if (isValidId(u))
            {
                DataManager.Users.Add(u);
                DataManager.Save();
                Users = DataManager.Users;
            }
            else { MessageBox.Show("중복된 ID가 있습니다."); Id = ""; Password = ""; }
        }
        private void LoginMethod(object obj)
        {
            if (MPassword == "12345")
            {
                LoginV = "Hidden"; ManageV = "Visible";
            }
            else { MessageBox.Show("로그인 실패! 패스워드가 다릅니다."); }
        }
        private void LogoutMethod(object obj)
        {
            LoginV = "Visible";
            ManageV = "Hidden";
        }
        #endregion
    }
}
