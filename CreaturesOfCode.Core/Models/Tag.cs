﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaturesOfCode.Core
{
    public class Tag : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; } 
    }
}
