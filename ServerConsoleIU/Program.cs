using CounterLib.Enums;
using CounterLib.Services;
using ServerConsoleIU.LogicHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerConsolIU
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalHelper.Initialize();

            ILogicHelper logicHelper = new LogicHelper();

            List<Task> Servers = new List<Task>();

            Servers.Add(logicHelper.RunServer(ConProtocols.Socket));
            Servers.Add(logicHelper.RunServer(ConProtocols.Tcp));

            Task.WaitAll(Servers.ToArray());
        }
    }
}