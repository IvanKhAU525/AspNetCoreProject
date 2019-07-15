using System;
using System.Collections.Generic;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Learn
    {
        public int LearnId { get; set; }
        public string TextDisplay { get; set; }
        
        public IEnumerable<Answer> Answers { get; set; }
    }
}
