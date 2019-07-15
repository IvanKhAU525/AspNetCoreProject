using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class ActionPlanDb
    {
        public ActionPlanDb()
        {
            Actions = new HashSet<ActionDb>();
        }

        public int ActionPlanId { get; set; }
        public string TextDisplay { get; set; }
        public string LinkToWebsite { get; set; }

        public virtual ICollection<ActionDb> Actions { get; set; }
    }
}
