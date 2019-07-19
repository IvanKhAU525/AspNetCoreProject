using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using AspNetCoreProject.Domain.Entities.Domain;
using AspNetCoreProject.ServiceInterfaces.Interfaces;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AspNetCoreProject.Application.BusinessLogic.Services
{
    public class QuestionnaireFormatter : IQuestionnaireFormatter
    {
        private readonly ILogger _logger;
        public QuestionnaireFormatter(ILogger logger) {
            _logger = logger;
        }
        
        public Stream DownloadQuestionnaireJSON(Questionnaire questionnaire) {
            try {
                var memoryStream = new MemoryStream();
                var streamWriter = new StreamWriter(memoryStream);
                new JsonSerializer().Serialize(streamWriter, questionnaire);
                streamWriter.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Error, eventId: 1, exception: ex, null, null);
                return null;
            }
        }

        public Stream DownloadQuestionnaireCSV(IEnumerable<SimpleQuestionnaire> questionnaire) {
            try {
                var memoryStream = new MemoryStream(); 
                var streamWriter = new StreamWriter(memoryStream);
                var csvWriter = new CsvWriter(streamWriter);
                csvWriter.WriteRecords(questionnaire);
                csvWriter.Flush();
                streamWriter.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);
                return memoryStream;
            }
            catch (Exception ex) {
                _logger.Log(LogLevel.Error, eventId: 1, exception: ex, null, null);
                return null;
            }
        }
    }
}