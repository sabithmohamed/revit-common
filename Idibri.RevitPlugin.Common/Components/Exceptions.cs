using System;

namespace Idibri.RevitPlugin.Common
{
    public class CommandExceptionBase : Exception
    {
        public CommandExceptionBase() : base() { }
        public CommandExceptionBase(string message) : base(message) { }
        public CommandExceptionBase(string message, Exception innerException) : base(message, innerException) { }
    }

    public class MissingParameterException : CommandExceptionBase
    {
        public MissingParameterException() : base() { }
        public MissingParameterException(string message) : base(message) { }
        public MissingParameterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
