using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class ActionPlan
    {
        public int ActionPlanId { get; set; }
        public string TextDisplay { get; set; }
        public string LinkToWebsite { get; set; }
        
        public IEnumerable<Action> Actions { get; set; }
    }
}
