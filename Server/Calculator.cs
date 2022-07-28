using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Calculator
    {
        private int _firstnumber;
        private int _secondNumber;
        private int _calculateResult;
        private char _mathOperation;

        private const int PORT = 8081;
        private const string IP = "127.0.0.1";
        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public char MathOperation
        {
            get { return _mathOperation; }
            set { _mathOperation = value; }
        }

        public int CalculateResult
        {
            get { return _calculateResult; }
            set { _calculateResult = value; }
        }

        public int SecondNumber
        {
            get { return _secondNumber; }
            set { _secondNumber = value; }
        }

        public int FirstNumber
        {
            get { return _firstnumber; }
            set { _firstnumber = value; }
        }

        private int Sum(int firstNum, int secondNum) => _calculateResult = firstNum + secondNum;
        
        private int Substract(int firstNum, int secondNum) => _calculateResult = firstNum - secondNum;
        
        private int Multiply(int firstNum, int secondNum) => _calculateResult = firstNum * secondNum;
        
        private int Devide(int firstNum, int secondNum) => _calculateResult = firstNum / secondNum;
        
        private bool NullDevideResult(int firstNum, int secondNum)
        {
            if ((firstNum == 0) || (secondNum == 0)) { return false; }
            else return true;
        }

        public void ServerUp()
        {
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(iPEnd);
            serverSocket.Listen(5);
            Console.WriteLine("Сервер поднят");

            while (true)
            {
                var listner = serverSocket.Accept();
                byte[] buffer = new byte[256];
                int dataSize = 0;
                StringBuilder dataString = new StringBuilder();

                do
                {
                    dataSize = listner.Receive(buffer);
                    dataString.Append(Encoding.UTF8.GetString(buffer, 0, dataSize));
                }
                while (listner.Available > 0);

                Console.WriteLine(dataString.ToString());
                listner.Send(Encoding.UTF8.GetBytes("Ok!"));
                //listner.Shutdown(SocketShutdown.Both);
                //listner.Close();
            }
        }

        public void ListenClient()
        {
            var listner = serverSocket.Accept();
            byte[] buffer = new byte[256];
            int dataSize = 0;
            StringBuilder dataString = new StringBuilder();

            do
            {
                dataSize = listner.Receive(buffer);
                dataString.Append(Encoding.UTF8.GetString(buffer, 0, dataSize));

            }
            while(listner.Available > 0);

            Console.WriteLine(dataString.ToString());

            listner.Send(Encoding.UTF8.GetBytes("Ok!"));
            listner.Shutdown(SocketShutdown.Both);
            listner.Close();
        }
    }
}
