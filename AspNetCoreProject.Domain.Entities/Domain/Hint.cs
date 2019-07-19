using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Hint
    {
        public int HintId { get; set; }
        public string TextDisplay { get; set; }
        
        [JsonIgnore]
        public IEnumerable<Question> Questions { get; set; }
    }
}
