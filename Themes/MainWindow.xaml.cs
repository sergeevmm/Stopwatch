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
using MaterialDesignThemes.Wpf;
using Stopwatch;
using Stopwatch.Pages;

namespace Sleeptimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
       
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new TimerPage();
            this.labelHeader.Content = string.Empty;
            SetDefaultValuesToWindow();
        }

        public MainWindow(Page page, string labelText, bool showHideButton = true, bool showCloseButton = true)
        {
            InitializeComponent();
            MainFrame.Content = page;
            this.labelHeader.Content = labelText;
            btnCloseApp.IsEnabled = showCloseButton;
            btnHideApp.IsEnabled = showHideButton;
            SetDefaultValuesToWindow();
        }

        private void SetDefaultValuesToWindow()
        { 
            SizeToContent = SizeToContent.Manual;
            SizeToContent = SizeToContent.Width;
            SizeToContent = SizeToContent.Height;
            SizeToContent = SizeToContent.WidthAndHeight; 

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnHideApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            bool yes = false;
            Page paged = new DialogPage("Вы действительно хотите выйти?", ref yes);
            var mainWindow = new MainWindow(paged, "Внимание!", false, false);
            mainWindow.ShowDialog();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
