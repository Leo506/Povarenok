namespace DemoExam.Domain.Services.Manufacturer;

public interface IManufacturerService
{
    Task<IEnumerable<Domain.Models.Manufacturer>> GetAll();
}