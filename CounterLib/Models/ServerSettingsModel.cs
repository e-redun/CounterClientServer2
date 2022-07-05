using CounterLib.Enums;
using CounterLib.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace CounterLib.Models
{
    /// <summary>
    /// Модель сервера в клиентской части
    /// </summary>
    public class ServerSettingsModel : ObservableObject, IDataErrorInfo, ICloneable
    {
        #region Свойства


        private ConProtocols _conProtocol;
        
        /// <summary>
        /// Протокол соединения
        /// </summary>
        public ConProtocols ConProtocol
        {
            get { return _conProtocol; }
            set { OnPropertyChanged(ref _conProtocol, value); }
        }


        private string _serverIpAddress;

        /// <summary>
        /// Адрес сервера
        /// </summary>
        public string ServerIpAddress
        {
            get { return _serverIpAddress; }
            set { OnPropertyChanged(ref _serverIpAddress,value); }
        }

        private string _serverPort;

        /// <summary>
        /// Порт сервера
        /// </summary>
        public string ServerPort
        {
            get { return _serverPort; }
            set { OnPropertyChanged(ref _serverPort, value); }
        }

        #endregion

        #region Валидация WPF
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case "ServerIpAddress":
                        ValidateIpAddress(out error);
                        break;

                    case "ServerPort":
                        ValidatePort(out error);
                        break;
                }

                return error;
            }
        }

        public string Error
        {
            get { return ""; }
        }
        #endregion

        #region Методы валидации

        public bool ValidateIpAddress(out string errMessage)
        {
            bool output = ValidateIPv4();

            errMessage = "";

            if (output == false)
            {
                errMessage = StandardMessages.Validation.NotValidIp;
            }

            return output;
        }


        public bool ValidatePort(out string errMessage)
        {
            bool output = ValidatePortNumber();

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
        private bool ValidateIPv4()
        {
            if (ServerIpAddress.Count(c => c == '.') != 3)
            {
                return false;
            }

            return IPAddress.TryParse(ServerIpAddress, out IPAddress address);
        }

        /// <summary>
        /// Валидирует IpAddress
        /// </summary>
        /// <param name="portString">Порт</param>
        /// <returns>True, если порт валидный</returns>
        private bool ValidatePortNumber()
        {
            bool result = int.TryParse(ServerPort, out int portNumber);

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

        #endregion

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            var settings = (ServerSettingsModel)obj;
            if (settings == null)
                return false;
            return settings.ConProtocol == ConProtocol
                && settings.ServerIpAddress == ServerIpAddress
                && settings.ServerPort == ServerPort;
        }
    }
}