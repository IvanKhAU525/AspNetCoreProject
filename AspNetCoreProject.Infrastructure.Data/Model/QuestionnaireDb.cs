using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class QuestionnaireDb
    {
        public QuestionnaireDb()
        {
            Questions = new HashSet<QuestionDb>();
        }

        public int QuestionnaireId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<QuestionDb> Questions { get; set; }
    }
}
