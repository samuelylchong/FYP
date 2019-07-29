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
            [Required(ErrorMessage = "*This field is required!")]
            public string Question { get; set; }

            public string Hint { get; set; }

            [Required(ErrorMessage = "*This field is required!")]
            public string Answer1 { get; set; }

            [Required(ErrorMessage = "*This field is required!")]
            public string Answer2 { get; set; }

            [Required(ErrorMessage = "*This field is required!")]
            public string Answer3 { get; set; }

            [Required(ErrorMessage = "*This field is required!")]
            public string Answer4 { get; set; }

            [Display(Name = "Correct Answer")]
            [Required(ErrorMessage = "*Please select an answer!")]
            public string CorrectAnswer { get; set; }
            public int ExampleID { get; set; }
        }

        public bool IsDone { get; set; }
        public bool IsCorrect { get; set; }
        public string ActualCorrectAnswer { get; set; }
        public StudentRecord StudentRecordForThisExercise { get; set; }

    }
}
