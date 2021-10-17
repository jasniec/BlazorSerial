namespace BlazorSerial.Enums
{
    public enum RequestPortResponseEnum
    {
        Unknown,
        Ok,

        /// <summary>
        ///  Feature Policy restricts use of this API or a permission to use it has not granted via a user gesture.
        /// </summary>
        SecurityError,

        /// <summary>
        /// User does not select a port when prompted
        /// </summary>
        AbortError,
    }
}