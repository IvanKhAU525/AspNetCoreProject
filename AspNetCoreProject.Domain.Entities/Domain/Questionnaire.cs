using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  IEnumerable<Question> Questions { get; set; }
    }
}
