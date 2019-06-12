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
            public string SelectionList { get; set; }
            public string FinalAnswer { get; set; }
            public string Hint { get; set; }
            public int ExampleID { get; set; }
        }

        public List<string> SelectionLists { get; set; }

        public Exercis()
        {
            //SelectionLists = new List<string>();
        }
    }
}
