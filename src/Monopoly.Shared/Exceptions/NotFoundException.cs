using System;
using System.Runtime.Serialization;
using Monopoly.Shared.Exceptions.Base;

namespace Monopoly.Shared.Exceptions
{
    public class NotFoundException : ServiceException
    {
        public override int ResponseCode => 404;
        public override string DefaultMessage => "The specified resource was not found or does not exist";

        public NotFoundException() { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}