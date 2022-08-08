using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
    internal class Calculator
    {
        #region --- Fields & prop ----
        private double _firstnumber;
        private double _secondNumber;
        private double _calculateResult;
        private char _mathOperation;

        private const int PORT = 8081;
        private const string IP = "127.0.0.1";
        private Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public char MathOperation
        {
            get { return _mathOperation; }
            set { _mathOperation = value; }
        }

        public double CalculateResult
        {
            get { return _calculateResult; }
            set { _calculateResult = value; }
        }

        public double SecondNumber
        {
            get { return _secondNumber; }
            set { _secondNumber = value; }
        }

        public double FirstNumber
        {
            get { return _firstnumber; }
            set { _firstnumber = value; }
        }
        #endregion
        #region --- Math operatons ---
        private string AnswerServerToUser(string userString)
        {
            double result = 0;
            string[] splitExpression = userString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            double firstNum = Convert.ToDouble(StringDotCheck(splitExpression[0]));         
            char operation = Convert.ToChar(splitExpression[1]);
            double secondNum = Convert.ToDouble(StringDotCheck(splitExpression[2]));

            switch (operation)
            {
                case '＋':
                    result = Sum(firstNum, secondNum);             
                    break;
                case '－':
                    result = Substract(firstNum, secondNum);
                    break;
                case '×':
                    result = Multiply(firstNum, secondNum);
                    break;
                case '÷':
                    if (NullDevideResult(firstNum, secondNum))
                        result = Devide(firstNum, secondNum);
                    else
                        Console.WriteLine("dont devide by 0");
                    break;
            }
            return result.ToString();
        }

        private double Sum(double firstNum, double secondNum) => _calculateResult = firstNum + secondNum;
        
        private double Substract(double firstNum, double secondNum) => _calculateResult = firstNum - secondNum;
        
        private double Multiply(double firstNum, double secondNum) => _calculateResult = firstNum * secondNum;
        
        private double Devide(double firstNum, double secondNum) => _calculateResult = firstNum / secondNum;
        
        private bool NullDevideResult(double firstNum, double secondNum)
        {
            if ((firstNum == 0) || (secondNum == 0)) { return false; }
            else return true;
        }

        private string StringDotCheck(string str)
        {
            string pattern = @"\W";
            string target = ",";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(str, target);
            return result;
        }

        #endregion
        #region --- Connections ---
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
                listner.Send(Encoding.UTF8.GetBytes(AnswerServerToUser($"{dataString.ToString()}")));
                listner.Shutdown(SocketShutdown.Both);
                listner.Close();
            }
        }

        public void QuestionsToClient()
        {         
           ServerUp();
        }
        #endregion
    }
}
