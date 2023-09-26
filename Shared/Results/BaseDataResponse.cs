namespace RiseX.Shared.Results;
public class BaseDataResponse<T> : BaseResponse
{
    public T Data { get; set; }
    public BaseDataResponse(T data, bool success) : base(success)
    {
        Data = data;
    }
    public BaseDataResponse(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }
}
