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
        DispatcherTimer timer = new DispatcherTimer();
        DateTime time, timeSlider;
        private int secondSlider = 0;
        private CurrentStatus currentStatus = CurrentStatus.Wait;
        enum CurrentStatus
        {
            Start,
            Stop,
            Pause,
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
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            TimerPause();
        }
        
        private void TimerStart()
        {
            timer = new DispatcherTimer();

            if (currentStatus != CurrentStatus.Pause) 
                ResetTime(); 

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            currentStatus = CurrentStatus.Start;
            RefreshEnableControls();

        }

        private void ResetTime()
        {
            TimeFirst.Text = TimeSecond.Text;
            time = DateTime.Parse(TimeFirst.Text);
        }

        private void TimerStop()
        {
            ResetTime();
            timer.Stop();
            currentStatus = CurrentStatus.Stop;
            RefreshEnableControls();
        }

        private void TimerPause()
        {
            timer.Stop();
            currentStatus = CurrentStatus.Pause;
            RefreshEnableControls();
        }

        private void timer_Tick(object sender, EventArgs e)
        { 
            time = time.AddSeconds(-1);  
            TimeFirst.Text = time.ToString("HH:mm:ss"); 
        }

        private void RefreshEnableControls()
        {
            switch (currentStatus)
            {
                case CurrentStatus.Start: 
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = false;
                    Stop.IsEnabled = Pause.IsEnabled = true; 
                    break;
                case CurrentStatus.Stop: 
                    Stop.IsEnabled = Pause.IsEnabled = false; 
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = true;
                    break;
                case CurrentStatus.Pause: 
                    Pause.IsEnabled = PanelEditTime.IsEnabled = false;
                    Start.IsEnabled = Stop.IsEnabled = PanelMode.IsEnabled = true; 
                    break;
                case CurrentStatus.Wait:
                    Stop.IsEnabled = Pause.IsEnabled = false;
                    Start.IsEnabled = PanelMode.IsEnabled = PanelEditTime.IsEnabled = true; 
                    break;
            }
        }

        private void EndTimer()
        {

        }

        private void SliderTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            secondSlider = Convert.ToInt32(SliderTime.Value) * 600;
            timeSlider = DateTime.ParseExact("00:00:00", "HH:mm:ss", null);
            timeSlider = timeSlider.AddSeconds(secondSlider);
            TimeSecond.Text = timeSlider.ToString("HH:mm:ss"); 
        }
    }
}
