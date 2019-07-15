using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Action
    {
        public int ActionId { get; set; }
        public int ActionPlanId { get; set; }
        public string TextDisplay { get; set; }
        
        public ActionPlan ActionPlan { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}
