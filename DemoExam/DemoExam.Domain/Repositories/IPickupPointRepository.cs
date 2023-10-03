using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IPickupPointRepository
{
    Task<List<PickupPoint>> GetAllAsync();
}