using System;
using System.Collections.Generic;

namespace CreaturesOfCode.Core
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Category Category { get; set; }
    }
}
