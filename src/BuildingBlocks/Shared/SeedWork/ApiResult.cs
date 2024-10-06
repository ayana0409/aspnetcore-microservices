namespace Shared.SeedWork
{
    public class ApiResult<T>
    {
        ApiResult() { }

        public ApiResult(bool isSuccessded, string? message = null) 
        {
            Message = message ?? string.Empty;
            IsSuccessded = isSuccessded;
        }
        public ApiResult(bool isSuccessded, T data, string? message = null)
        {
            Data = data;
            Message = message ?? string.Empty;
            IsSuccessded = isSuccessded;
        }

        public bool IsSuccessded { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }
}
