using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DB
{
    public class MessagesDB
    {
        public static string Encrypt(string clearText)
        {
            string cypherText = "";
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    cypherText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return cypherText;
        }

        public static string Decrypt(string cipherText)
        {
            string clearText = "";
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    clearText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return clearText;
        }

        public static DataTable GetMessagesTable(string username)
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetMessage @username", new object[] { username });
        }

        public static bool InsertMessage(string body, string user_from, string user_to, DateTime date_sent)
        {
            int result = DataProvider.ExecuteNonQuery
                ("EXEC USP_InsertMessage @body  , @user_from , @user_to , @date_sent", new object[] { body, user_from, user_to, date_sent });

            return result > 0;
        }

        public static DataTable GetCustomNeedSupport()
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetCusNeedSupportForAdmin");
        }

        public static bool DeleteMessage(string username)
        {
            int result = DataProvider.ExecuteNonQuery
               ("EXEC USP_DeleteMessage @username", new object[] { username });
            return result > 0;
        }
    }
}