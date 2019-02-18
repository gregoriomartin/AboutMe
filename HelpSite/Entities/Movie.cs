using System.Collections.Generic;

namespace HelpSite.Entities
{
    public class Movie : Title
    {
        public int Length { get; set; }
        public int Score { get; set; }
    }
}
