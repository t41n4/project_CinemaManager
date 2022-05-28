using Application;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace DB
{
    public class MessagesDB
    {
        ////ảnh -> byte[]
        //public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    return ms.ToArray();
        //}

        ////byte[] -> ảnh
        //public static Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}

        public static DataTable GetMessagesTable(string username)
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetMessage @username", new object[] { username });           
        }

        public static bool InsertMessage(string body, string user_from, string user_to, DateTime date_sent)
        {
            int result = DataProvider.ExecuteNonQuery
                ("EXEC USP_InsertMessage @body  , @user_from , @user_to , @date_sent", new object[]  { body,  user_from,  user_to,  date_sent });
          
            return result > 0;
        }     
        public static DataTable GetCustomNeedSupport()
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetCusNeedSupportForAdmin");
        }


    }
}