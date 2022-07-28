using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ClientServerCalculatur_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow? s_window;
        private const int PORT = 8081;
        private const string IP = "127.0.0.1";
        IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        public MainWindow()
        {
            InitializeComponent();
            s_window = this;
            clientSocket.Connect(iPEnd);
            //this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (clientSocket.Connected)
            //clientSocket.Connect(iPEnd);
        }

        #region --- Header ---
        private void MinimiseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MovingWindow(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                s_window.DragMove();
            }
        }
        #endregion

        #region --- Opertions ---
        private void SumButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DevideButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionToServer();
        }
        #endregion

        #region --- Methods ---
        private void ConnectionToServer()
        {
        
            string message = DisplayOutput.Text;
            var data = Encoding.UTF8.GetBytes(message);

            //clientSocket.Connect(iPEnd);
            clientSocket.Send(data);

            byte[] buffer = new byte[256];
            int dataSize = 0;
            StringBuilder answerString = new StringBuilder();

            do
            {
                dataSize = clientSocket.Receive(buffer);
                answerString.Append(Encoding.UTF8.GetString(buffer, 0, dataSize));

            }
            while (clientSocket.Available > 0);

            ServerTerminalText.Text = $"{answerString.ToString()}\n";
            //clientSocket.Shutdown(SocketShutdown.Both);
            //clientSocket.Close();
            
}
        #endregion
    }
}
