using System.Threading;
using System.Windows;

namespace WpfAppSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Thread thread = Thread.CurrentThread;
            this.DataContext = new
            {
                ThreadId = thread.ManagedThreadId
            };
        }

        private void OnCreateNewWindow(
            object sender,
            RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                MainWindow w = new MainWindow();
                w.Show();

                w.Closed += (sender2, e2) =>
                    w.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}