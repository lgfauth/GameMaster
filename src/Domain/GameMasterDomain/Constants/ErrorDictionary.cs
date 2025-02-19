using GameMasterDomain.Exceptions;

namespace GameMasterDomain.Constants
{
    public class ErrorDictionary
    {
        public static ErrorObject DatabaseError(string message) => new ErrorObject { Details = message, ErrorCode = "DB401" };
        public static ErrorObject ServiceError(string message) => new ErrorObject { Details = message, ErrorCode = "SR400" };
        public static ErrorObject HandledError(string message) => new ErrorObject { Details = message, ErrorCode = "HD400" };
        public static ErrorObject GeneralError(string message) => new ErrorObject { Details = message, ErrorCode = "GE325" };
        public static ErrorObject NotFoundError(string message) => new ErrorObject { Details = message, ErrorCode = "NF404" };
    }
}
