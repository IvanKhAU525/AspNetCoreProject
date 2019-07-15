using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int? NextQuestionId { get; set; }
        public int? ActionId { get; set; }
        public int? LearnId { get; set; }
        public string TextDisplay { get; set; }
        
        public Action Action { get; set; }
        public Learn Learn { get; set; }
        public Question NextQuestion { get; set; }
        public Question Question { get; set; }
    }
}
