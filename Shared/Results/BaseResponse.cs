using System.Text.Json.Serialization;

namespace Shared.Results;
public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    [JsonConstructor]
    public BaseResponse(bool success,string message="")
    {
        Success = success;
        Message = message;
    }
}
