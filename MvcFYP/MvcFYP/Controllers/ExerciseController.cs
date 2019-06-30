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

        public ActionResult Edit(int exerciseID)
        {
            ExerciseViewModel exerciseVM = new ExerciseViewModel();
            exerciseVM.Exercise = db.Exercises.SingleOrDefault(x => x.Id == exerciseID);

            return View(exerciseVM);
        }

        [HttpPost]
        public ActionResult Edit(ExerciseViewModel exerciseVM)
        {
            Exercis exercise = new Exercis();
            exercise = db.Exercises.SingleOrDefault(x => x.Id == exerciseVM.Exercise.Id);
            exercise.Question = exerciseVM.Exercise.Question;
            exercise.Hint = exerciseVM.Exercise.Hint;
            exercise.CorrectAnswer = exerciseVM.Exercise.CorrectAnswer;
            exercise.Answer1 = exerciseVM.Exercise.Answer1;
            exercise.Answer2 = exerciseVM.Exercise.Answer2;
            exercise.Answer3 = exerciseVM.Exercise.Answer3;
            exercise.Answer4 = exerciseVM.Exercise.Answer4;

            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Show", "Example", new { exampleID = exerciseVM.Exercise.ExampleID });
        }

        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {        
            var exercise = db.Exercises.Find(id);
            int ExampleID = exercise.Example.Id;
            db.Exercises.Remove(exercise);
            db.SaveChanges();

            return RedirectToAction("Show", "Example", new { exampleID = ExampleID });
        }
    }
}