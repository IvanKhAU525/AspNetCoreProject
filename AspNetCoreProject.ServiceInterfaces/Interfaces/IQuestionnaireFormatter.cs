using System.Collections.Generic;
using System.IO;
using AspNetCoreProject.Domain.Entities.Domain;

namespace AspNetCoreProject.ServiceInterfaces.Interfaces
{
    public interface IQuestionnaireFormatter
    {
        Stream DownloadQuestionnaireJSON(Questionnaire questionnaire);
        Stream DownloadQuestionnaireCSV(IEnumerable<SimpleQuestionnaire> questionnaire);
    }
}