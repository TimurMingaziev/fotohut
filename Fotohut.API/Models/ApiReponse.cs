namespace Fotohut.API.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ApiResponse()
    {
        Errors = new List<string>();
    }

    public ApiResponse(T data, string message = null)
    {
        Success = true;
        Data = data;
        Message = message;
        Errors = new List<string>();
    }

    public ApiResponse(string errorMessage)
    {
        Success = false;
        Message = errorMessage;
        Errors = new List<string> { errorMessage };
    }
}