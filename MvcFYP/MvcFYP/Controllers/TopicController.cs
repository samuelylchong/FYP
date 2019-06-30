using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;
using System.Data.Entity;
using System.Reflection;

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

        public ActionResult Edit(int topicID)
        {
            Topic topic = db.Topics.Find(topicID);
            TopicViewModel topicVM = new TopicViewModel();
            topicVM.Topic = topic;

            return View(topicVM);
        }

        [HttpPost]
        public ActionResult Edit(TopicViewModel topicVM)
        {
            Topic topic = db.Topics.Find(topicVM.Topic.Id);
            topic.Name = topicVM.Topic.Name;

            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult test()
        {
            return View();
        }
    }
}