using DemoExam.Domain.Models;

namespace DemoExam.Domain.Services.Suppliers;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAll();
}