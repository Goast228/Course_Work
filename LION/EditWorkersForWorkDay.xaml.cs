using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Data;

namespace LION
{
    /// <summary>
    /// Логика взаимодействия для EditWorkersForWorkDay.xaml
    /// </summary>
    public partial class EditWorkersForWorkDay : Page
    {
        public List<UserControl1> users = new List<UserControl1>();
        protected int count = 0;
        public EditWorkersForWorkDay()
        {
            InitializeComponent();

            Timer();
            CreateListWorker();
            
        }
        public void CreateListWorker()
        {
            wrap.Children.Clear();
            List<Worker> workers = ReadAndSendForDataBase.ReadWorkerMy();

            foreach (Worker worker in workers)
            {
                UserControl1 userControl1 = new UserControl1(this);
                userControl1.nameWorkers.Text = Convert.ToString(worker.name);
                userControl1.idWorkers.Text = $"{Convert.ToString(worker.id)})";
                wrap.Children.Add(userControl1);
                if (worker.worked)
                {
                    userControl1.check.IsChecked = true;
                    userControl1.grd.Background = Brushes.LightGreen;
                    userControl1.check.Content = "Убрать сотрудника со смены";
                }
                else
                {
                    userControl1.check.IsChecked = false;
                    userControl1.check.Content = "Добавить сотрудника на смену";
                }
                users.Add(userControl1);
            }
            Button buttonAddNewWorker = new Button();
            buttonAddNewWorker.Content = "Добавить нового рабочего";
            buttonAddNewWorker.Width = 300;
            buttonAddNewWorker.Height = 50;
            buttonAddNewWorker.Click += ButtonAddNewWorker_Click;
            wrap.Children.Add(buttonAddNewWorker);
        }
            private void ButtonAddNewWorker_Click(object sender, RoutedEventArgs e)
        {
            NewWorkersWindow newWorkersWindow = new NewWorkersWindow();
            if(newWorkersWindow.ShowDialog() == true)
            {
                if(newWorkersWindow.firstnameBox.Text.Length !=0 && !newWorkersWindow.firstnameBox.Text.Contains(' ') &&
                    newWorkersWindow.lastnameBox.Text.Length != 0 && !newWorkersWindow.lastnameBox.Text.Contains(' '))
                {
                    Worker worker = new Worker();
                    worker.name = newWorkersWindow.firstnameBox.Text + " " + newWorkersWindow.lastnameBox.Text;
                    ReadAndSendForDataBase.SendWorkerMy(worker);
                    MessageBox.Show("Новый рабочий добавлен");
                    CreateListWorker();

                }
                else
                {
                    MessageBox.Show("Не верные имя или фамилия!!!");
                }
            }
            else
            {
                MessageBox.Show("Вы не добавили нового сотрудника");
            }
        }


            public void Timer()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) => { time.Content = $"Текущая дата: {DateTime.Now.ToString()}"; };
            timer.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in users)
            {
                if (item.check.IsChecked == true)
                {
                    ReadAndSendForDataBase.SendforWorkersMy(Convert.ToInt32(item.idWorkers.Text.TrimEnd(')')), true);
                }
                else
                {
                    ReadAndSendForDataBase.SendforWorkersMy(Convert.ToInt32(item.idWorkers.Text.TrimEnd(')')), false);
                }
            }
            NavigationService.Navigate(new WorkDay());
        }
    }
}
