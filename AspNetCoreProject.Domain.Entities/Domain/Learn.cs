using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Learn
    {
        public int LearnId { get; set; }
        public string TextDisplay { get; set; }
        
        [JsonIgnore]
        public IEnumerable<Answer> Answers { get; set; }
    }
}
