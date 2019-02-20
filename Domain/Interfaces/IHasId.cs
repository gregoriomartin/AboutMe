using System.ComponentModel.DataAnnotations;

namespace Domain.Interfaces
{
    public interface IHasId
    {
        [Key]
        long Id { get; set; }
    }
}
