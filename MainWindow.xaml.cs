using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Sleeptimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int second, minute, hour = 0;
        private string secondText ,minuteText, hourText;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            TimeFirst.IsEnabled = false;
            if (StartStop.Content == "Стоп")
            {
                StartStop.Content = "Старт";
                timer.Stop();
                return;
            }
         
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            StartStop.Content = "Стоп";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            second++;
            if (second > 59)
            {
                minute++;
                second = 0;
            }

            if (minute > 59)
            {
                hour++;
                minute = second = 0;
            }

            if (hour > 59)
            { 
                minute = second = hour = 0;
            }

            if (second < 10)
            {
                secondText = $"0{second}";
            }
            else
            {
                secondText = second.ToString();
            }

            if (minute < 10)
            {
                minuteText = $"0{minute}";
            }
            else
            {
                minuteText = minute.ToString();
            }

            if (hour < 10)
            {
                hourText = $"0{hour}";
            }
            else
            {
                hourText = hour.ToString();
            }
            TimeFirst.Text = $"{hourText}:{minuteText}:{secondText}";



        }
    }
}
