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

        public ActionResult Show(int exampleID)
        {
            ExampleViewModel exampleVM = new ExampleViewModel()
            {
                StudentRecord = new StudentRecord()
            };

            var example = db.Examples.SingleOrDefault(x => x.Id == exampleID);
            exampleVM.Example = example;

            int userID = (int)Session["userID"];

            foreach(var exercise in exampleVM.Example.Exercises)
            {
                if (exercise.SelectionList != null)
                {
                    exercise.SelectionLists = exercise.SelectionList.Split(',').ToList();
                }

                if(db.StudentRecords.Any(y => y.UserID == userID && y.ExerciseID == exercise.Id))
                {
                    exercise.IsDone = true;
                }
                else
                {
                    exercise.IsDone = false;
                }

                if(db.StudentRecords.Any(y => y.UserID == userID && y.ExerciseID == exercise.Id && y.Result == "Correct"))
                {
                    exercise.IsCorrect = true;
                }
                else
                {
                    exercise.IsCorrect = false;
                }

            }

            return View(exampleVM);
        }

        [HttpPost]
        public ActionResult StoreStudentRecord(ExampleViewModel exampleVM)
        {
            exampleVM.StudentRecord.UserID = (int)Session["userID"];
            var exercise = db.Exercises.SingleOrDefault(y => y.Id == exampleVM.StudentRecord.ExerciseID);
            Exercis ex = exercise;

            if (exampleVM.StudentRecord.Answer == ex.FinalAnswer)
            {
                exampleVM.StudentRecord.Result = "Correct";
            }
            else
            {
                exampleVM.StudentRecord.Result = "Wrong";
            }

            db.StudentRecords.Add(exampleVM.StudentRecord);
            db.SaveChanges();


            return RedirectToAction("Show", "Example", new { exampleID = ex.ExampleID });
        }
    }
}