using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace WpfSqliteTestApp
{
    public class SqlLiteDataAccess
    {

        public static List<ItemModel> LoadAllItems()
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = conn.Query<ItemModel>("Select * from Items", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveItem(ItemModel item)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Execute("Insert into Items (title, username, password, comment) values (@title, @username, @password, @comment)", item);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static void DeleteItem(int id)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Execute("Delete from Items Where id =" + id);
            }
        }
    }
}