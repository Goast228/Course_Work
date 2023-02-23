using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LION
{
    /// <summary>
    /// Логика взаимодействия для WorkDay.xaml
    /// </summary>
    public partial class WorkDay : Page
    {
        public List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        public List<int> idServices = new List<int>();
        public List<string> nameServices = new List<string>();
        public List<string> discriptionServices = new List<string>();
        public List<double> priceServices = new List<double>();
        public string nameCar;
        public string services;
        public string cuantity;
        public double price;
        public int count = -1;
        public double allPrice = 0;
        Label line = new Label();
        public Label Alllabel = new Label();
        Label label = new Label();
        PrintDialog PrintDialog = new PrintDialog();

        public WorkDay()
        {
            ReadAndSendForDataBase.ReadForServicesMy(ref idServices, ref nameServices, ref discriptionServices, ref priceServices);
            InitializeComponent();
            cuantityServices.ItemsSource = list;
            servicesList.ItemsSource = nameServices;
            servicesList.SelectedItem = nameServices[0];
            cuantityServices.SelectedItem = list[0];

        }

        private void enterCar_Click(object sender, RoutedEventArgs e)
        {
            if (nameCarTextBox.Text == "" || nameCarTextBox.Text.Contains(' '))
            {
                MessageBox.Show("Введите название машины!");
            }
            else
            {
                nameCar = nameCarTextBox.Text;

                label.Content = nameCar;
                label.FontSize = 48;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                listServicesForAdd.Children.Add(label);
                labelServices.Visibility = Visibility.Visible;
                labelQuantity.Visibility = Visibility.Visible;
                servicesList.Visibility = Visibility.Visible;
                cuantityServices.Visibility = Visibility.Visible;
                enterServices.Visibility = Visibility.Visible;
                labelCar.IsEnabled = false;
                nameCarTextBox.IsEnabled = false;
                enterCar.IsEnabled = false;
                del.IsEnabled = true;
                clear.IsEnabled = true;

                count++;
            }
        }

        private void enterServices_Click(object sender, RoutedEventArgs e)
        {
            services = servicesList.SelectedItem.ToString();
            price = priceServices[servicesList.SelectedIndex];
            double totalPrice;
            Label label = new Label();
            label.FontSize = 24;
            cuantity = cuantityServices.SelectedItem.ToString();
            totalPrice = price * Convert.ToInt32(cuantity);
            label.Content = $"{services}: {price}р*{cuantity}={totalPrice}р";
            listServicesForAdd.Children.Add(label);
            count++;
            all.IsEnabled = true;

        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0)
            {
                if (listServicesForAdd.Children.Contains(line) && listServicesForAdd.Children.Contains(Alllabel))
                {
                    listServicesForAdd.Children.Remove(line);
                    listServicesForAdd.Children.Remove(Alllabel);
                }
                labelServices.Visibility = Visibility.Visible;
                labelQuantity.Visibility = Visibility.Visible;
                servicesList.Visibility = Visibility.Visible;
                cuantityServices.Visibility = Visibility.Visible;
                enterServices.Visibility = Visibility.Visible;
                listServicesForAdd.Children.RemoveAt(count);
                count--;
                allPrice = 0;
            }
            else if (count == 0)
            {
                labelServices.Visibility = Visibility.Hidden;
                labelQuantity.Visibility = Visibility.Hidden;
                servicesList.Visibility = Visibility.Hidden;
                cuantityServices.Visibility = Visibility.Hidden;
                enterServices.Visibility = Visibility.Hidden;
                labelCar.IsEnabled = true;
                nameCarTextBox.IsEnabled = true;
                enterCar.IsEnabled = true;
                del.IsEnabled = false;
                clear.IsEnabled = false;
                Print.IsEnabled = false;

                listServicesForAdd.Children.RemoveAt(count);
                count--;
                allPrice = 0;
            }
            if (count <= 0)
            {
                all.IsEnabled = false;
                Print.IsEnabled = false;
            }
            compliteCar.IsEnabled = false;
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            labelServices.Visibility = Visibility.Hidden;
            labelQuantity.Visibility = Visibility.Hidden;
            servicesList.Visibility = Visibility.Hidden;
            cuantityServices.Visibility = Visibility.Hidden;
            enterServices.Visibility = Visibility.Hidden;
            labelCar.IsEnabled = true;
            nameCarTextBox.IsEnabled = true;
            enterCar.IsEnabled = true;
            all.IsEnabled = false;
            del.IsEnabled = false;
            clear.IsEnabled = false;
            Print.IsEnabled = false;
            compliteCar.IsEnabled = false;
            listServicesForAdd.Children.Clear();
            count = -1;
            allPrice = 0;
        }

        private void compliteCar_Click(object sender, RoutedEventArgs e)
        {

            foreach (Label item in listServicesForAdd.Children)
            {
                string nameServices;
                string quantity;
                if (listServicesForAdd.Children.IndexOf(item) == 0)
                {
                    ReadAndSendForDataBase.SendForCarMy(item.Content.ToString(), allPrice);
                }
                else if (item != Alllabel && item != line)
                {
                    nameServices = item.Content.ToString();
                    nameServices = nameServices.Remove(nameServices.IndexOf(':'));
                    quantity = item.Content.ToString();
                    quantity = quantity.Remove(0, quantity.IndexOf('*') + 1);
                    quantity = quantity.Remove(quantity.IndexOf('='));
                    ReadAndSendForDataBase.SendForOrdersMy(Convert.ToInt32(quantity), nameServices);
                }
            }
            ReadAndSendForDataBase.SendForSalaryMy(allPrice);
            allPrice = 0;
            listServicesForAdd.Children.Clear();
            labelServices.Visibility = Visibility.Hidden;
            labelQuantity.Visibility = Visibility.Hidden;
            servicesList.Visibility = Visibility.Hidden;
            cuantityServices.Visibility = Visibility.Hidden;
            enterServices.Visibility = Visibility.Hidden;
            labelCar.IsEnabled = true;
            nameCarTextBox.IsEnabled = true;
            enterCar.IsEnabled = true;
            all.IsEnabled = false;
            del.IsEnabled = false;
            clear.IsEnabled = false;
            Print.IsEnabled = false;
            compliteCar.IsEnabled = false;
            listServicesForAdd.Children.Clear();
            count = -1;
            allPrice = 0;
            MessageBox.Show("Машина добавлена");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PrintDialog.ShowDialog() == true)
            {

                PrintDialog.PrintVisual(listServicesForAdd, "Распечатываем чек");

            }
        }

        private void all_Click(object sender, RoutedEventArgs e)
        {
            foreach (Label item in listServicesForAdd.Children)
            {
                string totalPrice;
                if (listServicesForAdd.Children.IndexOf(item) != 0)
                {
                    totalPrice = item.Content.ToString();
                    totalPrice = totalPrice.Remove(0, totalPrice.IndexOf('=') + 1);
                    totalPrice = totalPrice.TrimEnd('р');
                    allPrice += Convert.ToDouble(totalPrice);
                }
            }




            line.Content = "_________________________________________________________________________________________________________________________________________";
            listServicesForAdd.Children.Add(line);
            Alllabel.FontSize = 24;
            Alllabel.Content = $"Итого: {allPrice}";
            listServicesForAdd.Children.Add(Alllabel);
            all.IsEnabled = false;
            compliteCar.IsEnabled = true;
            Print.IsEnabled = true;
            labelServices.Visibility = Visibility.Hidden;
            labelQuantity.Visibility = Visibility.Hidden;
            servicesList.Visibility = Visibility.Hidden;
            cuantityServices.Visibility = Visibility.Hidden;
            enterServices.Visibility = Visibility.Hidden;
        }
        public void HistoryDate(string datetime1)
        {
            historyStackPanel.Children.Clear();
            List<int> idCars = new List<int>();
            List<string> nameCars = new List<string>();
            List<string> dateTimeCars = new List<string>();
            List<double> totalPriceCars = new List<double>();


            List<string> nameCarsHistory = new List<string>();
            List<string> dataTimeHistory = new List<string>();
            List<string> nameServicesHistory = new List<string>();
            List<double> priceHistory = new List<double>();
            List<int> quantityHistory = new List<int>();
            List<double> totalPriceHistory = new List<double>();
            ////////
            ReadAndSendForDataBase.ReadForCarsMy(ref idCars, ref nameCars, ref dateTimeCars, ref totalPriceCars);
            ReadAndSendForDataBase.ReadForHistoryMy(ref nameCarsHistory, ref dataTimeHistory, ref nameServicesHistory, ref priceHistory, ref quantityHistory, ref totalPriceHistory);
            string date1 = datetime1.Remove(datetime1.IndexOf(' '));
            string date2;
            bool flag = false;
            for (int i = 0; i < idCars.Count; i++)
            {
                date2 = dateTimeCars[i].Remove(dateTimeCars[i].IndexOf(' '));
                if (date1 == date2) 
                {
                    flag = true;
                    Label labelName = new Label();
                    labelName.FontSize = 24;
                    labelName.HorizontalAlignment = HorizontalAlignment.Center;
                    labelName.Content = $"Машина: {nameCars[i]} \nДата: {dateTimeCars[i]}";
                    historyStackPanel.Children.Add(labelName);

                    for (int j = 0; j < nameCarsHistory.Count; j++)
                    {
                        if (nameCars[i] == nameCarsHistory[j] && dateTimeCars[i] == dataTimeHistory[j])
                        {
                            Label labelServices1 = new Label();
                            labelServices1.FontSize = 24;
                            labelServices1.Content = $"{nameServicesHistory[j]}: {priceHistory[j]}р*{quantityHistory[j]}={priceHistory[j] * quantityHistory[j]}";
                            historyStackPanel.Children.Add(labelServices1);
                        }
                    }
                    Label labelAll = new Label();
                    labelAll.FontSize = 24;
                    labelAll.Foreground = Brushes.Red;
                    Label labelLine = new Label();
                    labelLine.Content = "_________________________________________________________________________________________________________________________________________";
                    labelAll.Content = $"Итого: {totalPriceCars[i]}";
                    historyStackPanel.Children.Add(labelLine);
                    historyStackPanel.Children.Add(labelAll);
                }
                
            }
            if (!flag)
            {
                Label label = new Label();
                label.Content = "В эту дату машин не было";
                label.FontSize = 30;
                historyStackPanel.Children.Add(label);
                flag = false;
            }
        }
        public void History()
        {
            historyStackPanel.Children.Clear();
            List<int> idCars = new List<int>();
            List<string> nameCars = new List<string>();
            List<string> dateTimeCars = new List<string>();
            List<double> totalPriceCars = new List<double>();


            List<string> nameCarsHistory = new List<string>();
            List<string> dataTimeHistory = new List<string>();
            List<string> nameServicesHistory = new List<string>();
            List<double> priceHistory = new List<double>();
            List<int> quantityHistory = new List<int>();
            List<double> totalPriceHistory = new List<double>();
            ////////
            ReadAndSendForDataBase.ReadForCarsMy(ref idCars, ref nameCars, ref dateTimeCars, ref totalPriceCars);
            ReadAndSendForDataBase.ReadForHistoryMy(ref nameCarsHistory, ref dataTimeHistory, ref nameServicesHistory, ref priceHistory, ref quantityHistory, ref totalPriceHistory);
            for (int i = idCars.Count - 1; i >= 0; i--)
            {
                Label labelName = new Label();
                labelName.FontSize = 24;
                labelName.HorizontalAlignment = HorizontalAlignment.Center;
                labelName.Content = $"Машина: {nameCars[i]} \nДата: {dateTimeCars[i]}";
                historyStackPanel.Children.Add(labelName);

                for (int j = 0; j < nameCarsHistory.Count; j++)
                {
                    if (nameCars[i] == nameCarsHistory[j] && dateTimeCars[i] == dataTimeHistory[j])
                    {
                        Label labelServices1 = new Label();
                        labelServices1.FontSize = 24;
                        labelServices1.Content = $"{nameServicesHistory[j]}: {priceHistory[j]}р*{quantityHistory[j]}={priceHistory[j] * quantityHistory[j]}";
                        historyStackPanel.Children.Add(labelServices1);
                    }
                }
                Label labelAll = new Label();
                labelAll.FontSize = 24;
                labelAll.Foreground = Brushes.Red;
                Label labelLine = new Label();
                labelLine.Content = "_________________________________________________________________________________________________________________________________________";
                labelAll.Content = $"Итого: {totalPriceCars[i]}";
                historyStackPanel.Children.Add(labelLine);
                historyStackPanel.Children.Add(labelAll);
            }
        }
        public List<string> name = new List<string>();
        public List<double> salary = new List<double>();
        private void SelectedPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (SelectedPage.SelectedIndex == 1)
            {
                History();
                
                
            }
            else if(SelectedPage.SelectedIndex == 2)
            {
                salaryStackPanel.Children.Clear();
                name.Clear();
                salary.Clear();
                ReadAndSendForDataBase.ReadForWorkersMy(ref name, ref salary);
                for (int i = 0; i < name.Count; i++)
                {
                    Label label = new Label();
                    label.Content = $"{name[i]}: {salary[i]}руб";
                    label.FontSize = 30;
                    salaryStackPanel.Children.Add(label);
                }
            }

            
        }

        private void dateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HistoryDate(calendar.SelectedDate.ToString());

            }
            catch
            {
                MessageBox.Show("Введите правильную дату!!!");
            }
        }

        private void dateTodayButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryDate(DateTime.Today.ToString());
        }

        private void dateYesterdayButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryDate(DateTime.Today.AddDays(-1).ToString());
        }

        private void dateAllButton_Click(object sender, RoutedEventArgs e)
        {
            History();
        }

       
        private void TabItem_Initialized(object sender, EventArgs e)
        {
            
            
        }
        private void clearsalary_Click(object sender, RoutedEventArgs e)
        {
            List<Worker> workers = new List<Worker>();
            workers = ReadAndSendForDataBase.ReadWorkerMy();
            double count = 0;
            foreach(Worker i in workers)
            {
                count += i.salary;
            }
            if (count != 0)
            {
                salaryStackPanel.Children.Clear();
                for (int i = 0; i < name.Count; i++)
                {
                    salary[i] = 0;
                    Label label = new Label();
                    label.Content = $"{name[i]}: {salary[i]}руб";
                    label.FontSize = 30;
                    salaryStackPanel.Children.Add(label);
                }

                string connectionString = "server=localhost;port=3306;userName=root;password=root;database=course_work";
                string command = $"UPDATE Workers SET Salary = {0}";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand sqlCommand = new MySqlCommand();
                    sqlCommand.CommandText = command;
                    sqlCommand.Connection = connection;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            else
            {
                MessageBox.Show("Зарплата уже выдана");
            }
        }
    }
}




