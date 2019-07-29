using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFYP.Models
{
    [MetadataType(typeof(Topic.Metadata))]
    public partial class Topic
    {
        sealed class Metadata
        {
            [Required(ErrorMessage = "*This field is required!")]
            public string Name { get; set; }
        }

    }
}
