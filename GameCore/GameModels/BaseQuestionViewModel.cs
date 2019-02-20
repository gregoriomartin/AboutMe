using System.ComponentModel.DataAnnotations;

namespace GameCore.GameModels
{
    public class BaseQuestionViewModel 
    {
        public string Question { get; set; }
        [Display(Name = "Answer")]
        [Required]
        public string Answer { get; set; }
    }
}
