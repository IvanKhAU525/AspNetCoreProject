using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreProject.API.Logger;
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

        public QuestionnaireController(IQuestionnaireService myService) {
            _myService = myService;
        }

        [HttpPut("OpenSession")]
        public int OpenSession() => _myService.OpenSession();
        
        [HttpGet("Questionnaire")]
        public ActionResult<Questionnaire> UploadQuestionnaire(int questionnaireId = 1) =>
            _myService.GetQuestionnaire(questionnaireId);

        [HttpGet("NextQuestion")]
        public ActionResult<Question> NextQuestion(int questionId, int answerId, int sessionId) => 
            _myService.GetNextQuestion(questionId, answerId, sessionId);

        [HttpGet("FirstQuestionOfQuestionnaire")]
        public ActionResult<Question> FirstQuestionOfQuestionnaire(int questionnaireId) =>
            _myService.GetFirstQuestionOfQuestionnaire(questionnaireId);

        [HttpGet("AnswerAction")]
        public ActionResult<Domain.Entities.Domain.Action> AnswerAction(int answerId) =>
            _myService.GetAnswerAction(answerId);

        [HttpGet("DownloadQuestionnaireCSV")]
        public FileResult DownloadQuestionnaireCSV(int questionnaireId) {
            var stream = _myService.DownloadQuestionnaireCSV(questionnaireId);
            return File(stream, "application/octet-stream", "questionnaire.csv");
        }

        [HttpGet("DownloadQuestionnaireJSON")]
        public FileResult DownloadQuestionnaireJSON(int questionnaireId) {
            var stream = _myService.DownloadQuestionnaireJSON(questionnaireId);
            return File(stream, "application/octet-stream", "questionnaire.json");
        }
    }
}
