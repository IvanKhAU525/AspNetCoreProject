using AgileObjects.AgileMapper;
using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreProject.Infrastructure.Data.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly InquirerContext _inquirerContext;
        public QuestionnaireRepository() {
            _inquirerContext = new InquirerContext();
        }
        
        public Questionnaire GetQuestionnaire(int questionnaireId) {
            var questionnaire =
                _inquirerContext.Questionnaires
                    .Include(qr => qr.Questions)
                    .ThenInclude(q => q.AnswersQuestion)
                    .FirstOrDefault(qr => qr.QuestionnaireId == questionnaireId);
            return Mapper.Map(questionnaire).ToANew<Questionnaire>();
        }
        public Question GetFirstQuestionOfQuestionnaire(Questionnaire questionnaire) {
            var question =
                _inquirerContext.Questionnaires
                .Include(qr => qr.Questions)
                .FirstOrDefault(qr => qr.QuestionnaireId == questionnaire.QuestionnaireId)
                ?.Questions
                .First();
            return Mapper.Map(question).ToANew<Question>();
        }

        public Question GetNextQuestion(Answer answer) {
            var question =
                _inquirerContext.Answers
                    .Include(a => a.Question)
                    .Include(a => a.NextQuestion)
                        .ThenInclude(q => q.AnswersQuestion)
                            .ThenInclude(a => a.Learn)
                    .Include(a => a.Learn)
                    .Include(a => a.Action)
                    .FirstOrDefault(a => a.AnswerId == answer.AnswerId)
                    ?.NextQuestion;
            return Mapper.Map(question).ToANew<Question>();
        }
    }
}
