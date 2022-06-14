using System;
using System.Data;

namespace Application
{
    internal class Messages
    {
        public int Id_msg { get; set; }

        public string Body { get; set; }

        public string User_from { get; set; }

        public string User_to { get; set; }

        public DateTime Date_sent { get; set; }

        public Messages(int id_msg, string body, string user_from, string user_to, DateTime date_sent)
        {
            this.Id_msg = id_msg;
            this.Body = body;
            this.User_from = user_from;
            this.User_to = user_to;
            this.Date_sent = date_sent;
        }

        public Messages(DataRow row)
        {
            this.Id_msg = (int)row["id_msg"];
            this.Body = row["body"].ToString();
            this.User_from = row["user_from"].ToString();
            this.User_to = row["user_to"].ToString();
            this.Date_sent = (DateTime)row["date_sent"];
        }
    }
}