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
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime time, timeSlider;
        private int secondSlider;
        private TimerStatus currentStatus = TimerStatus.Wait; 
        enum TimerStatus
        {
            Start,
            Stop, 
            Wait
        }
         
        public MainWindow()
        {
            InitializeComponent();
            RefreshEnableControls(); 
        }
         
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            TimerStart();
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            TimerStop();
        } 
        private void TimerStart()
        {
            timer = new DispatcherTimer();
            ReloadTime(); 
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            currentStatus = TimerStatus.Start;
            RefreshEnableControls();

        }

        private void ReloadTime()
        {
            if (((ComboBoxItem) TimeMode.SelectedValue).Name == "InTime")
            {
                time = DateTime.Parse(TimeFirst.Text) - DateTime.Now.TimeOfDay;
                TimeFirst.Text = "00:00:00";
            } 
            else
            { 
                TimeFirst.Text = TimeSecond.Text;
                time = DateTime.Parse(TimeFirst.Text);
            }
                
        }

        private void TimerStop()
        {
            ReloadTime();
            timer.Stop();
            currentStatus = TimerStatus.Stop;
            RefreshEnableControls();
        }
         
        private void timer_Tick(object sender, EventArgs e)
        { 
            time = time.AddSeconds(-1);  
            TimeFirst.Text = time.ToString("HH:mm:ss"); 
            if (time.Hour == 0 && time.Minute == 0 && time.Second == 0)
                EndTimer();
        }

        private void RefreshEnableControls()
        {
            switch (currentStatus)
            {
                case TimerStatus.Start: 
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = false;
                    Stop.IsEnabled = true; 
                    break;
                case TimerStatus.Stop: 
                    Stop.IsEnabled = false; 
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = true;
                    break; 
                case TimerStatus.Wait:
                    Stop.IsEnabled = false;
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = true; 
                    break;
            }
        }

        private void EndTimer()
        {
            TimerStop(); 

            if (MessageBox.Show("Вы действительно хотите выключить компьютер?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                return;

            switch (((ComboBoxItem)PowerMode.SelectedValue).Name)
            {
                case "Shutdown":
                    System.Diagnostics.Process.Start("shutdown", "-s -t 0"); 
                    break;
                case "Sleep":
                    System.Diagnostics.Process.Start("shutdown", "-s - f - t 00"); 
                    break;
                case "Reboot":
                    System.Diagnostics.Process.Start("shutdown", "-r -t 0");
                    break;
                case "SignOut":
                    System.Diagnostics.Process.Start("shutdown", "-1");
                    break; 
            } 
             
        } 
        private void SliderTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            secondSlider = Convert.ToInt32(SliderTime.Value) * 600;
            timeSlider = DateTime.ParseExact("00:00:00", "HH:mm:ss", null);
            timeSlider = timeSlider.AddSeconds(secondSlider == 86400? 86399 : secondSlider);
            TimeSecond.Text = timeSlider.ToString("HH:mm:ss"); 
        }
    }
}
