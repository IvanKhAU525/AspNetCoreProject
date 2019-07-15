using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class AnswerDb
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int? NextQuestionId { get; set; }
        public int? ActionId { get; set; }
        public int? LearnId { get; set; }
        public string TextDisplay { get; set; }

        public virtual ActionDb Action { get; set; }
        public virtual LearnDb Learn { get; set; }
        public virtual QuestionDb NextQuestion { get; set; }
        public virtual QuestionDb Question { get; set; }
    }
}
