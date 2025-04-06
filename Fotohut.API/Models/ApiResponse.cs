namespace Fotohut.API.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ApiResponse()
    {
        Success = false;
        Message = string.Empty;
        Data = default!;
        Errors = new List<string>();
    }

    public ApiResponse(T data, string? message = null)
    {
        Success = true;
        Data = data;
        Message = message ?? string.Empty;
        Errors = new List<string>();
    }

    public ApiResponse(string errorMessage)
    {
        Success = false;
        Message = errorMessage;
        Data = default!;
        Errors = new List<string> { errorMessage };
    }
}