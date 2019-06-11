using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;

namespace MvcFYP.Controllers
{
    public class ExampleController : Controller
    {
        FYPEntities db = new FYPEntities();

        // GET: Example
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int topicID)
        {
            ExampleViewModel exampleVM = new ExampleViewModel()
            {
                Example = new Example()
            };

            exampleVM.Example.TopicID = topicID;

            return View(exampleVM);
        }

        [HttpPost]
        public ActionResult Create(ExampleViewModel exampleVM)
        {
            db.Examples.Add(exampleVM.Example);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult show(int exampleID)
        {
            ExampleViewModel exampleVM = new ExampleViewModel();

            var example = db.Examples.SingleOrDefault(x => x.Id == exampleID);
            exampleVM.Example = example;

            foreach(var exercise in exampleVM.Example.Exercises)
            {
                if (exercise.SelectionList != null)
                {
                    exercise.SelectionLists = exercise.SelectionList.Split(',').ToList();
                }
            }

            return View(exampleVM);
        }
    }
}