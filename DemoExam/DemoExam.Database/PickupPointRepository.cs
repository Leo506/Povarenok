using DemoExam.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoExam.Database;

public partial class TradeContext : IPickupPointRepository
{
    Task<List<PickupPoint>> IPickupPointRepository.GetAllAsync() => PickupPoints.ToListAsync();
}