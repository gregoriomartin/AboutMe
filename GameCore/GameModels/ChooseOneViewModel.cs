using Domain.Entities;
using System.Collections.Generic;

namespace GameCore.GameModels
{
    public class ChooseOneViewModel : BaseQuestionViewModel
    {
        public List<Title> Titles { get; set; }
        public long SelectedTitleId { get; set; }
    }
}
