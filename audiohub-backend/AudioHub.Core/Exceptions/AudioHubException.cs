using System;

namespace AudioHub.Core.Exceptions
{
    public class AudioHubException : Exception
    {
        internal AudioHubException() : base() { }
        internal AudioHubException(string message) : base(message) { }
        internal AudioHubException(string message, Exception innerException) : base(message, innerException) { }
    }
}
