namespace Core.Domain.Models;

public class ApiResponse<T>
{
    public T Response { get; set; }
    public string Error { get; set; }
    public int StatusCode { get; set; }
}