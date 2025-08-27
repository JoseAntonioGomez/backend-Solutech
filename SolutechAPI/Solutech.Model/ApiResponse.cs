namespace Solutech.Model
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public T? Data { get; set; }
    }
}
