using FluentValidation;

namespace TodoWebApi.Models
{
    public class TestRequest
    {
        public string ValidTodo { get; set; }

    }
    public class TestRequestValidator : AbstractValidator<TestRequest>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.ValidTodo). 
                NotEmpty().
                MinimumLength(4);

        }
    }

    public class TestDelete
    {
        public int Num { get; set; }
    }

    public class DeleteValidator : AbstractValidator<TestDelete>
    {
        public DeleteValidator()
        {
            RuleFor(x => x.Num).
                   NotEmpty().
                   InclusiveBetween(0, int.MaxValue);
        }
    }
}
