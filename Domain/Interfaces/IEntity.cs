using Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IEntity : IHasId
    {
        bool Fake { get; set; }
    }
}
