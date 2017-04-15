using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clamsocket
{
    class NetClamd
    {
        private string _ip;

        private int _port;

        public NetClamd(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        /// <summary>
        /// 发送命令并执行得到结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needResult"></param>
        /// <returns></returns>
        public List<string> SendCmdAndGetRes(string cmd, bool needResult = true)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                List<string> list = new List<string>();
                IPAddress ipaddress = IPAddress.Parse(_ip);
                socket.Connect(new IPEndPoint(ipaddress, _port));
                socket.Send(Encoding.ASCII.GetBytes(string.Format("n{0}\n", cmd)));
                if (needResult)
                {
                    string result = "...";
                    while (!string.IsNullOrEmpty(result))
                    {
                        byte[] dataArr = new byte[10240];
                        int len = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                len = socket.Receive(dataArr);
                                if (len > 0) break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("SendCmdAndGetRes异常:{0}", ex.Message);
                                Thread.Sleep(1000);
                            }
                        }
                        result = Encoding.ASCII.GetString(dataArr, 0, len);
                        if (!string.IsNullOrEmpty(result)) list.Add(result);
                    }
                    return list;
                }
            }
            return null;
        }


        /// <summary>
        /// 发送命令并执行得到结果
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="needResult"></param>
        /// <returns></returns>
        public string SendSimpleCmdAndGetRes(string cmd, bool needResult = true)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPAddress ipaddress = IPAddress.Parse(_ip);
                socket.Connect(new IPEndPoint(ipaddress, _port));
                socket.Send(Encoding.ASCII.GetBytes(string.Format("n{0}\n", cmd)));
                if (needResult)
                {
                    byte[] dataArr = new byte[10240];
                    int len = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            len = socket.Receive(dataArr);
                            if (len > 0) break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("SendCmdAndGetRes异常:{0}", ex.Message);
                            Thread.Sleep(100);
                        }
                    }
                    return Encoding.ASCII.GetString(dataArr, 0, len);
                }

            }
            return "";
        }
    }
}
