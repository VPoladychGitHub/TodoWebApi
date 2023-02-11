using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using TodoWebApi.Models;

namespace TodoWebApi.DataAccess.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.Property<DateOnly>(nameof(Todo.DateTodo))
                .HasColumnType("datetime2")
                .HasConversion<DateOnlyConvertor>();
        }
    }
    public class DateOnlyConvertor : ValueConverter<DateOnly, DateTime>
    {
        public DateOnlyConvertor():base(
            d => d.ToDateTime(TimeOnly.MinValue),
            d => DateOnly.FromDateTime(d)
            )
        {
        }
    }
}
