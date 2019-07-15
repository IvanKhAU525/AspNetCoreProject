using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Action = AspNetCoreProject.Domain.Entities.Domain.Action;

namespace AspNetCoreProject.Application.BusinessLogic.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _myRepository;

        public QuestionnaireService(IQuestionnaireRepository myRepository)
        {
            _myRepository = myRepository;
        }

        public Questionnaire GetQuestionnaire(int questionnaireId) => _myRepository.GetQuestionnaire(questionnaireId);
        public Question GetFirstQuestionOfQuestionnaire(Questionnaire questionnaire) => _myRepository.GetFirstQuestionOfQuestionnaire(questionnaire);
        public Question GetNextQuestion(Answer answer) => _myRepository.GetNextQuestion(answer);

        public Action GetAnswerAction(Answer answer) {
            throw new NotImplementedException();
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
