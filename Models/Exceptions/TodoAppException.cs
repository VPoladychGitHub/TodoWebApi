namespace TodoWebApi.Models.Exceptions
{
    abstract public class TodoAppException:Exception
    {
        public TodoAppException()
        {
        }
        public TodoAppException(string? message) : base(message)
        {
        }
    }
}
