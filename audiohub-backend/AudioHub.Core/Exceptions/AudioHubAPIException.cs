using System.Diagnostics;

namespace AudioHub.Core.Exceptions
{
    [DebuggerDisplay($"API exception: {{{nameof(ErrorCode)}}}: {{{nameof(Message)}}}")]
    public class AudioHubAPIException : AudioHubException
    {
        public int ErrorCode { get; }

        internal AudioHubAPIException() : base() { }

        internal AudioHubAPIException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }       
        
        public override string ToString()
        {
            return base.ToString().Replace(Message, $"{ErrorCode}: {Message}");
        }
    }
}
