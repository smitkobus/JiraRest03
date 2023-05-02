using System.Data.SQLite;
using System.IO;

namespace lb
{
    //get nuget package : System.Data.SQLite.Core
    class clsSQLiteDB
    {
        public SQLiteConnection myConfigConnection;
        public clsSQLiteDB()
        {
            myConfigConnection = new SQLiteConnection("Data Source=./data/db_config.sqlite3");
            if (!File.Exists("./data/db_config.sqlite3"))
            {
                SQLiteConnection.CreateFile("./data/db_config.sqlite3");
            }
        }

        public void OpenConnection()
        {
            if (myConfigConnection.State != System.Data.ConnectionState.Open)
            {
                myConfigConnection.Open();
            }
        }
        public void CloseConnection()
        {
            if (myConfigConnection.State != System.Data.ConnectionState.Closed)
            {
                myConfigConnection.Close();
            }
        }
    }
}
