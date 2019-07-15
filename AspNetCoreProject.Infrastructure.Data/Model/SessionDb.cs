using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Infrastructure.Data.Model
{
    public partial class SessionDb
    {
        public SessionDb()
        {
            Interactions = new HashSet<InteractionDb>();
        }

        public int SessionId { get; set; }
        public DateTime? Created { get; set; }

        public virtual ICollection<InteractionDb> Interactions { get; set; }
    }
}
