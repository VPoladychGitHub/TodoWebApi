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
  
}
