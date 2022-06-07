using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi2.Models;
using TodoTaskApi2.Models.Shared;

namespace TodoTaskApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ColorController : ControllerBase
    {
        [EnableCors]
        [HttpPut("{id}")]
        public async Task<ActionResult<ColorModel>> PostColor(int id, ColorModel colorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "UPDATE TodoTable SET Color = @color WHERE Id = " + id;
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.Parameters.AddWithValue("@color", colorModel.ColorCode);
                        //cmd.Parameters.AddWithValue("@todoId", colorModel.TodoId);
                        int res = cmd.ExecuteNonQuery();
                        if(res > 0)
                        {
                            return Ok();
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ColorModel>> GetColor(int id)
        {
            ColorModel colorModel = new ColorModel();
            using(SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "SELECT c.ColorCode FROM SetColorTable c INNER JOIN TodoTable t ON t.ColorID = C.Id WHERE t.ColorID = " + id;
                await using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (read.HasRows)
                        {
                            colorModel = new ColorModel
                            {
                                ColorCode = read["ColorCode"].ToString()
                            };
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                   
                }
                return colorModel;
            }
        }
        
        [HttpPut("[action]/{id}")]
        public async Task<ActionResult<TodoModel>> PutPosition(int id, TodoModel todo)
        {
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                string query = "UPDATE TodoTable SET Position = @position WHERE Id = " + id;
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.Parameters.AddWithValue("@position", todo.Position);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            return Ok();
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }    
}
