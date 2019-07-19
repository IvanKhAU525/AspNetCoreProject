using AspNetCoreProject.API.Controllers;
using AspNetCoreProject.Application.BusinessLogic.Services;
using AspNetCoreProject.Infrastructure.Data.Repositories;
using System;
using System.Linq;
using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.Domain.Interfaces.Repositories;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using Xunit;

namespace AspNetCoreProject.Tests.ModuleTest.WebApiTests
{
    public class QuestionnaireControllerTests
    {
        private readonly IQuestionnaireRepository repository;
        private readonly IQuestionnaireService service;
        private readonly QuestionnaireController controller;

        public QuestionnaireControllerTests() {
            #warning hard-coded behaviours of entities in the database
            repository = new QuestionnaireRepository(null);
            service = new QuestionnaireService(repository);
            controller = new QuestionnaireController(service);
        }
        
//        [Fact]
//        public void GetFirstQuestionOfQuestionnaires_Question() {
//            //  Act
//            var questionnaire = controller.GetQuestionnaire();
//            var question = controller.GetFirstQuestionOfQuestionnaire(questionnaire.Value).Value;
//            //  Assert
//            Assert.Equal("HOW DO I SAVE FOR RETIREMENT?", question.TextDisplay);
//        }

        [Fact]
        void GetNextQuestion_NextQuestion() {
            //    Arrange
            var answer = new Answer() {
                AnswerId = 10,
                NextQuestionId = 6
            };
            //    Act
            var question = controller.NextQuestion(answer).Value;
            
            //    Assert
            Assert.NotNull(question);
            Assert.Equal("ARE YOU MAXING OUT THE CONTRIBUTION AND MATCH?", question.TextDisplay);
        }

        [Fact]
        void GetNextQuestion_NoNextQuestion_NoException() {
            //    Arrange
            var answer = new Answer() {
                AnswerId = 16,
                NextQuestionId = null
            };
            //    Act
            var question = controller.NextQuestion(answer).Value;
            //    Assert
            Assert.Null(question);
        }
        
        [Fact]
        void GetLearnFromQuestionFromAnswer_Learn() {
            //    Arrange
            var questionIdContainedLearn = 5;
            var answer = new Answer() {
                AnswerId = 7,
                NextQuestionId = 5
            };
            //    Act
            var question = controller.NextQuestion(answer).Value;
            //    Assert
            Assert.NotNull(question);
            var secondAnswer = question.AnswersQuestion.ElementAt(0);
            Assert.NotNull(secondAnswer.Learn);
            var learn = secondAnswer.Learn;
            Assert.Equal("EMPLOYER MATCH / REDUCTION IN CURRENT TAXES / AUTO-SAVING", learn.TextDisplay);
        }

    }
}
