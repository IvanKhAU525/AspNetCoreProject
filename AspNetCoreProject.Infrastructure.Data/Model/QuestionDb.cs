using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class QuestionDb
    {
        public QuestionDb()
        {
            AnswersNextQuestion = new HashSet<AnswerDb>();
            AnswersQuestion = new HashSet<AnswerDb>();
        }

        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public int? HintId { get; set; }
        public string TextDisplay { get; set; }

        public virtual HintDb Hint { get; set; }
        public virtual QuestionnaireDb Questionnaire { get; set; }
        public virtual ICollection<AnswerDb> AnswersNextQuestion { get; set; }
        public virtual ICollection<AnswerDb> AnswersQuestion { get; set; }
    }
}
