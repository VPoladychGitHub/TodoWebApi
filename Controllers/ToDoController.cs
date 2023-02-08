using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TodoWebApi.Models;
using TodoWebApi.Models.Exceptions;
using TodoWebApi.Services;
using TodoWebApi.Models;
using Microsoft.Extensions.Localization;

namespace TodoWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("{culture:culture}/[controller]")]
    public class ToDoController : ControllerBase
    {
        Random rnd = new Random();
        private readonly TodoService todoService;
        private readonly IStringLocalizer<ToDoController> localizer;
        public ToDoController( TodoService todoService, IStringLocalizer<ToDoController> localizer)
        {
            this.todoService = todoService;
            this.localizer = localizer;
        }

        [AllowAnonymous]
        [HttpGet("/listtodo")]
        public IEnumerable<Todo> Get()
        {
            return (IEnumerable<Todo>)todoService.TodusListProp;
        }

        [HttpPost("/newtodo")]
        public IResult PostAddTodo([FromQuery] TestRequest testRequest)
        {
            try
            {
                todoService.AddTodo(new Todo { ID = rnd.Next(), MyTodo = testRequest.ValidTodo, DateTodo = DateTime.Now.AddDays(rnd.Next(5)) });
            }
            catch (Exception e)
            {
                throw new ValidationException("ValidationException", e);
            }
           return Results.Ok("record   add");
        }

        [HttpDelete("/deletedtodo")]
        public IResult  DeleteTodo([FromQuery] TestDelete   testDelete)
        {
            return  todoService.DeleteTodo(testDelete.Num, localizer);         
        }
    }
}
