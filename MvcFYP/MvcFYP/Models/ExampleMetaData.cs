using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcFYP.Models
{   [MetadataType(typeof(Example.Metadata))]
    public partial class Example
    {
        sealed class Metadata
        {
            public string Name { get; set; }
            public string Question { get; set; }

            [AllowHtml]
            public string Answer { get; set; }
        }
    }
}
