using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaturesOfCode.Core.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; } 
    }
}
