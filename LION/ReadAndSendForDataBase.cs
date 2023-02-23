using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System;

namespace LION
{
    public class ReadAndSendForDataBase
    {
        public static List<Worker> ReadWorkerMy()
        {
            List<Worker> workers = new List<Worker>();
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("select * from Workers");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Worker worker = new Worker();
                worker.id = sqlDataReader.GetInt32(0);
                worker.name = sqlDataReader.GetString(1);
                worker.worked = sqlDataReader.GetBoolean(2);
                worker.salary = sqlDataReader.GetDouble(3);
                workers.Add(worker);
            }
            connection.Close();
            return workers;
        }

        public static void SendWorkerMy(Worker worker)
        {
            string command = $"insert into Workers (name, worked, salary)values('{worker.name}','{Convert.ToInt16(worker.worked)}','{worker.salary}')";
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand(command);
                sqlCommand.Connection = connection;
                sqlCommand.ExecuteNonQuery();
            }

        }

        public static void ReadForHistoryMy(ref List<string> nameCars, ref List<string> dateTime, ref List<string> nameServices,ref List<double> price, ref List<int> quantity, ref List<double> totalPrice)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("select Cars.Name, Cars.DateTime, Services.Name, Services.Price, Orders.quantity,Cars.TotalPrice From Cars INNER JOIN Orders ON Cars.id = Orders.idCars INNER JOIN Services ON Services.id = Orders.idServices ORDER BY Cars.id");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                nameCars.Add(sqlDataReader.GetString(0));
                dateTime.Add(sqlDataReader.GetString(1));
                nameServices.Add(sqlDataReader.GetString(2));
                price.Add(sqlDataReader.GetDouble(3));
                quantity.Add(sqlDataReader.GetInt32(4));
                totalPrice.Add(sqlDataReader.GetDouble(5));
            }
            connection.Close();
        }

        public static void ReadForOrdersMy(ref List<int> idCars, ref List<int> idServices, ref List<int> quantity)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM Orders");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                idCars.Add(sqlDataReader.GetInt32(0));
                idServices.Add(sqlDataReader.GetInt32(1));
                quantity.Add(sqlDataReader.GetInt32(2));
            }
            connection.Close();
        }
        
        public static void ReadForWorkersMy(ref List<string> name, ref List<double> salary)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM workers");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                name.Add(sqlDataReader.GetString(1));
                salary.Add(sqlDataReader.GetDouble(3));
            }
            connection.Close();

        }
        public static void SendforWorkersMy(int id, bool j)
        {
            string command1 = $"UPDATE Workers SET Worked = 1 WHERE id={id}";
            string command2 = $"UPDATE Workers SET Worked = 0 WHERE id={id}";
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                if (j)
                {

                    sqlCommand.CommandText = command1;
                    sqlCommand.Connection = connection;
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {

                    sqlCommand.CommandText = command2;
                    sqlCommand.Connection = connection;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        
        public static void ReadForServicesMy(ref List<int> id, ref List<string> name, ref List<string> discription, ref List<double> price)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM services");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                id.Add(sqlDataReader.GetInt32(0));
                name.Add(sqlDataReader.GetString(1));
                discription.Add(sqlDataReader.GetString(2));
                price.Add(sqlDataReader.GetDouble(3));
            }
        }

        public static void SendForCarMy(string nameCar, double TotalPrice)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.Now;
            string data = dateTime.ToString();
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            string command1 = $"INSERT INTO Cars (Name, DateTime, TotalPrice) VALUES ('{nameCar}', '{data}', '{TotalPrice}')";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.CommandText = command1;
                sqlCommand.Connection = connection;
                sqlCommand.ExecuteNonQuery();
                
            }
        }

        public static void ReadForCarsMy(ref List<int> id, ref List<string> name, ref List<string> dateTime, ref List<double> totalPrice)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand sqlCommand = new MySqlCommand("SELECT * FROM cars");
            sqlCommand.Connection = connection;
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                id.Add(sqlDataReader.GetInt32(0));
                name.Add(sqlDataReader.GetString(1));
                dateTime.Add(sqlDataReader.GetString(2));
                totalPrice.Add(sqlDataReader.GetDouble(3));
            }
            connection.Close();
        }
        public static void SendForOrdersMy(int quantity, string nameServices)
        {
            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            string command = "SELECT MAX(id) FROM Cars;";
            int idcars, idServices;
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.CommandText = command;
                sqlCommand.Connection = sqlConnection;
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                idcars = sqlDataReader.GetInt32(0);

            }
            command = $"SELECT id FROM Services WHERE name = '{nameServices}';";
            using (MySqlConnection sqlConnection = new MySqlConnection(connectionString))
            {
                sqlConnection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.CommandText = command;
                sqlCommand.Connection = sqlConnection;
                MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                sqlDataReader.Read();
                idServices = sqlDataReader.GetInt32(0);

            }
            
            command = $"INSERT INTO Orders (idCars, idServices, quantity) VALUES ({idcars}, {idServices},{quantity})";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.CommandText = command;
                sqlCommand.Connection = connection;
                sqlCommand.ExecuteNonQuery();
            }

        }
        public static void SendForSalaryMy(double allPrice)
        {
            double salary = allPrice / 20;

            string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
            string command = $"UPDATE Workers SET Salary = salary + {salary} WHERE Worked = 1";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlCommand sqlCommand = new MySqlCommand();
                sqlCommand.CommandText = command;
                sqlCommand.Connection = connection;
                sqlCommand.ExecuteNonQuery();
            }
        }


    }
}
