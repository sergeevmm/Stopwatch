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
        private int sliderSeconds;
        private TimerStatus Status = TimerStatus.Wait; 

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
            SetCurrentDateTimeToBottom();
        }

        private void SetCurrentDateTimeToBottom()
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, e) =>
            { CurrentDateTime.Content = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(); };
            timer.Start();
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
            ReloadTimeControls();
            timer = new DispatcherTimer(); 
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            Status = TimerStatus.Start;
            RefreshEnableControls();

        }

        private void ReloadTimeControls()
        {
            TimeFirst.Text = TimeSecond.Text;

            if (((ComboBoxItem) TimeMode.SelectedValue).Name == "InTime") 
                time = DateTime.Parse(TimeFirst.Text) - DateTime.Now.TimeOfDay;  
            else 
                time = DateTime.Parse(TimeFirst.Text); 
                
        }

        private void TimerStop()
        { 
            timer.Stop();
            Status = TimerStatus.Stop;
            ReloadTimeControls();
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
            Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = Status != TimerStatus.Start;
            Stop.IsEnabled = Status == TimerStatus.Start;
        }

        private void EndTimer()
        {
            TimerStop();
            ShutDownSystem();  
        }

        private void ShutDownSystem()
        {
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
            sliderSeconds = Convert.ToInt32(Slider.Value) * 600;
            timeSlider = DateTime.ParseExact("00:00:00", "HH:mm:ss", null);
            timeSlider = timeSlider.AddSeconds(sliderSeconds == 86400? 86399 : sliderSeconds);
            TimeSecond.Text = timeSlider.ToString("HH:mm:ss"); 
        }
    }
}
