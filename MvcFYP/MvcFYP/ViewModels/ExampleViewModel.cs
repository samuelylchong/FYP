using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFYP.Models;

namespace MvcFYP.ViewModels
{
    public class ExampleViewModel
    {
        public Example Example { get; set; }
        public StudentRecord StudentRecord { get; set; }
        public string Feedback { get; set; }
    }
}