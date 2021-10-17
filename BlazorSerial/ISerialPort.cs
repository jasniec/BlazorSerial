using BlazorSerial.Enums;
using System.Threading.Tasks;

namespace BlazorSerial
{
    public interface ISerialPort
    {
        bool IsConnected { get; }
        bool IsPortChoosen { get; }

        Task<ConnectResponseEnum> Connect(int baudRate);
        Task<bool> IsSupported();
        Task<RequestPortResponseEnum> RequestPort();
        Task Write(string text);
    }
}