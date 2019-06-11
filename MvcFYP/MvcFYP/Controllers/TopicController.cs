using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;

namespace MvcFYP.Controllers
{
    public class TopicController : Controller
    {
        FYPEntities db = new FYPEntities();

        // GET: Topic
        public ActionResult Index()
        {
            return View();
        }

        //not using. create topic already moved to home/index
        public ActionResult Create()
        {
            TopicViewModel topicVM = new TopicViewModel()
            {
                Topic = new Topic()
            };

            return View(topicVM);
        }

        [HttpPost]
        public ActionResult Create(TopicViewModel topicVM)
        {
            db.Topics.Add(topicVM.Topic);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult test()
        {
            return View();
        }
    }
}