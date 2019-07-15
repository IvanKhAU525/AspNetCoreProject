using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Hint
    {
        public int HintId { get; set; }
        public string TextDisplay { get; set; }
        
        public IEnumerable<Question> Questions { get; set; }
    }
}
