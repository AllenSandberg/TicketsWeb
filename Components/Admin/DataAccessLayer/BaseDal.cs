using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class BaseDal
    {
        public BaseDal()
        {
            
        }

        protected MySqlConnection GetConnection()
        {
            /** Initialize a new instance of the MySQLConnection class when given a string
             *  containing the connection String. */
            return new MySqlConnection(ConnectionDb.MySqlConnectionString);
        }

    }
}
