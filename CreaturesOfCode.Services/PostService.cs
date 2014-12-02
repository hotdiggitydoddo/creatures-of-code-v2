using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public Post CreatePost(string title, string content, string category, string tags)
        {
            var cat = _categoryRepository.Find(x => x.Name.ToLower() == category.ToLower()).SingleOrDefault() ??
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
            
            var split = tags.Split(',');

            foreach (var word in split)
            {
                var trimmed = word.Replace(",", "").Trim();

                var tag = _tagRepository.Find(x => x.Name.ToLower() == trimmed.ToLower()).SingleOrDefault();
                if (tag != null)
                {
                    tag.Posts.Add(post);
                    _tagRepository.Update(tag);
                }
                else
                {
                    tag = new Tag
                    {
                        Name = trimmed,
                        Posts = new List<Post>()
                    };
                    _tagRepository.Create(tag);
                    tag.Posts.Add(post);
                }
            }
            return post;
        }


        public Post GetPostById(int id)
        {
            return _postRepository.Get(id);
        }

        public List<Post> GetLatestPosts(int quantity = 10)
        {
            return _postRepository.GetAll().OrderByDescending(x => x.PublishDate).Take(quantity).ToList();
        }

        public bool DeletePost(int id)
        {
            var post = _postRepository.Get(id);
            if (post == null) return false;

            var tagsToDelete = post.Tags.Where(x => x.Posts.Count == 1).ToList();
            foreach (var tag in tagsToDelete)
                _tagRepository.Delete(tag);

            if (post.Category.Posts.Count <= 1)
                _categoryRepository.Delete(post.Category);

            _postRepository.Delete(post);
            
            return true;
        }

        public List<Post> GetPostsWithTag(string tag)
        {
            return _postRepository.Find(x => x.Tags.Select(o => o.Name.ToLower()).Contains(tag.ToLower())).ToList();
        }


        public Dictionary<string, int> GetTagsWithPostCounts()
        {
            var tags = _tagRepository.GetAll();
            return tags.ToDictionary(tag => tag.Name, tag => tag.Posts.Count);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll().ToList();
        }
    }
}
