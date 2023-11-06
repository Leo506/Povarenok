using Arbus.Network;
using DemoExam.Blazor.Network.Endpoints;
using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.PickupPoints;

public class PickupPointsService : IPickupPointsService
{
    private readonly HttpClientContext _clientContext;

    public PickupPointsService(HttpClientContext clientContext)
    {
        _clientContext = clientContext;
    }

    public async Task<List<PickupPoint>> GetAll()
    {
        try
        {
            return await _clientContext.RunEndpoint(new PickupPointsEndpoint());
        }
        catch (Exception)
        {
            return new();
        }
    }
}