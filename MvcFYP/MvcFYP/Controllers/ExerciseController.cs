using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;

namespace MvcFYP.Controllers
{
    public class ExerciseController : Controller
    {
        FYPEntities db = new FYPEntities();

        // GET: Exercise
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int exampleID)
        {
            ExerciseViewModel exerciseVM = new ExerciseViewModel()
            {
                Exercise = new Exercis()
            };

            exerciseVM.Exercise.ExampleID = exampleID;
            exerciseVM.Exercise.Example = db.Examples.SingleOrDefault(x => x.Id == exampleID);

            return View(exerciseVM);
        }

        [HttpPost]
        public ActionResult Create(ExerciseViewModel exerciseVM)
        {
            db.Exercises.Add(exerciseVM.Exercise);
            db.SaveChanges();

            return RedirectToAction("Show", "Example", new { exampleID = exerciseVM.Exercise.ExampleID });
        }
    }
}