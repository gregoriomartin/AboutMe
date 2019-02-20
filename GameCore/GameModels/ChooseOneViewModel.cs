using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameCore.GameModels
{
    public class ChooseOneViewModel : BaseQuestionViewModel
    {
        public List<Title> Titles { get; set; }
    }
}
