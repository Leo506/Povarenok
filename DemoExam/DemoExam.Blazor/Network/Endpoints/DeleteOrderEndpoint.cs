using Arbus.Network;

namespace DemoExam.Blazor.Network.Endpoints;

public class DeleteOrderEndpoint: ApiEndpoint
{
    public DeleteOrderEndpoint(int orderId) : base(HttpMethod.Delete, $"Orders/{orderId}")
    {
    }
}