using BlazorSerial.Enums;
using BlazorSerial.Exceptions;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BlazorSerial
{
    public class SerialPort : ISerialPort
    {
        public bool IsConnected { get; private set; }
        public bool IsPortChosen { get; private set; }

        private readonly IJSRuntime _jsRuntime;

        public SerialPort(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsSupported() => await _jsRuntime.InvokeAsync<bool>("blazorSerialIsSupported");

        public async Task<RequestPortResponseEnum> RequestPort()
        {
            var result = Enum.Parse<RequestPortResponseEnum>(await _jsRuntime.InvokeAsync<string>("blazorSerialGetPort"));

            if (result == RequestPortResponseEnum.Ok)
            {
                IsPortChosen = true;
            }

            return result;
        }

        public async Task<ConnectResponseEnum> Open(int baudRate)
        {
            if (!IsPortChosen)
            {
                throw new PortNotChoosenException();
            }

            if (IsConnected)
            {
                throw new AlreadyConnectedException();
            }

            var connectionResult = Enum.Parse<ConnectResponseEnum>(await _jsRuntime.InvokeAsync<string>("blazorSerialOpen", baudRate));

            if (connectionResult == ConnectResponseEnum.Ok)
            {
                IsConnected = true;
            }

            return connectionResult;
        }

        public async Task Close() => await _jsRuntime.InvokeVoidAsync("blazorSerialClose");

        public async Task Write(string text) => await _jsRuntime.InvokeAsync<string>("blazorSerialWriteText", text);
    }
}