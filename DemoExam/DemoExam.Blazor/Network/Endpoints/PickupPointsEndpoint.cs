using Arbus.Network;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Network.Endpoints;

public class PickupPointsEndpoint : ApiEndpoint<List<PickupPoint>>
{
    public PickupPointsEndpoint() : base(HttpMethod.Get, "/PickupPoints")
    {
    }
}