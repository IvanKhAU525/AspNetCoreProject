using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public int? HintId { get; set; }
        public string TextDisplay { get; set; }
        
        public Hint Hint { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public IEnumerable<Answer> AnswersNextQuestion { get; set; }
        public IEnumerable<Answer> AnswersQuestion { get; set; }
    }
}
