using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class OrderEndpoint : ApiEndpoint<Order>
{
    public OrderEndpoint(int orderId) : base(HttpMethod.Get, $"Orders/{orderId}")
    {
    }
}