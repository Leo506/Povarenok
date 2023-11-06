using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class UserOrdersEndpoint : ApiEndpoint<List<OrderShort>>
{
    public UserOrdersEndpoint(int userId) : base(HttpMethod.Get, $"/Orders/User/{userId}")
    {
    }
}