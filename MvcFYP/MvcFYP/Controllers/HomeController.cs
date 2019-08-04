using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;

namespace MvcFYP.Controllers
{
    public class HomeController : Controller
    {
        FYPEntities db = new FYPEntities();

        public ActionResult Index()
        {
            TopicViewModel topic_example_VM = new TopicViewModel()
            {
                Topic = new Topic()
            };

            topic_example_VM.TopicItems = db.Topics.ToList();

           
            return View(topic_example_VM);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}