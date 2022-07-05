using System.Linq;
using System.Net;

namespace CounterLib.Services
{
    /// <summary>
    /// Валидатор
    /// </summary>
    public class Validator : IValidator
    {
        public bool ValidateIpAddress(string ipString, out string errMessage)
        {
            bool output = ValidateIPv4(ipString);

            errMessage = "";

            if (output == false)
            {
                errMessage = StandardMessages.Validation.NotValidIp;
            }

            return output;
        }


        public bool ValidatePort(string portString, out string errMessage)
        {
            bool output = ValidatePortNumber(portString);

            errMessage = "";

            if (output == false)
            {
                errMessage = StandardMessages.Validation.NotValidPort;
            }

            return output;
        }


        /// <summary>
        /// Валидирует IPv4 Address
        /// </summary>
        /// <param name="ipString">IpAddress</param>
        /// <returns>True, если IpAddress валидный</returns>
        private bool ValidateIPv4(string ipString)
        {
            if (ipString.Count(c => c == '.') != 3)
            {
                return false;
            }

            return IPAddress.TryParse(ipString, out IPAddress address);
        }
        
        /// <summary>
        /// Валидирует IpAddress
        /// </summary>
        /// <param name="portString">Порт</param>
        /// <returns>True, если порт валидный</returns>
        private bool ValidatePortNumber(string portString)
        {
            bool result = int.TryParse(portString, out int portNumber);

            if (result == true &&
                portNumber > 0 &&
                portNumber <= 65535)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}