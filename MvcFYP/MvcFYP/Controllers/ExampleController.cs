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
            bool checkExist = false;
            bool checkExist2 = false;
            string checkAnswer = "";

            exampleVM.StudentRecord.UserID = (int)Session["userID"];
            var exercise = db.Exercises.SingleOrDefault(y => y.Id == exampleVM.StudentRecord.ExerciseID);
            Exercis ex = exercise;

            if(ex.CorrectAnswer == "Answer1")
            {
                checkAnswer = ex.Answer1;
            } else if(ex.CorrectAnswer == "Answer2")
            {
                checkAnswer = ex.Answer2;
            }
            else if (ex.CorrectAnswer == "Answer3")
            {
                checkAnswer = ex.Answer3;
            }
            else if (ex.CorrectAnswer == "Answer4")
            {
                checkAnswer = ex.Answer4;
            }

            if (exampleVM.StudentRecord.Answer == checkAnswer)
            {
                exampleVM.StudentRecord.Result = "Correct";
            }
            else
            {
                exampleVM.StudentRecord.Result = "Wrong";
            }

            exampleVM.StudentRecord.Date = DateTime.Now;

            //check if record exists
            checkExist = db.StudentRecords.Any(x => x.UserID == exampleVM.StudentRecord.UserID && x.ExerciseID == exampleVM.StudentRecord.ExerciseID && x.Answer == exampleVM.StudentRecord.Answer && x.Result == exampleVM.StudentRecord.Result);

            checkExist2 = db.StudentRecords.Any(x => x.UserID == exampleVM.StudentRecord.UserID && x.ExerciseID == exampleVM.StudentRecord.ExerciseID && x.Result == "Correct");

            if (checkExist == false && checkExist2 == false)
            {
                db.StudentRecords.Add(exampleVM.StudentRecord);
                db.SaveChanges();
            }

            return RedirectToAction("Show", "Example", new { exampleID = ex.ExampleID });
        }

        public ActionResult Edit(int exampleID)
        {
            ExampleViewModel exampleVM = new ExampleViewModel();
            exampleVM.Example = db.Examples.Single(x => x.Id == exampleID);

            return View(exampleVM);
        }

        [HttpPost]
        public ActionResult Edit(ExampleViewModel exampleVM)
        {
            Example example = db.Examples.Single(x => x.Id == exampleVM.Example.Id);
            example.Name = exampleVM.Example.Name;
            example.Question = exampleVM.Example.Question;
            example.Answer = exampleVM.Example.Answer;
            
            if(ModelState.IsValid)
            {
                db.Entry(example).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Show", "Example", new { exampleID = example.Id});
        }
    }
}