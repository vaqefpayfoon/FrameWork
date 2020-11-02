using Serilog;
using System;
using System.Net.Sockets;

namespace KavoshFrameWorkCore
{
    public class CustomPingServer
    {
        public CustomPingServer()
        {
        }
        public static bool IsADSAlive(string hostIp, int hostport)
        {
            bool flag;
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    tcpClient.Connect(hostIp, hostport);
                    flag = true;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                flag = false;
            }
            return flag;
        }
    }
}
