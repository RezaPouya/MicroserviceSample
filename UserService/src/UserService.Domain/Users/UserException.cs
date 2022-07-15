using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace UserService.Users
{
    public class UserException : BusinessException
    {
        public UserException(string code = null,
            string message = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(code, "UserService_User_Exception : " + message, details, innerException, logLevel)
        {
            Code = code;
            Details = details;
            LogLevel = logLevel;
        }

        public UserException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
    }
}