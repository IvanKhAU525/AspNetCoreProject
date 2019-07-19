using AspNetCoreProject.Domain.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AspNetCoreProject.Domain.Interfaces.Repositories
{
    public interface IQuestionnaireRepository
    {
        int OpenSession();
        Questionnaire GetQuestionnaire(int questionnaireId);
        Question GetFirstQuestionOfQuestionnaire(int questionnaireId);
        Question GetNextQuestion(int nextQuestionId);
        Domain.Entities.Domain.Action GetAnswerAction(int answerId);
        IEnumerable<SimpleQuestionnaire> JoinQuestionnaireTables(int questionnaireId);
        void SubmitAnswer(int questionId, int answerId, int sessionId);
        bool ValidateUserAndCheckAnyInteractions(int sessionId);
        bool ValidateAnswer(int questionId, int answerId);
        bool CheckFirstQuestionOfQuestionnaire(int questionId, int answerId);
        bool CheckIsCorrectSequenceOfQuestions(int questionId, int answerId, int sessionId);
    }
}
