using System;
using System.Runtime.Serialization;

namespace Monopoly.Shared.Exceptions.Base
{
    public abstract class ServiceException : Exception
    {
        public abstract int ResponseCode { get; }
        public abstract string DefaultMessage { get; }
        
        public override string Message => base.Message ?? DefaultMessage;

        protected ServiceException() { }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected ServiceException(string message) : base(message) { }

        protected ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}