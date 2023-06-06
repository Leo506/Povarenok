namespace DemoExam.Core.Repositories;

public interface IManufacturerRepository
{
    Task<IEnumerable<Manufacturer>> GetAll();
}