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
            public string Answer { get; set; }
            public string Result { get; set; }
        }

    }
}
