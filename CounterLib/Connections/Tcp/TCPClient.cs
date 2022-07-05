using CounterLib.Services;
using System;
using System.Net.Sockets;

namespace CounterLib.Connections.Tcp
{
    /// <summary>
    /// 
    /// </summary>
    public class TCPClient : BaseClient
    {
        public TCPClient(ConnectedObject connector) : base(connector)
        {
        }

        public override void Connect()
        {
            try
            {
                ((TcpConnectedObject)Connector).TcpClient = new TcpClient();

                ((TcpConnectedObject)Connector).TcpClient.Connect(ConnectionHelper.ServerIPAddress(), ConnectionHelper.Port());

                ((TcpConnectedObject)Connector).Stream = ((TcpConnectedObject)Connector).TcpClient.GetStream();
            }
            catch (Exception ex)
            {
                Print?.Invoke("метод Connect " + ex.Message);
            }
        }
    }
}