using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace clamsocket
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("--------------开始执行----------------");

            try
            {
                NetClamd netClamd = new NetClamd("127.0.0.1", 3310);

                string result = netClamd.SendSimpleCmdAndGetRes("PING");
                Console.WriteLine(result);

                //result = netClamd.SendSimpleCmdAndGetRes("RELOAD");
                //Console.WriteLine(result);

                List<string> list = netClamd.SendCmdAndGetRes("MULTISCAN /var/test/");
                foreach (string str in list)
                { Console.Write(str); }
            }
            catch(Exception ex)
            {
                Console.WriteLine("执行出现异常:{0}", ex.Message);
            }
            Console.Read();
        }
    }
}