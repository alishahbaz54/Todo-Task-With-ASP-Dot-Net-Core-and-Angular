using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi2.Models.Shared
{
    public class AppSettings
    {
        public static string ConnectionString()
        {
            return @"Data Source=DESKTOP-14240RM\SQLEXPRESS;Initial Catalog=TodoTaskDB1;Integrated Security=True";
        }
    }
}
