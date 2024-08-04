using MrTakuVetClinic.Models;

namespace MrTakuVetClinic.Helpers
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> SuccessResponse<T>(int statusCode, object data)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Data = data
            };
        }

        public static ApiResponse<T> FailResponse<T>(int statusCode, object errors)
        {
            return new ApiResponse<T>
            { 
                StatusCode = statusCode,
                Errors = errors
            };
        }
    }
}
