using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi2.Models;
using TodoTaskApi2.Models.Shared;
using TodoTaskApi2.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoTaskApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        // GET: api/<TodoController>
        [EnableCors]
        [HttpGet]
        public IEnumerable<TodoModel> Get()
        {
            TodoViewModel todoViewModel = new TodoViewModel();
            List<TodoModel> todos = todoViewModel.GetAllTodo();
            return todos;
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoModel>> GetTodoById(int id)
        {
            TodoModel todo;
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "SELECT Id, Description, IsDone FROM TodoTable WHERE Id = " + id;
                await using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (!read.HasRows)
                        {
                            return NotFound();
                        }
                        else
                        {

                            todo = new TodoModel
                            {
                                Id = Convert.ToInt32(read["Id"]),
                                Description = read["Description"].ToString(),
                                IsDone = Convert.ToBoolean(read["IsDone"])
                            };
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return todo;
        }
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<TodoController>
        //[EnableCors]
        [HttpPost]
        public async Task<ActionResult<TodoModel>> PostTodo(TodoModel todo)
        {
            todo.IsDone = false;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
               string query = "INSERT INTO TodoTable(Description, IsDone, CreatedOn, ColorID) VALUES(@desc, @isDone, @currentDateTime, @colorCode)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.Parameters.AddWithValue("@desc", todo.Description);
                        cmd.Parameters.AddWithValue("@isDone", todo.IsDone);
                        cmd.Parameters.AddWithValue("@currentDateTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@colorCode", todo.ColorId);
                        
                        int getRes = cmd.ExecuteNonQuery();
                        if (getRes > 0)
                        {
                            return Ok();
                        }
                        return BadRequest();
                    }
                    catch(Exception ex) {
                        throw ex;
                    }
                }
            }
        }
       
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TodoModel todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            //TodoModel todo = new TodoModel();
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
                {
                    string query = "UPDATE TodoTable SET Description = @desc, UpdatedOn = @upDate WHERE Id = " + id;
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.Parameters.AddWithValue("@desc", todo.Description);
                        cmd.Parameters.AddWithValue("@upDate", DateTime.Now);
                        int getRes = cmd.ExecuteNonQuery();
                        if(getRes > 0)
                        {
                            return NoContent();
                        }
                        conn.Close();

                    }
                }
            }
            return BadRequest();
        }
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "DELETE FROM TodoTable WHERE Id = " + id;
                 using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            return NoContent();
                        }
                        return BadRequest();
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        //[Route("api/[controller]/[action]")]
        [HttpPut("[action]/{id}")]
        //[Route("api/todo/done/{id}")]
        public async Task<ActionResult> PutDone(int id)
        {
            using(SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "UPDATE TodoTable SET IsDone = @done WHERE Id = " + id;
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.Parameters.AddWithValue("@done", true);
                        int res = cmd.ExecuteNonQuery();
                        if (res > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return NoContent();
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
