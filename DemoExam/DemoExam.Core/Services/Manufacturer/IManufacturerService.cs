namespace DemoExam.Core.Services.Manufacturer;

public interface IManufacturerService
{
    Task<IEnumerable<Domain.Models.Manufacturer>> GetAll();
}