using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Submittion
    {
        public long ID { get; set; }
        public string SubmittedAnswer { get; set; }
        public string Result { get; set; }
        public string SubmittedBy { get; set; }
        public DateTime SubmittedOn { get; set; }
        
    }
}