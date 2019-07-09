using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MvcFYP.Models
{
    [MetadataType(typeof(StudentRecord.Metadata))]
    public partial class StudentRecord
    {
        sealed class Metadata
        {
            [Display(Name = "First Attempt Answer")]
            public string Attempt1 { get; set; }

            [Display(Name = "Second Attempt Answer")]
            public string Attempt2 { get; set; }

            [Display(Name = "Third Attempt Answer")]
            public string Attempt3 { get; set; }

            [Display(Name = "Final Attempt Answer")]
            public string Attempt4 { get; set; }

            [DataType(DataType.Date)]
            public string Date { get; set; }
        }

        public string TempAnswer { get; set; }
        public bool showHint { get; set; }

    }
}
