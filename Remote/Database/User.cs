using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote.Database
{
    public class User
    {
        public int id { get; set; }

        public User(int id)
        {
            this.id = id;
        }
    }
}
