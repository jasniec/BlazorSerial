using BlazorSerial.Enums;
using BlazorSerial.Exceptions;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorSerial
{
    public class BlazorSerial
    {
        public bool IsConnected { get; private set; }
        public bool IsPortChoosen { get; private set; }

        private readonly IJSRuntime _jsRuntime;

        public BlazorSerial(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsSupported() => await _jsRuntime.InvokeAsync<bool>("blazorSerialIsSupported");

        public async Task<RequestPortResponseEnum> RequestPort()
        {
            var result = await _jsRuntime.InvokeAsync<RequestPortResponseEnum>("blazorSerialGetPort");

            if (result == RequestPortResponseEnum.Ok)
            {
                IsPortChoosen = true;
            }

            return result;
        }

        public async Task<ConnectResponseEnum> Connect(int baudRate)
        {
            if (!IsPortChoosen)
            {
                throw new PortNotChoosenException();
            }

            if (IsConnected)
            {
                throw new AlreadyConnectedException();
            }

            var connectionResult = await _jsRuntime.InvokeAsync<ConnectResponseEnum>("blazorSerialConnect", baudRate);

            if (connectionResult == ConnectResponseEnum.Ok)
            {
                IsConnected = true;
            }

            return connectionResult;
        }

        public async Task Write(string text) => await _jsRuntime.InvokeAsync<ConnectResponseEnum>("blazorSerialWriteText", text);
    }
}