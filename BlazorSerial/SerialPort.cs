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
        public bool IsPortChoosen { get; private set; }

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

            var connectionResult = Enum.Parse<ConnectResponseEnum>(await _jsRuntime.InvokeAsync<string>("blazorSerialConnect", baudRate));

            if (connectionResult == ConnectResponseEnum.Ok)
            {
                IsConnected = true;
            }

            return connectionResult;
        }

        public async Task Write(string text) => Enum.Parse<ConnectResponseEnum>(await _jsRuntime.InvokeAsync<string>("blazorSerialWriteText", text));
    }
}