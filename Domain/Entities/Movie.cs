using Domain;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Movie : Title
    {
        public int Length { get; set; }
        public int Score { get; set; }
    }
}
