using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Entities
{
    public abstract class Title : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<string> Genres { get; set; }
        public string ImgUrl { get; set; }
        public bool Fake { get; set; }
    }
}
