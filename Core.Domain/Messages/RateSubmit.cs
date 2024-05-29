
namespace Core.Domain.Messages;

public class RateSubmit 
{
    public Guid ServiceProviderId { get; set; }
    public Guid CustomerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}