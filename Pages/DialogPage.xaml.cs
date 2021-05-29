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

namespace Stopwatch.Pages
{
    /// <summary>
    /// Interaction logic for DialogPage.xaml
    /// </summary>
    public partial class DialogPage : Page
    { 
        public DialogPage(string message, ref bool dialogResult)
        {
            InitializeComponent();
            labelDefinition.Content = message;
            ButtonYes.Click += (s, e) => { Application.Current.Shutdown(); };
            ButtonNo.Click += (s, e) => { };
        }
         
    }
}
