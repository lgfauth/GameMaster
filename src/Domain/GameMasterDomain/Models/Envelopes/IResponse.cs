using GameMasterDomain.Exceptions;
using System.Text.Json.Serialization;

namespace GameMasterDomain.Models.Envelopes
{
    public interface IResponse<T>
    {
        bool IsSuccess { get; }
        T Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        ErrorObject Error { get; }
    }
}
