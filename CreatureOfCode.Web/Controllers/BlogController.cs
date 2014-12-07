using System.Web.Routing;
using CreatureOfCode.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreaturesOfCode.Core;
using CreaturesOfCode.Data;
using CreaturesOfCode.Services;

namespace CreatureOfCode.Web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IPostService _postservice;
        private readonly IUnitOfWork _uow;

        public BlogController(IPostService postservice, IUnitOfWork uow)
        {
            _postservice = postservice;
            _uow = uow;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var posts = _postservice.GetLatestPosts(20);
            var previews = GeneratePostPreviews(posts);

            return View(previews);

        }

        [AllowAnonymous]
        public ActionResult FindByTag(string tag)
        {
            var posts = _postservice.GetPostsWithTag(tag);
            var previews = GeneratePostPreviews(posts);

            ViewBag.Tag = tag;

            return View("Index", previews);
        }

        [AllowAnonymous]
        public ActionResult Read(int id)
        {
            var post = _postservice.GetPostById(id);

            if (post == null) return HttpNotFound();

            var tags = _postservice.GetTagsWithPostCounts();
            var categories = _postservice.GetAllCategories();

            var model = new ReadPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Category = post.Category.Name,
                Content = post.Content,
                PublishDate = post.PublishDate,
                Tags = new List<TagModel>(post.Tags.Select(x => new TagModel
                {
                    Name = x.Name,
                    PostIds = new List<int>(x.Posts.Select(o => o.Id).ToList())
                })),
                AllTags = new Dictionary<string, int>(),
                AllCategories = categories.ToDictionary(x => x.Name, x => x.Posts.Count)
            };

            foreach (var kvp in tags)
            {
                if (kvp.Value <= 1)
                    model.AllTags.Add(kvp.Key, 10);
                else if (kvp.Value > 1 && kvp.Value <= 5)
                    model.AllTags.Add(kvp.Key, 12);
                else if (kvp.Value > 5 && kvp.Value <= 15)
                    model.AllTags.Add(kvp.Key, 14);
                else if (kvp.Value > 15 && kvp.Value <= 25)
                    model.AllTags.Add(kvp.Key, 17);
                else if (kvp.Value > 25 && kvp.Value <= 35)
                    model.AllTags.Add(kvp.Key, 19);
                else if (kvp.Value > 35)
                    model.AllTags.Add(kvp.Key, 21);
            }

            return View(model);
        }

        public ActionResult Create()
        {
            var model = new PostModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _postservice.CreatePost(model.Title, model.Content, model.Category, model.Tags);
            _uow.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = _postservice.GetPostById(id);

            if (post == null) return new HttpNotFoundResult();

            var model = new PostModel
            {
                Category = post.Category.Name,
                Content = post.Content,
                Title = post.Title,
                Tags = string.Join(",", post.Tags.Select(x => x.Name)),
                Id = post.Id
            };

            ViewBag.IsEditing = true;

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostModel model)
        {
            if (!ModelState.IsValid)
                return View("Create", model);

            var post = _postservice.EditPost(model.Id, model.Title, model.Content, model.Category, model.Tags);
            if (post == null)
                return new HttpStatusCodeResult(500, "An internal error occured while trying to update the post.");

            _uow.SaveChanges();
            return RedirectToAction("Read", new {id = post.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (_postservice.DeletePost(id))
            {
                _uow.SaveChanges();
                return RedirectToAction("Index");
            }
            return new HttpNotFoundResult();
        }


        private List<PostPreviewModel> GeneratePostPreviews(IEnumerable<Post> posts)
        {
            return posts.Select(post => new PostPreviewModel
            {
                PostId = post.Id, 
                PublishDate = post.PublishDate, 
                Title = post.Title, 
                ContentSnippet = post.Content.Substring(0, post.Content.IndexOf('.') + 1), 
                CategoryName = post.Category.Name
            }).ToList();
        }
    }
}