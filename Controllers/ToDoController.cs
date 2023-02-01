using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TodoWebApi.Models;
using TodoWebApi.Services;
namespace TodoWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        Random rnd = new Random();
        private readonly TodoService todoService;
        public ToDoController( TodoService todoService)
        {
            this.todoService = todoService;
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
            todoService.AddTodo(new Todo { ID = rnd.Next(), MyTodo = testRequest.ValidTodo, DateTodo = DateTime.Now.AddDays(rnd.Next(5)) });
            return Results.Ok("record   add");
        }

        [HttpDelete("/deletedtodo")]
        public IResult  DeleteTodo([FromQuery] TestDelete   testDelete)
        {
            return  todoService.DeleteTodo(testDelete.Num);         
        }
           
    }
}
