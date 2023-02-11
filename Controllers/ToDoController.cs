using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TodoWebApi.Models;
using TodoWebApi.Models.Exceptions;
using TodoWebApi.Models;
using Microsoft.Extensions.Localization;
using TodoWebApi.DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("{culture:culture}/[controller]")]
    public class ToDoController : ControllerBase
    {
        Random rnd = new Random();
        private readonly IStringLocalizer<ToDoController> localizer;
        private readonly TodoContext todoContext;
        public ToDoController( IStringLocalizer<ToDoController> localizer, TodoContext todoContext)
        {
            this.localizer = localizer;
            this.todoContext = todoContext;
        }

        [AllowAnonymous]
        [HttpGet("/listtodo")]
        public async Task<IEnumerable<Todo>> Get()
        {
            var results = await todoContext.Todos.
                AsNoTracking()
               .ToListAsync();
            return results;
        }
   
        [HttpPost("/newtodo")]
        public async Task<ActionResult> PostAddTodo(Todo todo)
        {
            todoContext.Todos.Add(todo);
            await todoContext.SaveChangesAsync();
            return Ok("record   add");
        }

        [HttpDelete("/deletedtodo")]
        public async Task<ActionResult> DeleteTodo( int id)
        {
            /*  Todo td = todoContext.Todos.Where(x => x.ID == id).Single<Todo>();
            todoContext.SaveChangesAsync();
            return Ok("record   del");*/
            var td = await todoContext.Todos.FindAsync(id);
            if (td == null)
            {
                return NotFound();
            }
            todoContext.Todos.Remove(td);
            await todoContext.SaveChangesAsync();
            return Ok("record   del");
        }
    }
}
