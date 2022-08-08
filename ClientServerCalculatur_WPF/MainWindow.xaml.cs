using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
        IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);

        public MainWindow()
        {
            InitializeComponent();
            s_window = this;            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
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
            OutputChar.Text = "＋";
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            OutputChar.Text = "－";
        }

        private void DevideButton_Click(object sender, RoutedEventArgs e)
        {
            OutputChar.Text = "÷";
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            OutputChar.Text = "×";
        }

        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            if ((OutputNumberOne.Text != "") || (OutputNumberTwo.Text != "") || (OutputChar.Text != "")) 
            {
                string expression = $"{OutputNumberOne.Text} {OutputChar.Text} {OutputNumberTwo.Text}";
                ConnectionToServer(expression);           
            }
            else
            {
                ServerTerminalText.Text = "Заполните все поля или укажите операцию\n";
            }
        }
        #endregion

        #region --- Methods ---
        private void ConnectionToServer(string expression)
        {
            string message = expression;
            byte[] data = Encoding.UTF8.GetBytes(message);

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(iPEnd);
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

            ServerResult.Text = $"{answerString.ToString()}\n";
            ServerTerminalText.Text += $"{expression} = {answerString.ToString()}\n";
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();

        }
        #endregion
    }
}
