namespace MrTakuVetClinic.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public object Errors { get; set; }
    }
}
