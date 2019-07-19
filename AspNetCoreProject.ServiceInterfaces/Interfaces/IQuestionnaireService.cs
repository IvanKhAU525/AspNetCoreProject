using AspNetCoreProject.Domain.Entities.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Action = AspNetCoreProject.Domain.Entities.Domain.Action;

namespace AspNetCoreProject.ServiceInterfaces.Interfaces
{
    public interface IQuestionnaireService
    {
        int OpenSession();
        Questionnaire GetQuestionnaire(int questionnaireId);
        Question GetNextQuestion(int questionId, int answerId, int sessionId);
        Question GetFirstQuestionOfQuestionnaire(int questionnaireId);
        Action GetAnswerAction(int answerId);
        Stream DownloadQuestionnaireJSON(int questionnaireId);
        //Stream DownloadQuestionnaireCSV(int questionnaireId);
        Stream DownloadQuestionnaireCSV(int questionnaireId);
        Learn GetAnswerLearn(Answer answer);
        IList<Answer> GetAnswersForQuestion(Question question);
        Hint GetQuestionHint(Question question);
        ActionPlan GetActionPlanForAction(Domain.Entities.Domain.Action action);
        Interaction GetInteraction(Session session);
    }
}
