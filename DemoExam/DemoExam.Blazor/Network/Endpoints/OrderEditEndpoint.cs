using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Requests;

namespace DemoExam.Blazor.Network.Endpoints;

public class OrderEditEndpoint: ApiEndpoint
{
    private readonly OrderEdit _dto;

    public OrderEditEndpoint(int orderId, OrderEdit dto) : base(HttpMethod.Put, $"/Orders/{orderId}")
    {
        _dto = dto;
    }

    public override HttpContent? CreateContent() => ToJson(_dto);
}