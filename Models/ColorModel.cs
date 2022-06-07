using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoTaskApi2.Models
{
    public class ColorModel
    {
        public int Id { get; set; }
        public string ColorCode { get; set; }
        public int TodoId{ get; set; }
    }
}
