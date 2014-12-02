using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatureOfCode.Web
{
    public class ReadPostModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime PublishDate { get; set; }
        public List<TagModel> Tags { get; set; }
        public Dictionary<string, int> AllTags { get; set; }
        public List<string> AllCategories { get; set; }
    }
}