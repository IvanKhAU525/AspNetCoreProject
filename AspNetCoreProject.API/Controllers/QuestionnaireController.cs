using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionnaireController : ControllerBase
    {
        private readonly IQuestionnaireService _myService;

        public QuestionnaireController(IQuestionnaireService myService)
        {
            _myService = myService;
        }

        [HttpGet(nameof(GetQuestionnaire))]
        public ActionResult<Questionnaire> GetQuestionnaire(int questionnaireId = 1) {
            var questionnaire = _myService.GetQuestionnaire(questionnaireId);
            return questionnaire;
        }

        [HttpGet(nameof(GetNextQuestion))]
        public ActionResult<Question> GetNextQuestion(Answer answer) {
            var question = _myService.GetNextQuestion(answer);
            return question;
        }

        [HttpGet(nameof(GetFirstQuestionOfQuestionnaire))]
        public ActionResult<Question> GetFirstQuestionOfQuestionnaire(Questionnaire questionnaire) {
            var question = _myService.GetFirstQuestionOfQuestionnaire(questionnaire);
            return question;
        }
    }
}
