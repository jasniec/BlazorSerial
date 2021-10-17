# BlazorSerial

BlazorSerial is a library that wraps the serial API in chrome based browsers [check it here](https://developer.mozilla.org/en-US/docs/Web/API/Web_Serial_API)

## Installation

Use the package manager to install BlazorSerial.

```bash
PM> Install-Package BlazorSerial
```

Or add [nuget](https://www.nuget.org/packages/BlazorSerial) instead

Then, in your index file (wwwroot/index.html) add a line at the bottom of the body

```html
<script src="_content/BlazorSerial/blazorSerial.js"></script>
```

And register dependency in Program.cs file

```csharp
builder.Services.AddBlazorSerial();
```

## Usage

To write text to the serial port, you have to ask user to choose a port, if successfully chosen, you have to connect and then send some text.

```csharp
@inject ISerialPort Serial

// ...

@code{
private async Task WriteHelloWorld()
{
    if (await Serial.RequestPort() == BlazorSerial.Enums.RequestPortResponseEnum.Ok
     && await Serial.Connect(115200) == BlazorSerial.Enums.ConnectResponseEnum.Ok)
    {
        await Serial.Write("Hello World!");
    }
}

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
