using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Action = AspNetCoreProject.Domain.Entities.Domain.Action;

namespace AspNetCoreProject.Application.BusinessLogic.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _myRepository;
        private ILogger _logger;

        public QuestionnaireService(IQuestionnaireRepository myRepository, ILogger logger)
        {
            _myRepository = myRepository;
            _logger = logger;
        }

        public int OpenSession() => _myRepository.OpenSession();

        public Questionnaire GetQuestionnaire(int questionnaireId) => _myRepository.GetQuestionnaire(questionnaireId);

        public Question GetFirstQuestionOfQuestionnaire(int questionnaireId) { 
            return _myRepository.GetFirstQuestionOfQuestionnaire(questionnaireId);
        }

        public Question GetNextQuestion(int questionId, int answerId, int sessionId) {
            var hasInteractions = _myRepository.ValidateUserAndCheckAnyInteractions(sessionId);
            if (!hasInteractions) {
                var isFirstQuestionOfQuestionnaire = _myRepository.CheckFirstQuestionOfQuestionnaire(questionId, answerId);
                if (!isFirstQuestionOfQuestionnaire) 
                    throw new Exception("Answer is not to the first question.");    
            }
            else {
                var isCorrectSequenceOfQuestions = _myRepository.CheckIsCorrectSequenceOfQuestions(questionId, answerId, sessionId);
                if (!isCorrectSequenceOfQuestions) 
                    throw new Exception("Wrong sequence of questions.");
            }
            _myRepository.SubmitAnswer(questionId, answerId, sessionId);
            var nextQuestion = _myRepository.GetNextQuestion(answerId);
            return nextQuestion;
        }
        public Action GetAnswerAction(int answerId) => _myRepository.GetAnswerAction(answerId);
        public Stream DownloadQuestionnaireJSON(int questionnaireId) {
            var questionnaire = _myRepository.GetQuestionnaire(questionnaireId);
            var questionnaireFormat = new QuestionnaireFormatter(_logger);
            var stream = questionnaireFormat.DownloadQuestionnaireJSON(questionnaire);
            return stream;
        }

        public Stream DownloadQuestionnaireCSV(int questionnaireId) {
            var questionnaire = _myRepository.JoinQuestionnaireTables(questionnaireId);
            var questionnaireFormat = new QuestionnaireFormatter(_logger);
            var stream = questionnaireFormat.DownloadQuestionnaireCSV(questionnaire);
            return stream;
        }

        public Learn GetAnswerLearn(Answer answer) {
            throw new NotImplementedException();
        }

        public IList<Answer> GetAnswersForQuestion(Question question) {
            throw new NotImplementedException();
        }

        public Hint GetQuestionHint(Question question) {
            throw new NotImplementedException();
        }

        public ActionPlan GetActionPlanForAction(Action action) {
            throw new NotImplementedException();
        }

        public Interaction GetInteraction(Session session) {
            throw new NotImplementedException();
        }
    }
}
