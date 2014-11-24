using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaturesOfCode.Core;
using CreaturesOfCode.Data;

namespace CreaturesOfCode.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Comment> _commentRepository; 
        
        public PostService(IRepository<Post> postRepositoryrepository, 
            IRepository<Category> categoryRepository, 
            IRepository<Tag> tagRepository, 
            IRepository<Comment> commentRepository)
        {
            _postRepository = postRepositoryrepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
        }

        public Post CreatePost(string title, string content, string category, List<string> tags)
        {
            var cat = _categoryRepository.Find(x => String.Equals(x.Name, category, StringComparison.CurrentCultureIgnoreCase)).SingleOrDefault() ??
                      _categoryRepository.Create(new Category
            {
                Name = category,
            });

            var post = _postRepository.Create(new Post
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow,
                Category = cat,
            });

            return post;
        }


        public Post GetPostById(int id)
        {
            return _postRepository.Get(id);
        }
    }
}
