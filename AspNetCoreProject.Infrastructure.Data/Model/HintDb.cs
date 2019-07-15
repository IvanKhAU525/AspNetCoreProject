using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class HintDb
    {
        public HintDb() {
            Questions = new HashSet<QuestionDb>();
        }
        public int HintId { get; set; }
        public string TextDisplay { get; set; }
        
        public virtual ICollection<QuestionDb> Questions { get; set; }
    }
}
