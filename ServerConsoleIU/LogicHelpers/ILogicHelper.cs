using System.Threading.Tasks;
using CounterLib.Enums;

namespace ServerConsoleIU.LogicHelpers
{
    public interface ILogicHelper
    {
        /// <summary>
        /// Запускает сервер
        /// </summary>
        /// <param name="protocol">Протокол</param>
        /// <returns>Задача (Task) - прослушивание порта</returns>
        Task RunServer(ConProtocols protocol);
    }
}