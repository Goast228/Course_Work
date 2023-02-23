using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LION
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public EditWorkersForWorkDay EditWorkersForWorkDay;
        public UserControl1(EditWorkersForWorkDay editWorkersForWorkDay)
        {

            InitializeComponent();
            EditWorkersForWorkDay = editWorkersForWorkDay;


        }

        private void check_Click(object sender, RoutedEventArgs e)
        {
            if (check.IsChecked == true)
            {
                grd.Background = Brushes.LightGreen;
                check.Content = "Убрать сотрудника со смены";
            }
            else
            {
                grd.Background = Brushes.Gray;
                check.Content = "Добавить сотрудника на смену";
            }

        }

        private void DellWorker_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить рабочего?", "delWorker", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    string command = $"delete from Workers where Name = '{nameWorkers.Text}';";
                    string connectionString = "server=localhost;port=3306;userName=root;;password=root;database=course_work";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand sqlCommand = new MySqlCommand(command);
                        sqlCommand.Connection = connection;
                        sqlCommand.ExecuteNonQuery();
                    }
                    EditWorkersForWorkDay.CreateListWorker();
                    
                    MessageBox.Show("Рабочий удален");
                    
                    break;
                case MessageBoxResult.No:
                    break;

            }
            
        }
    }
}
