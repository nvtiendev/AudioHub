using System;

namespace AudioHub.Core.Exceptions
{
    public class AudioHubException : Exception
    {
        public AudioHubException() : base() { }
        public AudioHubException(string message) : base(message) { }
        public AudioHubException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AudioHubAPIException : AudioHubException
    {
        public int ErrorCode { get; }

        public AudioHubAPIException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
