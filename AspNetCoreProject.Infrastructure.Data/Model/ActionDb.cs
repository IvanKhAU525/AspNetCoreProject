using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class ActionDb
    {
        public ActionDb() {
            Answers = new HashSet<AnswerDb>();
        }
        public int ActionId { get; set; }
        public int ActionPlanId { get; set; }
        public string TextDisplay { get; set; }

        public virtual ActionPlanDb ActionPlan { get; set; }
        public virtual ICollection<AnswerDb> Answers { get; set; }
    }
}
