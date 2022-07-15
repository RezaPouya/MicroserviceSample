using Microsoft.Extensions.Logging;
using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace IdentityService.AuthUsers
{
    public class AuthUserException : BusinessException
    {
        public AuthUserException(string code = null,
            string message = null,
            string details = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(code, "IdentityService_AuthUser_Exception : " + message, details, innerException, logLevel)
        {
            Code = code;
            Details = details;
            LogLevel = logLevel;
        }

        public AuthUserException(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }
    }
}