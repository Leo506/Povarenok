using DemoExam.Domain.Models;
using DemoExam.Domain.Repositories;

namespace DemoExam.Domain.Services.Suppliers;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _repository;

    public SupplierService(ISupplierRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<Supplier>> GetAll()
    {
        return _repository.GetAll();
    }
}