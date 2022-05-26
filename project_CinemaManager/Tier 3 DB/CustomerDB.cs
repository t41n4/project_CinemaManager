using System;
using System.Data;

namespace DB
{
    public class CustomerDB
    {
        public static DataTable GetCustomerMember(string customerID, string name)
        {
            string query = "Select * from KhachHang where id = '" + customerID + "' and HoTen = N'" + name + "'";
            return DataProvider.ExecuteQuery(query);
        }

        public static DataTable GetListCustomer()
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetCustomer");
        }

        public static bool InsertCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
            int result = DataProvider.ExecuteNonQuery("EXEC USP_InsertCustomer @idCus , @hoTen , @ngaySinh , @diaChi , @sdt , @cmnd ", new object[] { id, hoTen, ngaySinh, diaChi, sdt, cmnd });
            return result > 0;
        }

        public static bool UpdateCustomer(string id, string hoTen, DateTime ngaySinh, string diaChi, string sdt, int cmnd)
        {
            string command = string.Format("UPDATE dbo.KhachHang SET HoTen = N'{0}', NgaySinh = '{1}', DiaChi = N'{2}', SDT = '{3}', CMND = {4} WHERE id = '{5}'", hoTen, ngaySinh, diaChi, sdt, cmnd, id);

            int result = DataProvider.ExecuteNonQuery(command);
            return result > 0;
        }

        public static bool UpdatePointCustomer(string id, int point)
        {
            string command = string.Format("UPDATE dbo.KhachHang SET  DiemTichLuy = {0} WHERE id = '{1}'", point, id);
            int result = DataProvider.ExecuteNonQuery(command);
            return result > 0;
        }

        public static bool DeleteCustomer(string id)
        {
            int result = DataProvider.ExecuteNonQuery("DELETE dbo.KhachHang WHERE id = '" + id + "'");
            return result > 0;
        }

        public static DataTable SearchCustomerByID(string ID)
        {
            return DataProvider.ExecuteQuery("EXEC USP_SearchCustomer @ID", new object[] { ID });
        }
    }
}