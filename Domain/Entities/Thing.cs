using Domain.Interfaces;

namespace Domain.Entities
{
    public class Thing : IEntity
    {
        public string Text { get; set; }
        public bool Loved { get; set; }
        public long Id { get; set; }
        public bool Fake { get; set; }
    }
}
