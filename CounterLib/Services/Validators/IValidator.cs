namespace CounterLib.Services
{
    public interface IValidator
    {
        bool ValidateIpAddress(string ipString, out string errMessage);
        bool ValidatePort(string portString, out string errMessage);
    }
}