namespace BlazorSerial.Enums
{
    public enum ConnectResponseEnum
    {
        Unknown,
        Ok,

        /// <summary>
        /// Indicates that the port is already open.
        /// </summary>
        InvalidStateError,

        /// <summary>
        /// Indicates that the attempt to open the port failed.
        /// </summary>
        NetworkError,
    }
}