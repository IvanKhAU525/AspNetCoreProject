using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Question
    {
        public int? QuestionId { get; set; }
        public int QuestionnaireId { get; set; }
        public int? HintId { get; set; }
        public string TextDisplay { get; set; }
        
        [JsonIgnore]
        public Questionnaire Questionnaire { get; set; }
        [JsonIgnore]
        public IEnumerable<Answer> AnswersNextQuestion { get; set; }
        public Hint Hint { get; set; }
        public IEnumerable<Answer> AnswersQuestion { get; set; }
    }
}
