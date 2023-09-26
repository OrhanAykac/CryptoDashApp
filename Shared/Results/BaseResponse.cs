namespace Shared.Results;
public class BaseResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public BaseResponse(bool success)
    {
        Success = success;
    }
    public BaseResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
