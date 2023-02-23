using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace LION
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            EditWorkersForWorkDay editWorkersForWorkDay = new EditWorkersForWorkDay();

            frame.Content = new EditWorkersForWorkDay();
        }
    }
}
