using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TodoWebApi.Models;
using TodoWebApi.Services;
namespace TodoWebApi.Controllers
{
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

        [HttpGet("/listtodo")]
        public IEnumerable<Todo> Get()
        {
            return (IEnumerable<Todo>)todoService.TodusListProp;
        }

        [HttpPost("/addtodo")]
        public void PostAddTodo([FromQuery] TestRequest testRequest)
        {
            todoService.AddTodo(new Todo { ID = rnd.Next(), MyTodo = testRequest.ValidTodo, DateTodo = DateTime.Now.AddDays(rnd.Next(5)) });
         
        }

        [HttpDelete("/deletetodo")]
        public string  DeleteTodo([FromQuery] TestDelete   testDelete)
        {
            return  todoService.DeleteTodo(testDelete.Num);         
        }
           
    }
}
