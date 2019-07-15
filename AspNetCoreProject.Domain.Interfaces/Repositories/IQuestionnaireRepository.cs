using AspNetCoreProject.Domain.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreProject.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireRepository
    {
        Questionnaire GetQuestionnaire(int questionnaireId);
        Question GetFirstQuestionOfQuestionnaire(Questionnaire questionnaire);
        Question GetNextQuestion(Answer answer);
    }
}
