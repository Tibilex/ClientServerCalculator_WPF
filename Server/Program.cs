using Server;
using System.Net;
using System.Net.Sockets;
using System.Text;

Calculator calculator = new Calculator();
//calculator.ServerUp();

calculator.ServerUp();

//const int PORT = 8081;
//const string IP = "127.0.0.1";

//IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(IP), PORT);
//Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

//try
//{
//    serverSocket.Bind(iPEnd);
//    serverSocket.Listen(10);
//    Socket clientSocket = serverSocket.Accept();
//    do
//    {
//        Console.WriteLine($"Server listen on {IP}:{PORT}");

//        int bytes = 0;
//        byte[] buffer = new byte[1024];
//        StringBuilder builder = new StringBuilder();
//        do
//        {
//            bytes = clientSocket.Receive(buffer);
//            builder.Append(Encoding.Unicode.GetString(buffer, 0, bytes));
//        } while (clientSocket.Available > 0);
//        Console.WriteLine($"client:\t{builder.ToString()}");

//        string msg = "Сообщение получено";
//        byte[] data = Encoding.Unicode.GetBytes(msg);
//        clientSocket.Send(data);

//    } while (true);

//    clientSocket.Close();
//    serverSocket.Shutdown(SocketShutdown.Both);
//    serverSocket.Close();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);


