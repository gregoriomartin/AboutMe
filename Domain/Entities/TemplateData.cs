using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TemplateData
    {
        [Key]
        public string TemplateType { get; set; }
        public List<TemplateQuestionText> QuestionsText { get; set; }
    }

    public class TemplateQuestionText : IHasId
    {
        public string QuestionText { get; set; }
        public long Id { get; set; }
    }
}
