using System;
using System.Data;

namespace DB
{
    public class RevenueDAO
    {
        public static DataTable GetRevenue(string idMovie, DateTime fromDate, DateTime toDate)
        {
            return DataProvider.ExecuteQuery("EXEC USP_GetRevenueByMovieAndDate @idMovie , @fromDate , @toDate", new object[] { idMovie, fromDate, toDate });
        }
    }
}