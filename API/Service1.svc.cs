using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<UN> ViewUsersFollowers()
        {
            string line;

            List<string> users = new List<string>();

            List<UN> UserList = new List<UN>();

            System.IO.StreamReader file = new System.IO.StreamReader(ConfigurationManager.AppSettings["usersLocation"]);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);

                int offs = line.IndexOf("follows");
                string follower = line.Substring(0, offs - 1);
                string userlist = line.Substring(offs + 8);
                string[] usersarr = userlist.Split(',');
                Console.WriteLine(follower + "-" + userlist);

                foreach (string user in usersarr)
                {
                    Console.WriteLine("Add " + user + " " + follower);
                    if(UserList.Contains(new UN(follower.Trim(), user.Trim())))
                    {
                        UserList.Add(new UN(follower.Trim(), user.Trim()));
                    }
                }

            }

            //UserList.Sort();
            UserList = UserList.OrderBy(x => x.User).ToList();
            foreach (UN un in UserList)
            {
                Console.WriteLine("List " + un.User + "-" + un.Follower);
            }

            // list follower to user
            string was_user = "";
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (UN un in UserList)
            {
                if (was_user != un.User)
                {
                    // outputing user
                    if (!first)
                    {
                        Console.WriteLine(was_user + " = " + sb.ToString());
                    }

                    was_user = un.User;
                    sb = new StringBuilder();
                }
                sb.Append(un.Follower + ",");
                first = false;

            }
            Console.WriteLine(was_user + " = " + sb.ToString());
            Console.ReadLine();

          
            file.Close();
            return UserList.OrderBy(x=>x.User).ToList();
        }
     public class UN
        {
            public string Follower;
            public string User;

            public UN(string _Follower, string _User)
            {
                Follower = _Follower;
                User = _User;
            }
        }
    }
}
