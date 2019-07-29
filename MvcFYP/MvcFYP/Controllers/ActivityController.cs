using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using MvcFYP.ViewModels;


namespace MvcFYP.Controllers
{
    public class ActivityController : Controller
    {
        FYPEntities db = new FYPEntities();
        //int? ExampleID;

        // GET: Activity
        public ActionResult Index()
        {
            Session["ExampleID"] = null;
            ActivityViewModel activityVM = new ActivityViewModel();

            activityVM.StudentRecordItems = db.StudentRecords.ToList();

            return View(activityVM);
        }

        public ActionResult GetRelatedActivity(int exampleID)
        {
            Session["ExampleID"] = exampleID;

            var example = db.Examples.Find(exampleID);
            ActivityViewModel activityVM = new ActivityViewModel();
            
            foreach(var exercise in example.Exercises)
            {
                var studentRecords = db.StudentRecords.Where(x => x.ExerciseID == exercise.Id).ToList();

                foreach(var studentRecord in studentRecords)
                {
                    activityVM.StudentRecordItems.Add(studentRecord);
                }

            }

            return View("Index", activityVM);
        }

        [HttpPost]
        public ActionResult Search(ActivityViewModel activityVM)
        {
            ActivityViewModel ActivityVM = new ActivityViewModel();

            if (Session["ExampleID"] != null)
            {
                int ExampleID = (int)Session["ExampleID"];

                if (activityVM.Search == null)
                {
                    ActivityVM.StudentRecordItems = db.StudentRecords.Where(x => x.Exercis.ExampleID == ExampleID).ToList();
                }
                else
                {
                    ActivityVM.StudentRecordItems = db.StudentRecords.Where(x => x.User.Username == activityVM.Search && x.Exercis.ExampleID == ExampleID).ToList();
                }

            }
            else
            {
                // ExampleID is null

                if (activityVM.Search == null)
                {
                    ActivityVM.StudentRecordItems = db.StudentRecords.ToList();
                }
                else
                {
                    ActivityVM.StudentRecordItems = db.StudentRecords.Where(x => x.User.Username == activityVM.Search).ToList();
                }
            }

            return View("Index", ActivityVM);
        }
    }
}