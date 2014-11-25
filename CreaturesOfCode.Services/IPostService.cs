using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaturesOfCode.Core;

namespace CreaturesOfCode.Services
{
    public interface IPostService
    {
        Post CreatePost(string title, string content, string category, string tags);
        Post GetPostById(int id);
        List<Post> GetLatestPosts(int quantity = 5);
    }
}
