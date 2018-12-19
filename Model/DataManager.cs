using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp.Model
{
    public class DataManager
    {
        internal static List<Desk> Desks = new List<Desk>();
        internal static List<User> Users = new List<User>();


        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string desksOutput = File.ReadAllText(@"../../Desks.xml");
                XElement desksXElement = XElement.Parse(desksOutput);
                Desks = (from item in desksXElement.Descendants("desk")
                         select new Desk()
                         {
                             DeskNumber = int.Parse(item.Element("desknumber").Value),
                             State = bool.Parse(item.Element("state").Value),
                             UserID = item.Element("userid").Value,
                         }).ToList<Desk>();

                string usersOutput = File.ReadAllText(@"../../Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             ID = item.Element("id").Value,
                             Password = item.Element("password").Value,
                             isBooked = bool.Parse(item.Element("isbooked").Value),
                             DeskNumber = int.Parse(item.Element("desknumber").Value),
                         }).ToList<User>();
            }
            catch (FileNotFoundException exception)
            {
                //파일이 없으면 파일생성.
                Save();
            }
            foreach(User u in Users)
            {
                u.Print = u.ID + "   |   " + u.Password + "   |   " + u.isBooked.ToString() + "  |   " + u.DeskNumber.ToString();
            }
            foreach (Desk d in Desks)
            {
                if (d.DeskNumber % 9 == 0)
                {
                    d.C = "9";
                    d.R = d.DeskNumber / 9 + "";
                }
                else
                {
                    d.C = d.DeskNumber % 9 + "";
                    d.R = d.DeskNumber / 9 + 1 + "";
                }
                d.Print = d.DeskNumber.ToString() + "   |   " + d.State.ToString() + "   |   " + d.UserID;
            }
        }

        public static void Save()
        {
            string desksOutput = "";
            desksOutput += "<desks>\n";
            foreach (var item in Desks)
            {
                desksOutput += "<desk>\n";
                desksOutput += "    <desknumber>" + item.DeskNumber + "</desknumber>\n";
                desksOutput += "    <state>" + item.State + "</state>\n";
                desksOutput += "    <userid>" + item.UserID + "</userid>\n";
                desksOutput += "</desk>\n";
            }
            desksOutput += "</desks>";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "    <id>" + item.ID + "</id>\n";
                usersOutput += "    <password>" + item.Password + "</password>\n";
                usersOutput += "    <isbooked>" + item.isBooked + "</isbooked>\n";
                usersOutput += "    <desknumber>" + item.DeskNumber + "</desknumber>\n";
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>";
            File.WriteAllText(@"../../Desks.xml", desksOutput);
            File.WriteAllText(@"../../Users.xml", usersOutput);
            Load();
        }
    }
}
