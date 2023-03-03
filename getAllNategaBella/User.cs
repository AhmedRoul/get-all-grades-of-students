using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace getAllNategaBella
{
    public  class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> subject ;

        public string  Total;

        public User(int id, string name, List<string> subject, string total)
        {
            Id = id;
            Name = name;
            this.subject = subject;
            Total = total;
        } 


    }
}
