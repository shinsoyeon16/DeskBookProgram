using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp.Model
{
    class Desk
    {
        public int DeskNumber { get; set; }
        public bool State { get; set; } // 0:빈자리 1: 예약완료
        public string BindingState { get { if (State==true) return "#ffff00"; else return "#00ff00"; } }
        public string UserID { get; set; }
        public string Print { get; set; }
        public string C { get; set; }
        public string R { get; set; }
        private static string selected;

        public static string Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Command Check { get; set; }
        public Desk()
        {
            Check = new Command(CheckMethod);
        }
        private void CheckMethod(object obj)
        {
            Selected = obj.ToString();
            Selected=Selected.Remove(0, 3);
        }
    }
}
