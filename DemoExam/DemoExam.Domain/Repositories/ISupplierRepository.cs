using DemoExam.Domain.Models;

namespace DemoExam.Domain.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAll();
}