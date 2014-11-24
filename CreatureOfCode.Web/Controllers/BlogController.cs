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
            var posts = _postservice.GetPostById(0);
            return View();
        }

        public ActionResult Create()
        {
            var model = new PostModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var p = _postservice.CreatePost(model.Title, model.Content, model.Category, new List<string>());
            _uow.SaveChanges();

            return View(model);
        }

        
    }
}