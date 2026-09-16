namespace EmployeeApi.Response
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = Array.Empty<object>();
        public List<string> Error { get; set; } = [];

        public static ApiResponse Success(object data, string message = "Success", int code = StatusCodes.Status200OK)
        {
            return new ApiResponse
            {
                Code = code,
                Message = message,
                Data = data,
                Error = []
            };
        }

        public static ApiResponse Failure(string message, IEnumerable<string>? errors = null, int code = StatusCodes.Status400BadRequest)
        {
            return new ApiResponse
            {
                Code = code,
                Message = message,
                Data = Array.Empty<object>(),
                Error = errors?.ToList() ?? []
            };
        }
    }
}
