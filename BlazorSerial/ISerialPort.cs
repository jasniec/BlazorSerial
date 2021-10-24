using BlazorSerial.Enums;
using System.Threading.Tasks;

namespace BlazorSerial
{
    public interface ISerialPort
    {
        bool IsConnected { get; }
        bool IsPortChosen { get; }

        Task<ConnectResponseEnum> Open(int baudRate);
        Task<bool> IsSupported();
        Task<RequestPortResponseEnum> RequestPort();
        Task Write(string text);
    }
}