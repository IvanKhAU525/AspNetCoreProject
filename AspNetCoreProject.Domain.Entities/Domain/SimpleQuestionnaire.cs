namespace AspNetCoreProject.Domain.Entities.Domain
{
    public class SimpleQuestionnaire
    {
        public string Questionnaire { get; set; }
        public string QuestionnaireDescription { get; set; }
        public string Question { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }
        public string Action { get; set; }
        public string Learn { get; set; }
    }
}