using AgileObjects.AgileMapper;
using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreProject.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Action = AspNetCoreProject.Domain.Entities.Domain.Action;

namespace AspNetCoreProject.Infrastructure.Data.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly InquirerContext _inquirerContext;
        private readonly ILogger _logger;
        public QuestionnaireRepository(InquirerContext inquirerContext, ILogger logger) {
            _inquirerContext = inquirerContext;
            _logger = logger;
        }

        public int OpenSession() {
            var lastSessionId = _inquirerContext.Sessions.LastOrDefault()?.SessionId ?? default;
            var newSessionId = lastSessionId + 1;
            AddSession(newSessionId);
            return newSessionId;
        }

        // TODO: trnasform to async model 
        private async Task AddSession(int sessionId) {
             _inquirerContext.Sessions.Add(new SessionDb() {
                Created = DateTime.Now
            });
             _inquirerContext.SaveChanges();
        }

        public Questionnaire GetQuestionnaire(int questionnaireId) {
            var questionnaire = _inquirerContext.Questionnaires
                .Where(x => x.QuestionnaireId == questionnaireId)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.AnswersQuestion)
                        .ThenInclude(x => x.Action)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.AnswersQuestion)
                        .ThenInclude(x => x.Learn)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Hint)
                .FirstOrDefault();
            return Mapper.Map(questionnaire).ToANew<Questionnaire>();
        }
        
        public Questionnaire GetQuestionnaire(int questionnaireId, int count) {
            
            var questionnaire =
                _inquirerContext.Questionnaires
                    .FirstOrDefault(qr => qr.QuestionnaireId == questionnaireId);
            _inquirerContext.Questions
                .Where(q => q.QuestionnaireId == questionnaireId)
                .Take(count)
                .Load();
            return Mapper.Map(questionnaire).ToANew<Questionnaire>();
        }
        
        public Question GetFirstQuestionOfQuestionnaire(int questionnaireId) {
            var question =
                _inquirerContext.Questions
                    .Include(q => q.Hint)
                    .Include(q => q.AnswersQuestion)
                    .FirstOrDefault(q => q.QuestionnaireId == questionnaireId);
            return Mapper.Map(question).ToANew<Question>();
        }

        public Question GetNextQuestion(int answerId) {
            var question =
                _inquirerContext.Answers
                    .Include(x => x.NextQuestion)
                    .First(x => x.AnswerId == answerId)
                    .NextQuestion;
            return Mapper.Map(question).ToANew<Question>();
        }

        public Action GetAnswerAction(int answerId) {
            var action =
                _inquirerContext.Answers
                    .FirstOrDefault(a => a.AnswerId == answerId);
            return Mapper.Map(action).ToANew<Action>();
        }

        public Learn GetAnswerLearn(int answerId) {
            var learn =
                _inquirerContext.Answers
                    .Include(a => a.Learn)
                    .FirstOrDefault(a => a.AnswerId == answerId);
            return Mapper.Map(learn).ToANew<Learn>();

        }

        public IEnumerable<SimpleQuestionnaire> JoinQuestionnaireTables(int questionnaireId) {
            /*
             *  This code produces getting questionnaire entity with linked entities to it.
             *  It's equivalent to the next SQL code:
             
                SELECT `qr`.`Name` AS `Questionnaire`, `q`.`TextDisplay` AS `Question`, `h`.`TextDisplay` AS `Hint`, `a`.`TextDisplay` AS `Answer`, `ac`.`TextDisplay` AS `Action`, `l`.`TextDisplay` AS `Learn`
                FROM `inquirer`.`questionnaires` AS `qr`
                INNER JOIN `inquirer`.`questions` AS `q` ON `qr`.`QuestionnaireId` = `q`.`QuestionnaireId`
                LEFT JOIN `inquirer`.`hints` AS `h` ON `q`.`HintId` = `h`.`HintId`
                INNER JOIN `inquirer`.`answers` AS `a` ON `q`.`QuestionId` = `a`.`QuestionId`
                LEFT JOIN `inquirer`.`actions` AS `ac` ON `a`.`ActionId` = `ac`.`ActionId`
                LEFT JOIN `inquirer`.`learns` AS `l` ON `a`.`LearnId` = `l`.`LearnId`
                WHERE `qr`.`QuestionnaireId` = <<questionnaireId>>
                
             */
            var joinQuestionnaireQuestions = 
                from qr in _inquirerContext.Questionnaires join q in _inquirerContext.Questions on qr.QuestionnaireId equals q.QuestionnaireId 
                where qr.QuestionnaireId == questionnaireId 
                select new {
                    Questionnaire = qr.Name,
                    QuestionnaireDescription = qr.Description,
                    Question = q.TextDisplay,
		
                    HintId = q.HintId,
                    QuestionId = q.QuestionId
                };
            var joinQuestionsHints =
                from q in joinQuestionnaireQuestions join h in _inquirerContext.Hints on q.HintId equals h.HintId into temp
                from t in temp.DefaultIfEmpty()
                select new {
                    Questionnaire = q.Questionnaire,
                    QuestionnaireDescription = q.QuestionnaireDescription,
                    Question = q.Question,
                    Hint = t.TextDisplay,
		
                    QuestionId = q.QuestionId
                };
            var joinQuestionsAnswers =
                from q in joinQuestionsHints join a in _inquirerContext.Answers on q.QuestionId equals a.QuestionId 
                select new {
                    Questionnaire = q.Questionnaire,
                    QuestionnaireDescription = q.QuestionnaireDescription,
                    Question = q.Question,
                    Hint = q.Hint,
                    Answer = a.TextDisplay,
		
                    ActionId = a.ActionId,
                    LearnId = a.LearnId
                };
            var joinAnswersActions = 
                from an in joinQuestionsAnswers join ac in _inquirerContext.Actions on an.ActionId equals ac.ActionId into temp
                from t in temp.DefaultIfEmpty()
                select new {
                    Questionnaire = an.Questionnaire,
                    QuestionnaireDescription = an.QuestionnaireDescription,
                    Question = an.Question,
                    Hint = an.Hint,
                    Answer = an.Answer,
                    Action = t.TextDisplay,
		
                    LearnId = an.LearnId
                };
            var joinAnswersLearns =
                from a in joinAnswersActions join l in _inquirerContext.Learns on a.LearnId equals l.LearnId into temp
                from t in temp.DefaultIfEmpty()
                select new {
                    Questionnaire = a.Questionnaire,
                    QuestionnaireDescription = a.QuestionnaireDescription,
                    Question = a.Question,
                    Hint = a.Hint,
                    Answer = a.Answer,
                    Action = a.Action,
                    Learn = t.TextDisplay
                };
            
            var joinedTables = joinAnswersLearns.ToList();
            return Mapper.Map(joinedTables).ToANew<List<SimpleQuestionnaire>>();
        }

        public void CheckSessionExist(int sessionId) {
            var sessionExist = _inquirerContext.Sessions
                .Include(x => x.Interactions)
                .FirstOrDefault(x => x.SessionId == sessionId);
            if (sessionExist is null) 
                throw new Exception("Invalid user.");
        }

        public bool ValidateUserAndCheckAnyInteractions(int sessionId) {
            var sessionExist = _inquirerContext.Sessions
                .Include(x => x.Interactions)
                .FirstOrDefault(x => x.SessionId == sessionId);
            if (sessionExist is null)
                throw new Exception("Invalid user.");
            return sessionExist.Interactions.Any();
        }

        public bool ValidateAnswer(int questionId, int answerId) {
            var isValid = _inquirerContext.Answers.FirstOrDefault(x => x.AnswerId == answerId)?.QuestionId;
            return !(isValid is null) && isValid == questionId;
        }

        public bool CheckFirstQuestionOfQuestionnaire(int questionId, int answerId) {
            var isValidAnswer = ValidateAnswer(questionId, answerId);
            if (!isValidAnswer)
                throw new Exception("Invalid answer.");
            var isFirstQuestionOfQuestionnaire = FindAndCheckFirstQuestionOfQuestionnaire(questionId);
            return isFirstQuestionOfQuestionnaire;
        }

        public bool CheckIsCorrectSequenceOfQuestions(int questionId, int answerId, int sessionId) {
            var lastInteraction = _inquirerContext.Interactions.Last(x => x.SessionId == sessionId);
            var lastAnswerId = lastInteraction.AnswerId;
            var nextQuestion = _inquirerContext.Answers.First(x => x.AnswerId == lastAnswerId);
            var nextQuestionId = nextQuestion.NextQuestionId;
            return nextQuestionId == questionId;
        }

        private bool FindAndCheckFirstQuestionOfQuestionnaire(int questionId) {
            var questionnaireId = _inquirerContext.Questions
                .First(x => x.QuestionId == questionId).QuestionnaireId;
            var firstQuestionId = _inquirerContext.Questions
                .First(x => x.QuestionnaireId == questionnaireId).QuestionId;
            return questionId == firstQuestionId;
        }

        public void SubmitAnswer(int questionId, int answerId, int sessionId) {
            _inquirerContext.Interactions.Add(new InteractionDb() {
                AnswerId = answerId,
                QuestionId = questionId,
                SessionId = sessionId
            });
            _inquirerContext.SaveChanges();
        }
    }
}