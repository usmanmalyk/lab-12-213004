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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Update every second, adjust as needed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;
        }

        public static readonly DependencyProperty CurrentTimeProperty = 
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), 
                new FrameworkPropertyMetadata(DateTime.Now, OnCurrentTimePropertyChanged, OnCoerceCurrentTimeProperty));
        // .NET Property wrapper
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimeProperty); }
            set { SetValue(CurrentTimeProperty, value); }
        }

        private static void OnCurrentTimePropertyChanged(DependencyObject source,DependencyPropertyChangedEventArgs e)
        {
            MainWindow mainWindow = source as MainWindow;
            DateTime newTime = (DateTime)e.NewValue;

            mainWindow.TimeLabel.Content = newTime.ToString();
        }

        private static object OnCoerceCurrentTimeProperty(DependencyObject sender, object data)
        {
            if ((DateTime)data > DateTime.Now)
            {
                data = DateTime.Now;
            }
            return data;

        }

        private bool OnValidateTimeProperty(object data)
        {
            return data is DateTime;
        }
    }
}