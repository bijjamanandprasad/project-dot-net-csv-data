namespace ClientApi.Models;

public class ApiResponse<T>
{
    public bool Status { get; set; }
    public int Code { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public object? Error { get; set; }

    public static ApiResponse<T> Success(T data, string? message = null, int code = 200)
    {
        return new ApiResponse<T>
        {
            Status = true,
            Code = code,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> CreateError(string message, int code = 400, object? error = null)
    {
        return new ApiResponse<T>
        {
            Status = false,
            Code = code,
            Message = message,
            Error = error
        };
    }
} 