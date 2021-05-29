using System;

namespace Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }
    }
    
    
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }

        public UnauthorizedException(string message) : base(message) { }
    }
    
    public class LogicException : Exception
    {
        public LogicException() { }

        public LogicException(string message) : base(message) { }
    }
}