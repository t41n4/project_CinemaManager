using System.Data;

namespace Application
{
    public class Account
    {
        public Account(string userName, string ID, int type, string password = null)
        {
            this.UserName = userName;
            this.Password = password;
            this.Type = type;
            this.ID = ID;
        }

        public Account(DataRow row)
        {
            this.UserName = row["UserName"].ToString();
            this.Password = row["Pass"].ToString();
            this.Type = (int)row["LoaiTK"];
            this.ID = row["id"].ToString();
        }

        public int Type { get; set; }

        public string Password { get; set; }

        public string ID { get; set; }

        public string UserName { get; set; }
    }
}