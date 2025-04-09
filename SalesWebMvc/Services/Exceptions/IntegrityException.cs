namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : Exception
    {
        public IntegrityException()
        {
        }
        public IntegrityException(string? message) : base(message)
        {
        }
    }
}
