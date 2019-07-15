using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class LearnDb
    {
        public LearnDb() {
            Answers = new HashSet<AnswerDb>();
        }
        public int LearnId { get; set; }
        public string TextDisplay { get; set; }
        
        public virtual ICollection<AnswerDb> Answers { get; set; }
    }
}
