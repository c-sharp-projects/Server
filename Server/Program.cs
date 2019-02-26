using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
class MarvellousServer
{
    public static string GetLocalIPAddress()
    {
        Console.WriteLine("Marvellous Web : Host name - {0}", Dns.GetHostName());

        var Marvelloushost = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in Marvelloushost.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Marvellous Web :No network adapters with an IPv4 address in the system!");
    }
    public static void Main(string[] args)
    {
        int MarvellousPort = 0;
        string MarvellousIP = null;

        Socket s = null;
        TcpListener myList = null;

        try
        {
            MarvellousIP = GetLocalIPAddress();
            MarvellousPort = 21000;

            IPAddress ipAd = IPAddress.Parse(MarvellousIP);
            Console.WriteLine("Marvellous Web : Server started ... ");

            myList = new TcpListener(ipAd, MarvellousPort);
            myList.Start();

            Console.WriteLine("Marvellous Web : Server started at port : " + MarvellousPort);
            Console.WriteLine("Marvellous Web : The local End point is :" + myList.LocalEndpoint);
            Console.WriteLine("Marvellous Web : Server Waiting for a connection ....");

            s = myList.AcceptSocket();
           

            Console.WriteLine("Marvellous Web : Connection Established with client....");
            Console.WriteLine("Marvellous Web : Connection accepted from " + s.RemoteEndPoint);

            byte[] b = new byte[100];
            int k = s.Receive(b);

            Console.WriteLine("Marvellous Web : Message Received ...");

            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(b[i]));
            }

            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("Marvellous Web :The string was received by the server ..."));

        }
        catch (Exception e)
        {
            Console.WriteLine("Marvellous Web : Exception - " + e.StackTrace);
        }
        finally
        {
            Console.WriteLine("\nMarvellous Web : Deallocating all resources ...");
            if (s != null)
            {
                s.Close();
            }
            if (myList != null)
            {
                myList.Stop();
            }
        }
    }
}