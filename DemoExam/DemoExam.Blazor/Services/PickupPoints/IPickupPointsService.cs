using DemoExam.Blazor.Shared.Dto.Responses;

namespace DemoExam.Blazor.Services.PickupPoints;

public interface IPickupPointsService
{
    Task<List<PickupPoint>> GetAll();
}