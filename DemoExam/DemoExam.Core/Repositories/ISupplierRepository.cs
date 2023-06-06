namespace DemoExam.Core.Repositories;

public interface ISupplierRepository
{
    Task<IEnumerable<Supplier>> GetAll();
}