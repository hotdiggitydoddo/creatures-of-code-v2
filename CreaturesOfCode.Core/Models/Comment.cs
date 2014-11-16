﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreaturesOfCode.Core
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Post Post { get; set; }
        public Comment ParentComment { get; set; }
    }
}