namespace Core.Domain.Entities;

public class TransactionLog : MongoDbBaseEntity
{
    public string IpAddress { get; set; }
    public string DeviceInfo { get; set; }
    public string Request { get; set; }
    public string RequestName { get; set; }
    public string ResponseName { get; set; }
    public string Response { get; set; }
    public int StatusCode { get; set; }
}