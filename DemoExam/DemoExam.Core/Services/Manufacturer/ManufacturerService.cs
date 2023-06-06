using DemoExam.Core.Repositories;

namespace DemoExam.Core.Services.Manufacturer;

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