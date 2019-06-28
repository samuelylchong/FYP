using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFYP.Models
{
    [MetadataType(typeof(Exercis.Metadata))]
    public partial class Exercis
    {
        sealed class Metadata
        {
            public string Question { get; set; }
            public string Hint { get; set; }
            public string Answer1 { get; set; }
            public string Answer2 { get; set; }
            public string Answer3 { get; set; }
            public string Answer4 { get; set; }
            [Display(Name = "Correct Answer")]
            public string CorrectAnswer { get; set; }
            public int ExampleID { get; set; }
        }

        public bool IsDone { get; set; }
        public bool IsCorrect { get; set; }

    }
}
