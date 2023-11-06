using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Requests;

namespace DemoExam.Blazor.Network.Endpoints;

public class NewOrderEndpoint : ApiEndpoint
{
    private readonly NewOrder _newOrder;
    
    public NewOrderEndpoint(NewOrder newOrder) : base(HttpMethod.Post, "Orders")
    {
        _newOrder = newOrder;
    }

    public override HttpContent? CreateContent() => ToJson(_newOrder);
}