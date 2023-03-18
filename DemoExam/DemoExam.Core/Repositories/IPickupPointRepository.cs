using DemoExam.Core.Models;

namespace DemoExam.Core.Repositories;

public interface IPickupPointRepository
{
    Task<List<PickupPoint>> GetAllAsync();
}