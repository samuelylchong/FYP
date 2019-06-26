using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFYP.Models;

namespace MvcFYP.ViewModels
{
    public class ActivityViewModel
    {
        public StudentRecord StudentRecord { get; set; }
        public List<StudentRecord> StudentRecordItems { get; set; }
        public string Search { get; set; }

        public ActivityViewModel()
        {
            StudentRecordItems = new List<StudentRecord>();
        }
    }
}