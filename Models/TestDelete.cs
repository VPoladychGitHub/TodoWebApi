using FluentValidation;

namespace TodoWebApi.Models
{
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
