using System.Text.Json.Serialization;

namespace Shared.Results;
public class BaseDataResponse<T> : BaseResponse
{
    public T Data { get; set; }

    [JsonConstructor]
    public BaseDataResponse(T data, bool success, string message="") : base(success, message)
    {
        Data = data;
    }
}
