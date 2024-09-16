namespace MilkWala.Models
{
    public class ListApiResponse<T>
    {
        public int StatusCode { get; set; }
        public int TotalCount { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ListApiResponse(int statusCode, T data, int totalCount = 0, string message = null)
        {
            StatusCode = statusCode;
            Data = data;
            TotalCount = totalCount;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Request was successful.",
                400 => "Bad request.",
                404 => "Resource not found.",
                500 => "An unexpected error occurred.",
                _ => "Bad request",
            };
        }
    }

    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public bool Data { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, bool data,  string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

    }

}
