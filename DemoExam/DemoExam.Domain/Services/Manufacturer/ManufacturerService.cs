using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Manufacturer;

public class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _repository;

    public ManufacturerService(IManufacturerRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Domain.Models.Manufacturer>> GetAll()
    {
        return _repository.GetAll();
    }
}