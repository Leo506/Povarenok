using DemoExam.Blazor.Shared;

namespace DemoExam.Blazor.Services.PickupPoints;

public interface IPickupPointsService
{
    Task<List<PickupPointDto>> GetAll();
}