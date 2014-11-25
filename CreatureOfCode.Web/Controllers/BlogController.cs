using CreatureOfCode.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreaturesOfCode.Data;
using CreaturesOfCode.Services;

namespace CreatureOfCode.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postservice;
        private readonly IUnitOfWork _uow;

        public BlogController(IPostService postservice, IUnitOfWork uow)
        {
            _postservice = postservice;
            _uow = uow;
        }

        public ActionResult Index()
        {
            var posts = _postservice.GetLatestPosts();
            var previews = new List<PostPreviewModel>();

            foreach (var post in posts)
            {
                previews.Add(new PostPreviewModel
                {
                    PostId = post.Id,
                    PublishDate = post.PublishDate,
                    Title = post.Title,
                    ContentSnippet = post.Content.Substring(0, post.Content.IndexOf('.') + 1),
                    CategoryName = post.Category.Name
                });
            }
            return View(previews);

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


    }
}