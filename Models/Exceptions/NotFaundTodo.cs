namespace TodoWebApi.Models.Exceptions
{
    public class NotFaundTodo: TodoAppException
    {
        public NotFaundTodo(string entityName) : this(entityName, null)
        {
        }
        public NotFaundTodo(string entityName, string? message) : base(message)
        {
            EntityName = entityName;
        }
        public string EntityName { get; set; }
    }
}
