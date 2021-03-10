using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizApp.Models
{
    public class Question
    {
        public long ID { get; set; }
        public string QuestionString { get; set; }
        public string Type { get; set; }
        public string Answer { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
     
        public string AddedBy { get; set; }
    }
}