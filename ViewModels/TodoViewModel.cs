using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TodoTaskApi2.Models;
using TodoTaskApi2.Models.Shared;

namespace TodoTaskApi2.ViewModels
{
    public class TodoViewModel
    {
        string query = "";
        public void InsertItem(TodoModel todo)
        {
            //int i;
            //using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            //{
            //    query = "INSERT INTO TodoTaskDB1(Descriptoin, IsDone, CreatedOn)VALUES(@desc, @isDone, @currentDateTime)";
            //    using(SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.CommandType = System.Data.CommandType.Text;
            //        if (conn.State != System.Data.ConnectionState.Open)
            //            conn.Open();
            //        cmd.Parameters.AddWithValue("@desc", todo.Description);
            //        cmd.Parameters.AddWithValue("@isDone", todo.IsDone);
            //        cmd.Parameters.AddWithValue("@currentDateTime", System.DateTime.Now);
            //         cmd.ExecuteNonQuery();
            //    }
            //}
        }

        public List<TodoModel> GetAllTodo()
        {
            List<TodoModel> todos = new List<TodoModel>();
            using (SqlConnection conn = new SqlConnection(AppSettings.ConnectionString()))
            {
                
                query = "SELECT Id, Description, IsDone, ColorID, Color, Position FROM TodoTable ORDER BY Position";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TodoModel todo = new TodoModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Description = reader["Description"].ToString(),
                                IsDone = Convert.ToBoolean(reader["IsDone"]),
                                //ColorId = Convert.ToInt32(reader["ColorID"])
                                ColorCode = reader["Color"].ToString(),
                                Position = Convert.ToInt32(reader["Position"])
                            };
                            todos.Add(todo);
                        }
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return todos;
        }
    }
}
