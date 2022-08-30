using System;
using System.Runtime.Serialization;
using Monopoly.Shared.Exceptions.Base;

namespace Monopoly.Shared.Exceptions
{
    public class BadRequestException : ServiceException
    {
        public override int ResponseCode => 400;
        public override string DefaultMessage => "The supplied request was unable to be handled by the server";

        public BadRequestException() { }

        public BadRequestException(string message) : base(message) { }

        public BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }
}