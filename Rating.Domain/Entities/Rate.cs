using Core.Domain.Entities;

namespace Rating.Domain.Entities;

public class Rate:BaseEntity
{
    public Guid ServiceProviderId { get; set; }
    public Guid CustomerId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public ServiceProviderEntity ServiceProviderEntity { get; set; }
}