using Domain.Interfaces;

namespace Domain.Entities
{
    public class Player : IHasId    
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Score { get; set; }
        public string MessageForGM { get; set; }
    }
}
