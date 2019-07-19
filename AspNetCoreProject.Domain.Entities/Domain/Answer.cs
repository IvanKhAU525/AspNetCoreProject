using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        
        [JsonIgnore]
        public Question Question { get; set; }
        [JsonIgnore]
        public Question NextQuestion { get; set; }
        public Action Action { get; set; }
        public Learn Learn { get; set; }
        
        
    }
}
