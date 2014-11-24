using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatureOfCode.Web.Models
{
    public class PostPreviewModel
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ContentSnippet { get; set; }
        public DateTime PublishDate { get; set; }
    }
}