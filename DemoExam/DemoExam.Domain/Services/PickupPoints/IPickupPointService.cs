using DemoExam.Domain.Models;

namespace DemoExam.Domain.Services.PickupPoints;

public interface IPickupPointService
{
    Task<List<PickupPoint>> GetAll();
}