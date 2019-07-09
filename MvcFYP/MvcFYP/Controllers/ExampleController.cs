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
            int userID = (int)Session["userID"];

            ExampleViewModel exampleVM = new ExampleViewModel()
            {
                StudentRecord = new StudentRecord()
            };

            var example = db.Examples.SingleOrDefault(x => x.Id == exampleID);
            exampleVM.Example = example;

            //assign the actual answer value to exercise.ActualCorrectAnswer
            foreach(var exercise in exampleVM.Example.Exercises)
            {
                if (exercise.CorrectAnswer == "Answer1")
                {
                    exercise.ActualCorrectAnswer = exercise.Answer1;
                }
                else if (exercise.CorrectAnswer == "Answer2")
                {
                    exercise.ActualCorrectAnswer = exercise.Answer2;
                }
                else if (exercise.CorrectAnswer == "Answer3")
                {
                    exercise.ActualCorrectAnswer = exercise.Answer3;
                }
                else if (exercise.CorrectAnswer == "Answer4")
                {
                    exercise.ActualCorrectAnswer = exercise.Answer4;
                }

                if(db.StudentRecords.Any(x => x.ExerciseID == exercise.Id))
                {
                    exercise.StudentRecordForThisExercise = db.StudentRecords.SingleOrDefault(x => x.ExerciseID == exercise.Id && x.UserID == userID);
                }
            }


            //foreach(var exercise in exampleVM.Example.Exercises)
            //{
            //    if(db.StudentRecords.Any(y => y.UserID == userID && y.ExerciseID == exercise.Id))
            //    {
            //        exercise.IsDone = true;
            //    }
            //    else
            //    {
            //        exercise.IsDone = false;
            //    }

            //    if(db.StudentRecords.Any(y => y.UserID == userID && y.ExerciseID == exercise.Id && y.Result == "Correct"))
            //    {
            //        exercise.IsCorrect = true;
            //    }
            //    else
            //    {
            //        exercise.IsCorrect = false;
            //    }

            //}

            return View(exampleVM);
        }

        [HttpPost]
        public ActionResult StoreStudentRecord(ExampleViewModel exampleVM)
        {
            bool checkExist = false;
            int userID = (int)Session["userID"];
            int exerciseID = exampleVM.StudentRecord.ExerciseID;
            Exercis currentExercise = db.Exercises.SingleOrDefault(x => x.Id == exerciseID);
            string submittedAnswer = exampleVM.StudentRecord.TempAnswer;
            string correctAnswer = "";

            if (currentExercise.CorrectAnswer == "Answer1")
            {
                correctAnswer = currentExercise.Answer1;
            }
            else if (currentExercise.CorrectAnswer == "Answer2")
            {
                correctAnswer = currentExercise.Answer2;
            }
            else if (currentExercise.CorrectAnswer == "Answer3")
            {
                correctAnswer = currentExercise.Answer3;
            }
            else if (currentExercise.CorrectAnswer == "Answer4")
            {
                correctAnswer = currentExercise.Answer4;
            }


            checkExist = db.StudentRecords.Any(x => x.UserID == userID && x.ExerciseID == exerciseID);
            StudentRecord existingStudentRecord = db.StudentRecords.SingleOrDefault(x => x.UserID == userID && x.ExerciseID == exerciseID);

            if (checkExist && existingStudentRecord.CorrectAttempt == null)
            {
                //if student record exist update the record.

                if (existingStudentRecord.Attempt2 == null)
                {
                    existingStudentRecord.Attempt2 = submittedAnswer;
                    if(submittedAnswer == correctAnswer)
                    {
                        existingStudentRecord.CorrectAttempt = "Attempt2";
                    }
                }
                else if (existingStudentRecord.Attempt3 == null)
                {
                    existingStudentRecord.Attempt3 = submittedAnswer;
                    if (submittedAnswer == correctAnswer)
                    {
                        existingStudentRecord.CorrectAttempt = "Attempt3";
                    }
                }
                else
                {
                    existingStudentRecord.Attempt4 = submittedAnswer;
                    if (submittedAnswer == correctAnswer)
                    {
                        existingStudentRecord.CorrectAttempt = "Attempt4";
                    }
                }

                existingStudentRecord.Date = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(existingStudentRecord).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            else if(!checkExist)
            {
                //if student record not exist then create a new student record

                StudentRecord newStudentRecord = new StudentRecord();
                newStudentRecord.UserID = userID;
                newStudentRecord.ExerciseID = exerciseID;
                newStudentRecord.Attempt1 = submittedAnswer;

                if(submittedAnswer == correctAnswer)
                {
                    newStudentRecord.CorrectAttempt = "Attempt1";
                }

                newStudentRecord.Date = DateTime.Now;

                db.StudentRecords.Add(newStudentRecord);
                db.SaveChanges();
            }

            return RedirectToAction("Show", "Example", new { exampleID = currentExercise.ExampleID });
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

        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var example = db.Examples.Find(id);
            db.Examples.Remove(example);
            db.SaveChanges();

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}