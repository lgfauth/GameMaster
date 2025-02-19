using GameMasterDomain.Exceptions;

namespace GameMasterDomain.Models.Envelopes
{
    public class ResponseError<T> : IResponse<T>
    {
        public bool IsSuccess { get; } = false;

        public T Data { get; } = default!;

        public ErrorObject Error { get; } = default!;

        public ResponseError(T data)
        {
            Data = data;
        }

        public ResponseError(ErrorObject error)
        {
            Error = error;
        }
    }
}