using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCoreProject.Domain.Entities.Domain
{
    public partial class Action
    {
        public int ActionId { get; set; }
        public int ActionPlanId { get; set; }
        public string TextDisplay { get; set; }
        
        [JsonIgnore]
        public ActionPlan ActionPlan { get; set; }
        [JsonIgnore]
        public IEnumerable<Answer> Answers { get; set; }
    }
}
