using GameCore.Questions;
using System.ComponentModel.DataAnnotations;

namespace AboutMe.Models
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }

        public Question Question { get; set; }
    }
}
