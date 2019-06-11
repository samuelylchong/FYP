using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcFYP.Models;

namespace MvcFYP.ViewModels
{
    public class TopicViewModel
    {
        public Topic Topic { get; set; }

        public List<Topic> TopicItems { get; set; }
        //public List<Example> ExampleItems { get; set; }

        public TopicViewModel()
        {
            TopicItems = new List<Topic>();
            //ExampleItems = new List<Example>();
        }
    }
}