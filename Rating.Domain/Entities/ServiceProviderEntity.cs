using Core.Domain.Entities;

namespace Rating.Domain.Entities;

public class ServiceProviderEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Rate> Ratings { get; set; }
}