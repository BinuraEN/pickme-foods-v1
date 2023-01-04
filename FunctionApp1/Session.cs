using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public  class Session
    {
        public String Title { get; set; }
        public String Description{ get; set; }
        public int Duration { get; set; }
        public Guid Id { get; set; }
    }
}
