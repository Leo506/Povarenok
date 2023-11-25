using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class AllOrdersEndpoint: ApiEndpoint<List<OrderShort>>
{
    public AllOrdersEndpoint() : base(HttpMethod.Get, "/Orders")
    {
    }
}