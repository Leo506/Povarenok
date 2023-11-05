using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.PickupPoints;

public class PickupPointService : IPickupPointService
{
    private readonly IPickupPointRepository _pickupPointRepository;

    public PickupPointService(IPickupPointRepository pickupPointRepository)
    {
        _pickupPointRepository = pickupPointRepository;
    }

    public Task<List<PickupPoint>> GetAll()
    {
        return _pickupPointRepository.GetAllAsync();
    }
}