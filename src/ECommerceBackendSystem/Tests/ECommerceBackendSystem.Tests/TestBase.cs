namespace ECommerceBackendSystem.Tests;

public partial class TestBase
{
    public int ANumber() => new Random(10000000).Next();
    public string AString() => Guid.NewGuid().ToString();
    public Guid AGuid() => Guid.NewGuid();
    public DateTime ADateTime() => DateTime.Now;
}
