using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Session
    {
        public int SessionId { get; set; }
        public DateTime? Created { get; set; }
    }
}
