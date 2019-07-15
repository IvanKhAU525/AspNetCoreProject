using AspNetCoreProject.Domain.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Action = AspNetCoreProject.Domain.Entities.Domain.Action;

namespace AspNetCoreProject.ServiceInterfaces.Interfaces
{
    public interface IQuestionnaireService
    {
        Questionnaire GetQuestionnaire(int questionnaireId);
        Question GetNextQuestion(Answer answer);
        Question GetFirstQuestionOfQuestionnaire(Questionnaire questionnaire);
        Action GetAnswerAction(Answer answer);
        Learn GetAnswerLearn(Answer answer);
        IList<Answer> GetAnswersForQuestion(Question question);
        Hint GetQuestionHint(Question question);
        ActionPlan GetActionPlanForAction(Domain.Entities.Domain.Action action);
        Interaction GetInteraction(Session session);
    }
}
