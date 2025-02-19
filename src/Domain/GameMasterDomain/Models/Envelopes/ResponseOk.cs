using GameMasterDomain.Exceptions;

namespace GameMasterDomain.Models.Envelopes
{
    public class ResponseOk<T> : IResponse<T>
    {
        public bool IsSuccess { get; } = true;

        public T Data { get; } = default!;

        public ErrorObject? Error { get; }

        public ResponseOk(T data)
        {
            Data = data;
        }
    }
}
