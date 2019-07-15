using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class InteractionDb
    {
        public int InteractionId { get; set; }
        public int SessionId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionTextDisplay { get; set; }
        public int AnswerId { get; set; }
        public string AnswerTextDisplay { get; set; }

        public virtual SessionDb Session { get; set; }
    }
}
