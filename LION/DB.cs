using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LION
{
    internal class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;userName=root;;password=root;database=course_work");
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                MessageBox.Show("Подключение открыто");
            }
            else
            {
                MessageBox.Show("Проблемы с подключением");
            }
            
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public DataTable getDataDB()
        {
            DB db = new DB();
            db.openConnection();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM workers", db.connection);
            adapter.SelectCommand = command;
            adapter.Fill(table);
            closeConnection();
            return table;
        }
    }
}
