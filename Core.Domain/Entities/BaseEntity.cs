namespace Core.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public BaseEntity()
    {
    }

    public BaseEntity(Guid id, DateTime createdAt) : this()
    {
        CreatedAt = createdAt;
        Id = id;
    }
}