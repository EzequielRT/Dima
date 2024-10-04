using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response<T>
{
    private readonly int _code;

    [JsonConstructor]
    public Response()
    {
        _code = Configuration.DEFAULT_STATUS_CODE;
    }

    public Response(T? data, int code = Configuration.DEFAULT_STATUS_CODE, string? message = null)
    {
        _code = code;
        Data = data;
        Message = message;
    }
    
    public bool IsSuccess => _code is >= 200 and <= 299;
    public string? Message { get; set; }
    public T? Data { get; set; }
}
