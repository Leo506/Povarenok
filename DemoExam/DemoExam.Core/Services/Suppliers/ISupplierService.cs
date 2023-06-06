namespace DemoExam.Core.Services.Suppliers;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAll();
}