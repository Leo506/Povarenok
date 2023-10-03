using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAll();
}